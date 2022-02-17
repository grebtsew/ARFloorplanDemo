using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlaySoundOnFingerCollision : MonoBehaviour
{
    private AudioSource sound;
    public Material collision_mat;

    public TextMeshProUGUI Text;

    private Renderer rend;
    private Material tmp_mat;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        sound = GetComponent<AudioSource>();
        tmp_mat = rend.material;
    }

     private void OnTriggerEnter(Collider other)
    {

        //Debug.Log(other.ToString());
        // play sound and change material of object
         if (other.tag == "TriggerPoint")
        {
       // Debug.Log("Tangent Pressed");
        Text.text = this.gameObject.name;
        rend.material = collision_mat;
        sound.Play();
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        // reset color
        Text.text = "";
        rend.material = tmp_mat;
    }
}
