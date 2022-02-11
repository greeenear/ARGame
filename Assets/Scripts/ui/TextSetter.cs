using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour {
    [SerializeField]
    private Text count;
    public Controller controller;
    // private void Start() {
    //     controller.onChangeScore += SetText;
    // }

    public void SetText(string text) {
        count.text = text;
    }
}
