using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Connection : MonoBehaviour, Memory.IClickable {
    public Memory memory;
    public NodeOut firstNode;
    public NodeIn secondNode;

    Block visualization;
    LineRenderer lr;
    MeshCollider collider;
    Mesh mesh;
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
        if (firstNode == null || secondNode == null) { //Delete self if source has been deleted
            Destroy(); return; 
        }
        lr.SetPosition(0, secondNode.transform.position);
        lr.SetPosition(1, firstNode.transform.position);
        lr.BakeMesh(mesh);
        collider.sharedMesh = mesh;
    }
    private void OnMouseOver() {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse1) && firstNode.data!=null) {
            StartCoroutine(ShowVisualization());
        }
    }
    private void OnMouseDown() {
        // highlight handler:
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
    IEnumerator ShowVisualization() {
        if (memory.dataDescriptor != null) {
            memory.dataDescriptor.SetActive(false);
            memory.dataDescriptor = null;
            Debug.Log("clearing data");
        }
        if (firstNode.data.GetLength(1) > 1) {
            //visualization = Instantiate(memory.imageDisplay, memory.controlSystem.transform).GetComponent<Block>();
            visualization = memory.imageDisplay.GetComponent<Block>();
        }
        else {
            //visualization = Instantiate(memory.sequenceDisplay, memory.controlSystem.transform).GetComponent<Block>();
            visualization = memory.sequenceDisplay.GetComponent<Block>();
        }
        visualization.gameObject.SetActive(true);
        memory.dataDescriptor = visualization.gameObject;
        visualization.nodesIn[0].connected = firstNode;
        visualization.nodesIn[0].isLined = true;
        visualization.Refresh();
        yield return new WaitForSeconds(.1f);
        //visualization.transform.position = new Vector3((firstNode.transform.position.x - secondNode.transform.position.x) / 2, (firstNode.transform.position.y - secondNode.transform.position.y) / 2, 0);
        visualization.transform.position = Vector3.Lerp(firstNode.transform.position, secondNode.transform.position, 0.5f);
        visualization.isMoveLocked = true;
    }
}
