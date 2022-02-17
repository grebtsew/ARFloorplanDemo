using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToggle : MonoBehaviour
{
    public Toggle toggle;
    private Camera cam;
    private bool start = true;
    private bool doonce = false;
    private Vector3 start_offset;

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
                start_offset = cam.transform.position - Target.transform.position;
                start = false;
            } 

           Target.transform.position = Vector3.MoveTowards(Target.transform.position, cam.transform.position + cam.transform.forward * 1, 0.1f );

        }else {
            if (doonce){
                start = true;
                doonce = false;
            }
        }
    }
}
