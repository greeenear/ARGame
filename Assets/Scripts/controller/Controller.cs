using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
public class Controller : MonoBehaviour {
    public TriangulationTest triangulationTest;
    public Action<string> onChangeScore;

    [SerializeField]
    private ARRaycastManager raycastManager;
    [SerializeField]
    private ObjectPooling objectPooling;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask enemyLayer;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update() {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;
        raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon);
        if (hits.Count > 0) {
            var type = (ObjectType)UnityEngine.Random.Range(0, (int)ObjectType.Count);
            var obj = objectPooling.GetObject(type);
            ShowObjectOnPlane(obj, hits[0].pose);
            triangulationTest.points.Add(obj);
            hits.Clear();
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

    public void Build() {
        triangulationTest.needRender = true;
    }
}
