using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {
    [SerializeField]
    private Text count;
    public Controller controller;
    private void Start() {
        controller.onKill += SetText;
    }

    public void SetText(string text) {
        count.text = text;
    }
}
