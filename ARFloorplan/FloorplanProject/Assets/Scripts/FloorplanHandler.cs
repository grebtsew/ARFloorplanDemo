using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorplanHandler : MonoBehaviour
{

    private GameObject floorplan;
    private List<GameObject>  instantiated = new List<GameObject>();

    private GameObject target;
    private GameObject ob_target;
    private GameObject target_instance;

    // Start is called before the first frame update
    void Start()
    {
        reloadFloorplan();
    }

    public void reloadScene(){
        // BAD IDEA!
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
    }

    public void reloadFloorplan(){
        // remove old
        if (floorplan != null){
            Destroy(floorplan);
            floorplan = null;

            foreach(GameObject furn in instantiated){
                 Destroy(furn);
            }

            instantiated.Clear();
        }

        // create new from global variables
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
        Rigidbody floorplanRB = floorplan.GetComponent<Rigidbody>();
        floorplanRB.useGravity = false;
        floorplanRB.isKinematic = true;
        floorplanRB.collisionDetectionMode = CollisionDetectionMode.Continuous;


        int count = GlobalVariables.Get<int>("FurnAmount");
        
        for (int i = 0; i < count; i++){
            string goname =  GlobalVariables.Get<string>("FurnName"+i.ToString()); 
            Debug.Log(goname);

            Vector3 furnpos = GlobalVariables.Get<Vector3>("PosFurn"+i.ToString());
            Debug.Log(furnpos);
            Quaternion furnrot = GlobalVariables.Get<Quaternion>("RotFurn"+i.ToString());
            Vector3 furnscale = GlobalVariables.Get<Vector3>("ScaleFurn"+i.ToString());
            GameObject go_furn = (GameObject)Resources.Load("Furniture/"+goname, typeof(GameObject));
            GameObject tmp = Instantiate(go_furn,furnpos, furnrot);

            tmp.AddComponent<BoxCollider>();
            BoxCollider tmpCollider = tmp.GetComponent<BoxCollider>();
            //tmpCollider.isTrigger = true;

            tmp.AddComponent<Rigidbody>(); // Add the rigidbody.
            Rigidbody tmpRB =tmp.GetComponent<Rigidbody>();
            tmpRB.useGravity = true;
            tmpRB.mass = 5; // Set the GO's mass to 5 via the Rigidbody. // update gravity and so on!
            tmpRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            instantiated.Add(tmp);
        }
        
        
       
    }

    public void Exit(){
    // save any game data here
     #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
    }

}
