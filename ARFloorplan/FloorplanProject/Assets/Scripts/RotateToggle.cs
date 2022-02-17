using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateToggle : MonoBehaviour
{
    public Toggle toggle;
    private Camera cam;


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
                var lookPos = cam.transform.position - Target.transform.position;
                
                var rotation = Quaternion.LookRotation(lookPos);
                Target.transform.rotation = Quaternion.Slerp(Target.transform.rotation, rotation, Time.deltaTime * 0.2f);

            
        }
    }
}