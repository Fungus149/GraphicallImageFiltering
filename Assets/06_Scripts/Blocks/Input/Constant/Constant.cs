using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : Block, Memory.IClickable {
    public float constant = 0;

    SpriteRenderer sr;
    Camera camuwu;
    Memory memory;
    Vector2 current;
    Vector2 offset;

    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        Refresh();
    }
    public void OnMouseDown() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        offset = new Vector2(transform.position.x - current.x, transform.position.y - current.y);

        // highlight handler:
        if (memory.selected == gameObject) {
            Unclick();
            memory.selected = null;
            return;
        }
        if (memory.selected != null) memory.selected.GetComponent<Memory.IClickable>().Unclick();
        sr.color = new Color32(255, 200, 255, 255);
        memory.selected = gameObject;
    }

    public void OnMouseDrag() {
        if (!isMoveLocked) {
            current = camuwu.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(current.x + offset.x, current.y + offset.y, -5);
        }
    }
    public void OnMouseUp() {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
    public override void Refresh() {
        nodesOut[0].data = new float[,,] { { { constant } } };
        if (nodesOut[0].isLined) {
            nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
        }
    } // Refresh
    public void Unclick() {
        sr.color = Color.white;
    }
}
