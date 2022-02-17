using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using static GlobalVariables;

namespace Kakera{
public class FloorplanSelector : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] floorplans;
    private int target_index = 0;
    private GameObject target;
    private GameObject target_instance;

    [SerializeField]
    public Unimgpicker imagePicker;

    public Camera cam;

    void Start()
    {
    // Only render objects in the first layer (Default layer)
    cam.cullingMask = 1 << 0;


    // load floorplans objects
    floorplans = Resources.LoadAll("Floorplans", typeof(GameObject))
     .Cast<GameObject>()
     .ToArray();

    
     update();

  // Unimgpicker returns the image file path.
            imagePicker.Completed += (string path) =>
            {
               Debug.Log( GetPath(path));
            };

    }

     private string GetPath(string path)
        {
            Debug.Log(path);
            return path;
        }

    public void select(){
        //imagePicker.Show("Select Image", "unimgpicker");

        GlobalVariables.Set("TargetFloorplan", floorplans[target_index].name);
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlaceFloorplan");
    }

    private void update(){
        
        GameObject newtarget = floorplans[target_index];
        if (target != newtarget){

        Destroy (target_instance);
        target_instance = Instantiate(newtarget,cam.transform.position + cam.transform.forward * 1, Quaternion.identity);
        target_instance.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        target = newtarget;
        }
    }

    public void left(){
    target_index--;
    if (target_index < 0){
         target_index = floorplans.Length-1;
    }
    update();
    }

    public void right(){
        target_index++;
    if (target_index >= floorplans.Length){
         target_index = 0;
    }
    update();
    }

    // Update is called once per frame
    void Update()
    {
        target_instance.transform.position = Vector3.MoveTowards(target_instance.transform.position, cam.transform.position + cam.transform.forward * 1, 0.1f );
    }
}
}