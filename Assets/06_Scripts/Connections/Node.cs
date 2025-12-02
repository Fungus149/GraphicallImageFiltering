using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour,Memory.IClickable {
    //[SerializeField] bool input;
    //public float[] input;
    //public float[] output;
    //public float width = 10f;
    //public int index = 0;
    //public bool lined = false;

    //[SerializeField] bool isInput;

    //GameObject firstNode;
    //GameObject lr;
    //Memory memory;
    //Connection lrConnection;
    //SpriteRenderer sr;
    //private void Start()
    //{
    //    sr = GetComponent<SpriteRenderer>();
    //    memory = Camera.main.GetComponent<Memory>();
    //}
    //private void OnMouseDown() {
    //    firstNode = memory.selected;
    //    //Debug.Log("click" + first_node != null);
    //    //if (first_node == gameObject) memory.selected = null;
    //    if (isInput) {
    //        if (firstNode != null && !firstNode.GetComponent<Node>().isInput && !lined)
    //        {
    //            Debug.Log("Lining");
    //            lined = true;
    //            lr = new GameObject("Line");
    //            lrConnection = lr.AddComponent<Connection>();
    //            lrConnection.second_node = gameObject;
    //            lrConnection.first_node = firstNode;
    //            GetComponentInParent<Block>().source[index] = firstNode.GetComponentInParent<Block>();
    //            firstNode.GetComponentInParent<Block>().destination[index] = GetComponentInParent<Block>();
    //            GetComponentInParent<Block>().Refresh();
    //        }
    //    } // if (input)

    //    if (firstNode == gameObject) {
    //        Unclick();
    //        memory.selected = null;
    //        return;
    //    }
    //    if (firstNode != null) firstNode.GetComponent<Memory.IClickable>().Unclick();
    //    memory.selected = gameObject;
    //    if (isInput) sr.color = new Color32(200, 255, 255, 255);
    //    else sr.color = new Color32(255, 255, 200, 255);
    //}

    //public void Unclick() {
    //    if (isInput) sr.color = UnityEngine.Color.cyan;
    //    else sr.color = UnityEngine.Color.yellow;
    //}

    //public Node connected = null;
    //public int index = 0;
    //public bool isLined = false;
}
