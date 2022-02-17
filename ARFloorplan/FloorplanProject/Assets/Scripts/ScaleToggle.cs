using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleToggle : MonoBehaviour
{
    public Toggle toggle;
    private Camera cam;
    private bool start = true;
    private bool doonce = false;
    private float start_distance = 1f;

    // Start is called before the first frame update
    private GameObject Target;
    void Start()
    {
        Target = this.GetComponent<FloorplanPlacement>().Target;
        cam =  this.GetComponent<FloorplanPlacement>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn){
            if(Target == null){
                Target = this.GetComponent<FloorplanPlacement>().Target;
                return;
            }
            if (start){
                doonce = true;
                start_distance = Vector3.Distance(cam.transform.position, Target.transform.position);

                start = false;
            } 

            float scale = ( Vector3.Distance(cam.transform.position, Target.transform.position) - start_distance)/10; // might need to be changed a bit!
            Target.transform.localScale += new Vector3(scale,scale,scale);

        }else {
            if (doonce){
                start = true;
                doonce = false;
            }
        }
    }
}
