    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using System;


public class AngleTowardCamera : MonoBehaviour
{

    // Set these in the editor, or in code.
    //transforms
    public Transform bannerLookTarget;
    public Transform bannerDolly;
    //banner text component
    public TextMesh tmp_text_banner;
    
    // Start is called before the first frame update
    void Start()
    {
        try{
        if (Camera.main.transform != null){
        bannerLookTarget = Camera.main.transform;  
       // Debug.Log(bannerDolly.ToString() +" " +bannerLookTarget.ToString()+ " "+ tmp_text_banner.ToString() );
        }
        } catch (Exception e) {
             Debug.Log("Failed in AngleTowardsCamera : " + e.ToString() );
             //Debug.Log(bannerDolly.ToString() +" " +bannerLookTarget.ToString()+ " "+ tmp_text_banner.ToString() );
        }
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if(bannerDolly != null){
        if(bannerLookTarget != null){
        Vector3 lookDir = tmp_text_banner.transform.position - bannerLookTarget.position * 2;
        lookDir.y = 0;
        tmp_text_banner.transform.rotation = Quaternion.LookRotation(lookDir);
    }
    }
    }

}
