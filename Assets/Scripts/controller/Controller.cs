
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Events;

public class Controller : MonoBehaviour {
    public Action<string> onChangeScore;

    [SerializeField]
    private ARRaycastManager raycastManager;
    [SerializeField]
    private ObjectPooling objectPooling;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private SceneLoader loader;

    private int enemyCount;
    private float spawnTimer;
    private float gameTimer;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update() {
        gameTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (gameTimer > 60) {
            loader.Load("MainMenu");
        }

        var xRandСoordinates = UnityEngine.Random.Range(0, Screen.width);
        var yRandСoordinates = UnityEngine.Random.Range(0, Screen.height);
        var screenCenter = new Vector2(xRandСoordinates, yRandСoordinates);

        raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
        if (hits.Count > 0 && spawnTimer > 3) {
            var type = (ObjectType)UnityEngine.Random.Range(0, (int)ObjectType.Count);
            ShowObjectOnPlane(objectPooling.GetObject(type), hits[0].pose);
            spawnTimer = 0;
            hits.Clear();
        }

        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

        var screenPoint = mainCamera.ScreenPointToRay(touch.position);
        if (Physics.Raycast(screenPoint, out RaycastHit hit, 10f, enemyLayer)) {
            if (!hit.transform.gameObject.TryGetComponent(out EnemyController enemy)) return;
            if (enemy.state == State.Die) return;

            if (enemy.state == State.Block) {
                enemyCount = Mathf.Clamp((enemyCount - 1), 0, int.MaxValue);
                onChangeScore.Invoke(enemyCount.ToString());
                return;
            }

            onChangeScore.Invoke((enemyCount++ + 1).ToString());
            enemy.animator.SetBool("isDie", true);
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

    public void RestartGame() {
        this.enabled = true;
        onChangeScore.Invoke((0).ToString());
        gameTimer = 0;
    }
}
