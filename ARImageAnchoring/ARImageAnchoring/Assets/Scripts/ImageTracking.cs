using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
   [SerializeField]
   public GameObject[] placeablePrefabs;

   private Dictionary<string, GameObject> spawnedPrefab = new Dictionary<string,GameObject>();
   private ARTrackedImageManager arTrackedImageManager;

   void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefab.Add(prefab.name, newPrefab);
           // Debug.Log("Created prefab, " +prefab.name);
        }
    }
 
    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
 
    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }
 
 
    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.rotation;

        spawnedPrefab[name].SetActive(true);
        spawnedPrefab[name].transform.position = position;
        spawnedPrefab[name].transform.rotation = rotation;

         //Debug.Log("Updated or Added to scene " +name);

        foreach(GameObject go in spawnedPrefab.Values){
            if (go.name != name){
                go.SetActive(false);
            }
        }
    }
 
    public void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
           UpdateImage(trackedImage);
        }
 
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefab[trackedImage.name].SetActive(false);
        }
    }
}

