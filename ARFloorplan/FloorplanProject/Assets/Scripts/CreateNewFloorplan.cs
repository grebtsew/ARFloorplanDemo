using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Linq;
using System.Threading;
//using static UnityEngine.Windows.File;

namespace Kakera{

public class idResponse
{
    public string id;
    public string hash;
}

public class CreateNewFloorplan : MonoBehaviour
{

    [SerializeField]
    public Unimgpicker imagePicker;

    private idResponse id;
    private string create_and_preform_json;

    private bool reachable = false;
    //public Panel ServerNotReachable;

    // Start is called before the first frame update
    void Start()
    {
  
    }

/*
 IEnumerator GetRequest(string uri, string path)
{
    var uwr = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbGET);
       
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
            Debug.LogError(uwr.error);
        else
            Debug.Log("File successfully downloaded and saved to " + path);
    
}
*/

/*
  IEnumerator CreateAndTransform(string url, string path)
{

    // load image!
    //byte[] dataToPut = ReadAllBytes(path);
    //Debug.Log(dataToPut.Length.ToString());
    //UnityWebRequest uwr = UnityWebRequest.Put(url, dataToPut);
    //uwr.SetRequestHeader("Content-Type", "multipart/form-data");

     //byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(dataToPut);
     //uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(dataToPut);
     uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    // uwr.SetRequestHeader("Content-Type", "application/json");
    yield return uwr.SendWebRequest();

    if (uwr.isNetworkError)
    {
        Debug.Log("Error While Sending: " + uwr.error);
    }
    else
    {
        Debug.Log("Received: " + uwr.downloadHandler.text);
        Debug.Log("Wait for it to end!");
        Thread.Sleep(5000);
        Debug.Log("Done, now collect .obj file!");
        StartCoroutine(GetRequest("http://192.168.2.225:8000/?func=object?id="+id.id+"?oformat=.obj", "Floorplans/"+id.id+".obj"));
        StartCoroutine(GetRequest("http://192.168.2.225:8000/?func=object?id="+id.id+"?oformat=.mtl", "Floorplans/"+id.id+".mtl"));
        // Restart here
        Debug.Log("All seems to be done, reload floorplans!");
    }
}
*/

 IEnumerator GetID(string url)
 {
     var uwr = new UnityWebRequest(url, "POST");
    
     uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
     uwr.SetRequestHeader("Content-Type", "application/json");

     //Send the request then wait here until it returns
     yield return uwr.SendWebRequest();

     if (uwr.isNetworkError)
     {
         Debug.Log("Error While Sending: " + uwr.error);
     }
     else
     {
         Debug.Log("Received: " + uwr.downloadHandler.text);

        string[] tmp = uwr.downloadHandler.text.Split('\'');
       

        id = new idResponse();
        id.id =  tmp[1];
        id.hash = tmp[3];
      /*
         #if UNITY_EDITOR
                    string path = UnityEditor.EditorUtility.OpenFilePanel("Open image","","jpg,png,bmp");
                    if (!System.String.IsNullOrEmpty (path))
                      

                    Debug.Log("file:///" + path);
            #else
                    ImageUploaderCaptureClick ();
            #endif
      */      
             create_and_preform_json = "{'func': 'createandtransform', 'id':'"+id.id+"', 'hash':'"+id.hash+"', 'iformat':'.jpg', 'oformat':'.obj'}";
            Debug.Log(create_and_preform_json);
            string _url = "http://192.168.2.225:8000/?func=createandtransform?id="+id.id+"?hash="+id.hash+"?iformat=.jpg?oformat=.obj";
            Debug.Log(_url);
             // CreateAndTransform( _url,path);
     }
 }

    public void create(){
       //StartCoroutine(GetID("http://192.168.2.225:8000/?func=create"));
       imagePicker.Show("Select Image", "unimgpicker");
    }

}
}
