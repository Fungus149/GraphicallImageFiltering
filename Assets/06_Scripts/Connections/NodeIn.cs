using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;
using UnityEngine.Windows;

public class NodeIn : MonoBehaviour, Memory.IClickable {
    public NodeOut connected = null;
    public bool isLined = false;

    GameObject selected;
    GameObject lr;
    Memory memory;
    Connection lrConnection;
    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        memory = Camera.main.GetComponent<Memory>();
    }

    // Update is called once per frame
    private void OnMouseDown() {
        selected = memory.selected;
        if (!isLined && selected != null && selected.GetComponent<NodeOut>()!=null) {
            Debug.Log("Lining");
            isLined = true;
            selected.GetComponent<NodeOut>().isLined = true;
            lr = new GameObject("Line");
            lrConnection = lr.AddComponent<Connection>();
            lrConnection.secondNode = this;
            lrConnection.firstNode = selected.GetComponent<NodeOut>();
            connected = selected.GetComponent<NodeOut>();
            selected.GetComponent<NodeOut>().connected = this;
            //GetComponentInParent<Block>().source[index] = selected.GetComponentInParent<Block>();
            //selected.GetComponentInParent<Block>().destination[index] = GetComponentInParent<Block>();
            GetComponentInParent<Block>().Refresh();
        }

        if (selected == gameObject) {
            Unclick();
            memory.selected = null;
            return;
        }
        if (selected != null) selected.GetComponent<Memory.IClickable>().Unclick();
        memory.selected = gameObject;
        sr.color = new Color32(200, 255, 255, 255);
    }
    public void Unclick() {sr.color = UnityEngine.Color.cyan;}
}
