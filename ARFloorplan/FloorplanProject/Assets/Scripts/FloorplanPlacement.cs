using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloorplanPlacement : MonoBehaviour
{
    // use this to access target object!
    public GameObject go_Target;
    public GameObject Target;
    public Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        string targetFloorplan = GlobalVariables.Get<string>("TargetFloorplan");
        Debug.Log(targetFloorplan); 

        // Will start by placing it in middle of screen, to be moved later!
        if (targetFloorplan == null){
            // use default
            go_Target = (GameObject)Resources.Load("Floorplans/floorplan1", typeof(GameObject));
            Target = Instantiate(go_Target,cam.transform.position + cam.transform.forward * 1, Quaternion.identity);
            Target.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

        } else {
            go_Target = (GameObject)Resources.Load("Floorplans/"+targetFloorplan, typeof(GameObject));
            Target = Instantiate(go_Target,cam.transform.position + cam.transform.forward * 1, Quaternion.identity);
            Target.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }
    }

    public void nextScene(){
        // does this when we want to run next scene. Save target transform and start next scene!

        GlobalVariables.Set("Position", Target.transform.position);
        GlobalVariables.Set("Rotation", Target.transform.rotation);
        GlobalVariables.Set("Scale", Target.transform.localScale);

        UnityEngine.SceneManagement.SceneManager.LoadScene("PlaceObject");
    }

      public void moveToCenter(){
        // does this when we want to run next scene. Save target transform and start next scene!

       Target.transform.position = cam.transform.position + cam.transform.forward * 1;
    }

}
