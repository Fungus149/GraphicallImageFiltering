using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Block : MonoBehaviour, Memory.IClickable{
    public bool isMoveLocked;
    public NodeIn[] nodesIn;

    [SerializeField] protected NodeOut[] nodesOut;
    [SerializeField] protected GameObject errorBox;

    [System.NonSerialized] protected Camera camuwu; // NonSerilaized due to Unity bug with inherited variables
    [System.NonSerialized] protected SpriteRenderer sr;
    [System.NonSerialized] protected Vector2 offset;
    [System.NonSerialized] protected Vector2 current;
    [System.NonSerialized] protected Memory memory;

    public void OnMouseDown() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        offset = new Vector2(transform.position.x - current.x, transform.position.y - current.y);
        if (memory.dataDescriptor != null && !isMoveLocked) {
            memory.dataDescriptor.SetActive(false);
        }
        // highlight handler:
        if (memory.selected == gameObject) {
            Unclick();
            memory.selected = null;
            return;
        }
        if (memory.selected != null) {
            memory.selected.GetComponent<Memory.IClickable>().Unclick();
        }
        sr.color = new Color32(255, 200, 255, 255);
        memory.selected = gameObject;
    }
    public void OnMouseDrag() {
        if (isMoveLocked) {
            return;
        }
        Vector2 current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.LeftControl)) {
            transform.position = new Vector3(Mathf.Round((current.x + offset.x )/ 200) * 200, Mathf.Round((current.y + offset.y)/ 120) * 120, -5);
            return;
        }
        transform.position = new Vector3(current.x + offset.x, current.y + offset.y, -5);
    }
    public void OnMouseUp() {
        if (isMoveLocked) {
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    public virtual void Refresh() { // Refreshing must spread. It is always overwritten
        errorBox.gameObject.SetActive(false);
        // do seomething
        foreach (NodeOut destination in nodesOut) { destination.GetComponentInParent<Block>().Refresh(); }
    }

    public virtual void Error(string error_message=null) { // Errors must spread
        if(error_message != null) {
            errorBox.SetActive(true);
            errorBox.GetComponentInChildren<TextMeshProUGUI>().text = error_message;
        }
        //foreach (NodeOut destination in nodesOut) { destination.GetComponentInParent<Block>().Error(); }
    }
    public void Unclick() {
        sr.color = UnityEngine.Color.white;
    }
}
