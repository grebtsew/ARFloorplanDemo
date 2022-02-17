using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrabObject : MonoBehaviour
{

    public GameObject F1;
    public GameObject F2;
    public float grab_treshhold = 0.05f; // 5 cm?
    public float grab_object_treshhold = 0.1f;

    List<GameObject> grabbinglist = new List<GameObject>();
    List<Vector3> offsetlist = new List< Vector3>();

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(F1.transform.position, F2.transform.position) < grab_treshhold){
            // Grabbing
            Vector3 center = (F1.transform.position + F2.transform.position) / 2;
             GameObject[] obj_to_grab_array = FindObjectsOfType<GameObject>()
                .Where(t => Vector3.Distance(t.transform.position, center) < grab_object_treshhold)
                .ToArray();

            foreach (GameObject obj in obj_to_grab_array){
                if (grabbinglist.Contains(obj) ){
                    continue;
                } else {
                    grabbinglist.Add(obj);
                    offsetlist.Add(obj.transform.position - center);
                }
            }

            // update all positions
            for (int i = 0; i<grabbinglist.Count; i++){
                grabbinglist[i].transform.position = offsetlist[i] + center;
            }

        } else {
            grabbinglist.Clear();
            offsetlist.Clear();
        }
    }
}
