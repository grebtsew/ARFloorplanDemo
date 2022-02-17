using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class CreateObjectOnPress : MonoBehaviour
{

/*
public bool Raycast(
    Vector2 screenPoint,
    List<ARRaycastHit> hitResults,
    TrackableType trackableTypes = TrackableType.All)
*/

[SerializeField]
ARRaycastManager m_RaycastManager;
List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

private Vector3 start_pos = new  Vector3(0,0,0);
public GameObject StartDot;



void Update()
{

    
    if (Input.touchCount == 0)
        return;

    //Debug.Log("Someone touched the screen!");

    if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
    {
        //Debug.Log("Touched " + m_Hits[0].hitType.ToString() + " at pose "+ m_Hits[0].pose.ToString() +" at distance "+ m_Hits[0].distance.ToString());

       DrawDot(m_Hits[0].pose.position, "test");
}

 void DrawDot(Vector3 pos, string name)
 {
    Instantiate(StartDot, pos, Quaternion.identity);
 }

}
}


