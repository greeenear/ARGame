using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
    [SerializeField]
    private ARRaycastManager raycastManager;
    [SerializeField]
    private ObjectPooling objectPooling;
    public Action onKill;

    private float timer;

    [SerializeField]
    private Text text;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update() {
        timer += Time.deltaTime;
        var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0 && timer > 2) {
            var type = (ObjectType)UnityEngine.Random.Range(0, (int)ObjectType.Count);
            ShowObjectOnPlane(objectPooling.GetObject(type), hits[0].pose);
            timer = 0;
            hits.Clear();
        }

        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

        var screenPoint = Camera.main.ScreenPointToRay(touch.position);
        var touchRaycast = Physics.Raycast(screenPoint, out RaycastHit hit);

        if (hit.transform.gameObject != null) {
            text.text += 1;
        }
    }

    private void ShowObjectOnPlane(GameObject obj, Pose point) {
        if (obj == null) {
            Debug.LogError("SpawnPrefab: Prefab is null");
            return;
        }

        obj.transform.position = point.position;
        obj.transform.rotation = point.rotation;
        obj.SetActive(true);
    }
}
