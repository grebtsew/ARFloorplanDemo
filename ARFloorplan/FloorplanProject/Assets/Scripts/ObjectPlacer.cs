using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using static GlobalVariables;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;


public class ObjectPlacer : MonoBehaviour
{

    public Dropdown dropdown;

    private List<GameObject>  instantiated = new List<GameObject>();
    private GameObject[] objects;

    private GameObject target;
    private GameObject ob_target;
    private GameObject target_instance;

    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    private GameObject floorplan;
    public float updateTime = 1f;
    float counter = 0;



    public void dropdownChange(Dropdown change){
        foreach (GameObject obj in objects)
        {
            
            if (obj.name == dropdown.options[change.value].text ){
                
                if (target != null){
                    Vector3 pos = target.transform.position;
                    Destroy(target);
                    ob_target = obj;
                    target = Instantiate(obj, pos, Quaternion.identity);
                    //target.transform.localScale = floorplan.transform.localScale;
                    target.AddComponent<BoxCollider>();
                    target.GetComponent<BoxCollider>().enabled = false;
                } else {
                ob_target = obj;
                target = Instantiate(obj, new Vector3(0f,0f,0f), Quaternion.identity);
                    //target.transform.localScale = floorplan.transform.localScale;
                    target.AddComponent<BoxCollider>();
                    target.GetComponent<BoxCollider>().enabled = false;
                }
               break;
            }
        }
    }

   

     public void clearTarget(){
        if (target != null){
        Destroy(target);
        target = null;
        }
        
        dropdown.value = 0;
    }

    public void removeLatest(){
        int index = instantiated.Count -1;

        if (index >= 0){
            Destroy(instantiated[index]);
            instantiated.RemoveAt( index);
        }
    }

    private void loadFloorplan(){
        string targetFloorplan = GlobalVariables.Get<string>("TargetFloorplan");
            
        // Will start by placing it in middle of screen, to be moved later!
        if (targetFloorplan == null){
            // use default
            GameObject go_floorplan = (GameObject)Resources.Load("Floorplans/floorplan1", typeof(GameObject));
            floorplan = Instantiate(go_floorplan,Camera.main.transform.position + Camera.main.transform.forward * 1, Quaternion.identity);
            floorplan.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

        } else {
            GameObject go_floorplan = (GameObject)Resources.Load("Floorplans/"+targetFloorplan, typeof(GameObject));
            floorplan = Instantiate(go_floorplan,Camera.main.transform.position + Camera.main.transform.forward * 1, Quaternion.identity);
            floorplan.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }

        Vector3 pos = GlobalVariables.Get<Vector3>("Position");
        Quaternion rot = GlobalVariables.Get<Quaternion>("Rotation");
        Vector3 scale = GlobalVariables.Get<Vector3>("Scale");

        floorplan.transform.position = pos;
        floorplan.transform.rotation = rot;
        floorplan.transform.localScale = scale;
        
        floorplan.AddComponent<BoxCollider>();

       BoxCollider boxCollider = floorplan.GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(0,-floorplan.transform.localScale.y/2,0);
        boxCollider.size = new Vector3(20f,floorplan.transform.localScale.y,20f);
        //boxCollider.isTrigger = true;

        floorplan.AddComponent<Rigidbody>(); // Add the rigidbody.
        Rigidbody floorplanRB =floorplan.GetComponent<Rigidbody>();
        floorplanRB.useGravity = false;
        floorplanRB.isKinematic = true;
    }

    // Start is called before the first frame update
    void Start()
    {   

        // setup floorplan
       loadFloorplan();

        // get available objects
        objects = Resources.LoadAll("Furniture", typeof(GameObject))
        .Cast<GameObject>()
        .ToArray();

        // setup combobox
        dropdown.options.Add (new Dropdown.OptionData() {text="None"});
        foreach (GameObject obj in objects)
        {
            dropdown.options.Add(new Dropdown.OptionData() {text=obj.name});
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(target != null){

      //  if (m_RaycastManager.Raycast(new Vector3(Screen.width/2, Screen.height/2, 0), m_Hits)) //Input.GetTouch(0).position
       //     {
            RaycastHit m_Hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out m_Hit, Mathf.Infinity)){
           
            target.transform.position=m_Hit.point+new Vector3(0,target.GetComponent<BoxCollider>().size.y/2,0);
            float yRotation = Camera.main.transform.eulerAngles.y;

            target.transform.eulerAngles = new Vector3( floorplan.transform.eulerAngles.x, yRotation, floorplan.transform.eulerAngles.z );

             if (counter < updateTime){
            counter += Time.deltaTime;
            }    
            else
            {
                
            if (Input.touchCount > 1){
                
                // instantiate new object!
                if (ob_target != null){
               
                target_instance = Instantiate(ob_target,target.transform.position, target.transform.rotation);
                instantiated.Add(target_instance);
                }
            counter = 0;
            }  
            
            }
      
            }
        }
    }  
    

    public void nextScene(){
        GlobalVariables.Set("Furniture", instantiated);
        GlobalVariables.Set("FurnAmount", instantiated.Count);
        // save all furnitures!
        for(int i = 0; i < instantiated.Count; i++ ){
            GlobalVariables.Set("Furn"+i.ToString() , instantiated[i]);
             
        foreach (GameObject obj in objects){
            if (obj.name == ""){
                continue;
            }
            if (instantiated[i].name.Contains(obj.name)){
                Debug.Log("Added "+obj.name);
                GlobalVariables.Set("FurnName"+i.ToString() , obj.name);
                break;
            }
        }
           
            GlobalVariables.Set("PosFurn"+i.ToString() , instantiated[i].transform.position);
            GlobalVariables.Set("RotFurn"+i.ToString() , instantiated[i].transform.rotation);
            GlobalVariables.Set("ScaleFurn"+i.ToString() , instantiated[i].transform.localScale);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("HandsInteractable");
    }
}
