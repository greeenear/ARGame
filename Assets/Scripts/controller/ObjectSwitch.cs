using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitch : MonoBehaviour {
    private float timer;

    private void OnEnable() {
        timer = 0;
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer > 2) {
            gameObject.SetActive(false);
        }
    }
}
