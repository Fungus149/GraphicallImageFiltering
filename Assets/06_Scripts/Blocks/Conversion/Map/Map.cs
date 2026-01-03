using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Map : Block, Memory.IClickable {
    //public float numB = 0; // just a value from entry - it shouldn't be changed in here
    //public float numC = 1; // just a value from entry - it shouldn't be changed in here

    //float[,,] input;
    //float[,,] output;

    //SpriteRenderer sr;
    //Camera camuwu;
    //Memory memory;
    //Vector2 current;
    //Vector2 offset;
    //float upper;
    //float lower;
    //float max;
    //float min;

    //public void Start() {
    //    sr = GetComponent<SpriteRenderer>();
    //    camuwu = Camera.main;
    //    memory = camuwu.GetComponent<Memory>();
    //    Refresh();
    //}
    //public void OnMouseDown() {
    //    current = camuwu.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
    //    offset = new Vector2(transform.position.x - current.x, transform.position.y - current.y);
    //    // highlight handler:
    //    if (memory.selected == gameObject) {
    //        Unclick();
    //        memory.selected = null;
    //        return;
    //    }
    //    if (memory.selected != null) memory.selected.GetComponent<Memory.IClickable>().Unclick();
    //    sr.color = new Color32(255, 200, 255, 255);
    //    memory.selected = gameObject;
    //}

    //public void OnMouseDrag() {
    //    if (!isMoveLocked) {
    //        current = camuwu.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
    //        transform.position = new Vector3(current.x + offset.x, current.y + offset.y, -5);
    //    }
    //}
    //public void OnMouseUp() {
    //    transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    //}
    //public override void Refresh() {
        //if (!nodesIn[0].isLined || nodesIn[0].connected.data == null) { //needed when the source block has no output given yet
        //    return;
        //}
        //input = nodesIn[0].connected.data;
        //if (nodesIn[1].isLined && nodesIn[1].connected.data != null) lower = nodesIn[1].connected.data[0,0,0];
        //else lower = numB;
        //if (nodesIn[2].isLined && nodesIn[2].connected.data != null) upper = nodesIn[2].connected.data[0, 0, 0];
        //else upper = numC;

        //for (int i = 0; i < length; i++)
        //{
        //    for (int j = 0; j < length; j++)
        //    {
        //        for (int k = 0; k < length; k++)
        //        {

        //        }
        //    }
        //}

        //if (float.IsFinite(input[0, 0, 0])) { max = input[0, 0, 0]; } // if first value is inf, default to maxY = 1
        //else { max = 1; }
        //for (int i = 0; i < entries; i++)
        //{
        //    for (int j = 0; j < layers; j++)
        //    { // when you send different discrete sequences on rgba channels.
        //        float y = input[i, 0, j];
        //        if (!float.IsFinite(y)) break;
        //        if (y > max) max = y;
        //        if (-y > max) max = -y;
        //    }
        //}
    //}// Refresh
    //public void Unclick() {
    //    sr.color = Color.white;
    //}
} // class
