using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using TMPro;
using System;

public class CalculateDistance : MonoBehaviour
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
private Vector3 end_pos;

private float timer;
private float press_time_limit = 1f;

public GameObject Endpoints;
public GameObject Banner;
private GameObject Banner_inst;

public TextMeshProUGUI Text;
public LineRenderer linerenderer; 

private GameObject start;
private GameObject end;

private bool gotStart = false;
private bool gotEnd = false;
private float low_tresh = 0.2f; // 10 cm

void Start()
{
   
}

void Update()
{
    try{

   
    
    if (Input.touchCount == 0)
        return;

    //Debug.Log("Someone touched the screen!");
    timer+=Time.deltaTime;

    if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
    {
         //Debug.Log(timer.ToString());
            
        if(timer < press_time_limit)
        {
            return;
        }

        timer= 0f;

        //Debug.Log("Touched " + m_Hits[0].hitType.ToString() + " at pose "+ m_Hits[0].pose.ToString() +" at distance "+ m_Hits[0].distance.ToString());

        if(start_pos != m_Hits[0].pose.position && end_pos !=  m_Hits[0].pose.position){


        if(gotEnd){ // new calculation
            start_pos = m_Hits[0].pose.position;
            gotEnd = false;
            gotStart = true;
           
        } else {

            if(!gotStart){ // first time
                start_pos = m_Hits[0].pose.position;
                gotEnd = false;
                gotStart = true;
            } else {
                gotEnd = true;
                gotStart = true;
                end_pos = m_Hits[0].pose.position;
            }
        }


        if (gotStart){
            DrawDot(ref start, start_pos, "start endpoint", true);
            
        }

        // perform new calculation!
        if (gotEnd && gotStart){
            string result = Vector3.Distance(start_pos, end_pos).ToString();
            if(Vector3.Distance(start_pos, end_pos) < low_tresh){
                gotEnd = false;
                linerenderer.positionCount = 0; // clear line!
                if (Banner_inst != null){
                Banner_inst.GetComponent<TextMesh>().text = "";
                   }
                return;
            }
            
            DrawDot(ref end, end_pos, "end endpoint");
            DrawLine(start_pos, end_pos, result, linerenderer);
            
            // show result on screen
            if (Text){
                Text.text = result;
            }

            gotStart = false;
            gotEnd = false;
        }
    }

 }}
 catch (Exception e)
 {
     Debug.Log("Failed in Calculate Distance : " + e.ToString() );
 }
}

void DrawLine(Vector3 start, Vector3 end, string result, LineRenderer linerenderer)
{
    // Draw 3d line from start to end with result text in middle, using linerenderer
   linerenderer.positionCount = 2;
   linerenderer.SetPosition (0, start);
   linerenderer.SetPosition (1, end);

    Vector3 midpos = (start + end) / 2f;
    if(Banner_inst == null){
       Banner_inst = GameObject.Instantiate(Banner, midpos, Quaternion.identity) as GameObject;
       Banner_inst.GetComponent<TextMesh>().text = result;
    } else {
        Banner_inst.GetComponent<TextMesh>().text = result;
        Banner_inst.transform.position = midpos;
    }

}


 void DrawDot(ref GameObject go, Vector3 pos, string name, bool start=false)
 {
    if (go == null){
       go = GameObject.Instantiate(Endpoints, pos, Quaternion.identity) as GameObject;
       go.GetComponentInChildren<TextMesh>().text = name;
    } else {
       go.transform.position = pos;
    }
 }

}


