using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PlacementController : MonoBehaviour {
    [SerializeField]
    private ARRaycastManager raycastManager;
    [SerializeField]
    private GameObject spawObj;

    [SerializeField]
    private Text text;
    private GameObject placementObj;
    // private List<ARRaycastHit> hits = new List<ARRaycastHit>();



    void Start() {
        //placementObj = Instantiate(placementPref, Vector3.zero, Quaternion.identity);
        //placementObj.SetActive(false);
    }

    void Update() {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
            return;
        }

        var hits2 = new List<ARRaycastHit>();
        var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        raycastManager.Raycast(screenCenter, hits2, TrackableType.Planes);

        text.text = hits2.Count.ToString();
        if (hits2.Count == 0) {
            text.text = Time.time.ToString();
        }

        if (hits2.Count > 0) {
            //var hitPos = hits2[0].pose;
            
            //placementObj.transform.SetPositionAndRotation(hitPos.position, hitPos.rotation);
            Instantiate(spawObj, hits2[0].pose.position, hits2[0].pose.rotation);
        }



        // ARRaycast raycast = raycastManager.AddRaycast(screenCenter, 15f);
        // if (raycast != null) {
        //     placementObj.transform.position = raycast.plane.transform.position;
        // }
    }

    private void UpdatePlacementPose() {
    }
}
