using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class ImageAnchorToggle : MonoBehaviour
{
    public Toggle toggle;
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

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
             // TODO: activate, deactivate cloudpoints!

        if (Input.touchCount == 0)
            return;

    if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
    {
         Target.transform.position = m_Hits[0].pose.position;
    }
        }
    }
}
