using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTrackToggle : MonoBehaviour
{
    public Toggle toggle;


    // this is more autonomous and need more setup at start!!!

    // Start is called before the first frame update
    private GameObject Target;
    void Start()
    {
        Target = this.GetComponent<FloorplanPlacement>().Target;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn){
            if(Target == null){
                Target = this.GetComponent<FloorplanPlacement>().Target;
                return;
            }
        }
    }
}
