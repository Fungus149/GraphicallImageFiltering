using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class NodeOut : MonoBehaviour, Memory.IClickable {
    public float[,,] data;
    public NodeIn connected = null;
    public int index = 0;
    public bool isLined = false;

    GameObject selected;
    Memory memory;
    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        memory = Camera.main.GetComponent<Memory>();
    }

    private void OnMouseDown() {
        selected = memory.selected;
        if (selected == gameObject) {
            Unclick();
            memory.selected = null;
            return;
        }
        if (selected != null) selected.GetComponent<Memory.IClickable>().Unclick();
        memory.selected = gameObject;
        sr.color = new Color32(255, 255, 200, 255);
    }
    public void Unclick() {
        sr.color = UnityEngine.Color.yellow;
    }
}
