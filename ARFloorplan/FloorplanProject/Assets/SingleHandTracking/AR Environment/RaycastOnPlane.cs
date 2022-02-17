using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class RaycastOnPlane : MonoBehaviour {

    [SerializeField]
    GameObject hitOnPlane;
    private ARHandProcessor handProcessor;
    private Laze lazeOnSpace = default;
    private RaycastHit hit;

    void Start() {
        handProcessor = GetComponent<ARHandProcessor>();
        lazeOnSpace = new Laze();
        lazeOnSpace.enable();
    }

    void Update() {
        if (!tryGetRayData(out Ray ray)) {
            return;
        } else {
            lazeOnSpace.Origin = handProcessor.CurrentHand.GetLandmark(0);
            lazeOnSpace.Tail = ray.direction * 20;
        }
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (hitOnPlane != null) hitOnPlane.transform.position = hit.point;
            lazeOnSpace.moveTo(handProcessor.CurrentHand.GetLandmark(0), hit.point);
            // update laze
            lazeOnSpace.Origin = handProcessor.CurrentHand.GetLandmark(0);
            lazeOnSpace.Tail = hit.point;
        } else {
            lazeOnSpace.Origin = handProcessor.CurrentHand.GetLandmark(0);
            lazeOnSpace.Tail = ray.direction * 20;
        }
    }

    private bool tryGetRayData(out Ray ray) {
        if (handProcessor == null || handProcessor.CurrentHand == null) {
            ray = default;
            Debug.Log("Can't provide Raycast");
            return false;
        }

        Vector3 origin = handProcessor.CurrentHand.GetLandmark(0);
        Vector3 a = handProcessor.CurrentHand.GetLandmark(0);
        Vector3 b = handProcessor.CurrentHand.GetLandmark(5);
        Vector3 c = handProcessor.CurrentHand.GetLandmark(17);
        Vector3 d = (a + b + c) / 3 + Vector3.Distance(b, c) * Vector3.down / 5;
        Vector3 direction = d - origin;

        ray = new Ray(origin, direction);
        return true;
    }

    public Laze LazeOnSpace { get => lazeOnSpace; }
    public RaycastHit Hit { get => hit; set => hit = value; }
}
