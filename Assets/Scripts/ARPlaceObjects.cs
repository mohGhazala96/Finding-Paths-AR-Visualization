using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
[RequireComponent(typeof(ARRaycastManager))]
public class ARPlaceObjects : MonoBehaviour
{
    private ARRaycastManager rayCastMgr;
    public GameObject placementIndicator;

    private Pose placementPose;
    bool isPlacementValid;
    bool didPlaceGride;
    public GameObject grid;
    private void Start()
    {
        rayCastMgr = GetComponent<ARRaycastManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!didPlaceGride)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }

        if (isPlacementValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !didPlaceGride)
        {
            didPlaceGride = true;
            grid.SetActive(true);
            placementIndicator.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        }

    }
    public void UpdatePlacementIndicator()
    {
        if (isPlacementValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);

        }
    }
    public void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        rayCastMgr.Raycast(screenCenter,hits, TrackableType.Planes);
        isPlacementValid = hits.Count > 0;
        if (isPlacementValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z);
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
