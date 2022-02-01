using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
    public Action<string> onKill;

    [SerializeField]
    private ARRaycastManager raycastManager;
    [SerializeField]
    private ObjectPooling objectPooling;

    [SerializeField]
    private Camera mainCamera;

    public LayerMask enemyLayer;

    private int enemyCount;
    private float timer;


    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update() {
        timer += Time.deltaTime;

        var xСoordinates = UnityEngine.Random.Range(0, Screen.width);
        var yСoordinates = UnityEngine.Random.Range(0, Screen.height);
        var screenCenter = new Vector2(xСoordinates, yСoordinates);
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0 && timer > 1) {
            var type = (ObjectType)UnityEngine.Random.Range(0, (int)ObjectType.Count);
            ShowObjectOnPlane(objectPooling.GetObject(type), hits[0].pose);
            timer = 0;
            hits.Clear();
        }

        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;


        var screenPoint = mainCamera.ScreenPointToRay(touch.position);
        var touchRaycast = Physics.Raycast(screenPoint, out RaycastHit hit, 10f, enemyLayer);
        if (hit.transform.gameObject != null) {
            hit.transform.gameObject.SetActive(false);
            onKill.Invoke((enemyCount + 1).ToString());
            enemyCount++;
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
