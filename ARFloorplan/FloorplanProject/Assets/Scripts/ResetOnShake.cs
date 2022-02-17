using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ResetOnShake : MonoBehaviour
{
// FROM
// https://stackoverflow.com/questions/31389598/how-can-i-detect-a-shake-motion-on-a-mobile-device-using-unity3d-c-sharp/31389776
// https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/manual/tracked-image-manager.html

float accelerometerUpdateInterval = 1.0f / 60.0f;
// The greater the value of LowPassKernelWidthInSeconds, the slower the
// filtered value will converge towards current input sample (and vice versa).
float lowPassKernelWidthInSeconds = 1.0f;
// This next parameter is initialized to 2.0 per Apple's recommendation,
// or at least according to Brady! ;)
float shakeDetectionThreshold = 2.0f;

float lowPassFilterFactor;
Vector3 lowPassValue;

private FloorplanHandler fph;

void Start()
{
    lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
    shakeDetectionThreshold *= shakeDetectionThreshold;
    lowPassValue = Input.acceleration;
    fph = GetComponent<FloorplanHandler>();
}

void Update()
{
    Vector3 acceleration = Input.acceleration;
    lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
    Vector3 deltaAcceleration = acceleration - lowPassValue;

    if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
    {
        // Perform your "shaking actions" here. If necessary, add suitable
        // guards in the if check above to avoid redundant handling during
        // the same shake (e.g. a minimum refractory period).

        /* Remove all boxes! */
        
        // TODO: Call on floorplan reset here!
         Debug.Log("Shake event detected at time:  "+Time.time);
         if (fph == null){
             fph = GetComponent<FloorplanHandler>();
         } else {
            fph.reloadFloorplan();
         }
        
    }
}
}


