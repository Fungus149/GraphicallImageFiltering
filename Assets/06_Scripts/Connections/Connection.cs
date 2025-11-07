using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Connection : MonoBehaviour, Memory.IClickable {
    public GameObject first_node;
    public GameObject second_node;

    //[SerializeField] GameObject highlight;

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
        if (first_node == null || second_node == null) { Destroy(); return; } //Delete self if source has been deleted
        lr.SetPosition(0, second_node.transform.position);
        lr.SetPosition(1, first_node.transform.position);
        lr.BakeMesh(mesh);
        collider.sharedMesh = mesh;
    }

    private void OnMouseDown() {
        if (memory.selected == gameObject)
        {
            Unclick();
            memory.selected = null;
            return;
        }
        if (memory.selected != null) memory.selected.GetComponent<Memory.IClickable>().Unclick();
        lr.endColor = new Color32(255, 255, 200, 255);
        lr.startColor = new Color32(200, 255, 255, 255);
        memory.selected = gameObject;

    }

    public void Unclick()
    {
        lr.endColor = Color.yellow;
        lr.startColor = Color.cyan;
    }

    public void Destroy() {
        if (first_node != null) { first_node.GetComponentInParent<Block>().destination = null; } //check for when you removed parent object
        if (second_node != null) {
            second_node.GetComponentInParent<Block>().source = null;
            second_node.GetComponentInParent<Block>().Refresh();
            second_node.GetComponent<Node>().lined = false;
        }
        Destroy(gameObject);
    }


    
}
