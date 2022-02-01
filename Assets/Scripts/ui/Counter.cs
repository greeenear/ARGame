using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {
    [SerializeField]
    private Text count;

    public void SetText(string text) {
        count.text = text;
    }
}
