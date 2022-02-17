using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Manager : MonoBehaviour {
    #region Singleton
    public static Manager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject HandOnSpace;
    public RaycastOnPlane RaycastOnPlane;
}