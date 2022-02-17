using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPosition : MonoBehaviour
{
    public GameObject arCameraObject;
    private TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = arCameraObject.transform.position.ToString();
        //Debug.Log(arCameraObject.transform.position.ToString());
    }
}
