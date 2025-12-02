using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Connection : MonoBehaviour, Memory.IClickable {
    public NodeOut firstNode;
    public NodeIn secondNode;

    LineRenderer lr;
    MeshCollider collider;
    Mesh mesh;
    public Memory memory;
    void Start() {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.widthMultiplier = 20f;
        lr.endColor = Color.yellow;
        lr.startColor = Color.cyan;
        collider = gameObject.AddComponent<MeshCollider>();
        mesh = new Mesh();
        memory = Camera.main.GetComponent<Memory>();
    }
    void Update() {
        if (firstNode == null || secondNode == null) { Destroy(); return; } //Delete self if source has been deleted
        lr.SetPosition(0, secondNode.transform.position);
        lr.SetPosition(1, firstNode.transform.position);
        lr.BakeMesh(mesh);
        collider.sharedMesh = mesh;
    }

    private void OnMouseDown() {
        if (memory.selected == gameObject) {
            Unclick();
            memory.selected = null;
            return;
        }
        if (memory.selected != null) memory.selected.GetComponent<Memory.IClickable>().Unclick();
        lr.endColor = new Color32(255, 255, 200, 255);
        lr.startColor = new Color32(200, 255, 255, 255);
        memory.selected = gameObject;

    }

    public void Unclick() {
        lr.endColor = Color.yellow;
        lr.startColor = Color.cyan;
    }

    public void Destroy() {
        if (firstNode != null) {
            firstNode.connected = null;
            firstNode.isLined = false;
        } //check for when you removed parent object
        if (secondNode != null) {
            secondNode.connected = null;
            secondNode.isLined = false;
            secondNode.GetComponentInParent<Block>().Refresh();
        }
        Destroy(gameObject);
    }


    
}
