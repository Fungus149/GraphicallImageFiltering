using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plotter : Block, Memory.IClickable {
    public bool connect = false;

    [SerializeField] GameObject dot;
    GameObject[] dots = new GameObject[0];

    Camera camuwu;
    LineRenderer lr;
    SpriteRenderer sr;
    Vector2 offset;
    Vector2 current;
    Memory memory;
    bool redi=false;

    public void Start() {
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        sr = GetComponent<SpriteRenderer>();
        Refresh();
    }
    public void OnMouseDown() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        offset = new Vector2(transform.position.x - current.x, transform.position.y - current.y);

        if (memory.selected == gameObject) {
            Unclick();
            memory.selected = null;
            return;
        }
        Debug.Log(memory.selected);
        if (memory.selected != null) memory.selected.GetComponent<Memory.IClickable>().Unclick();
        sr.color = new Color32(255, 200, 255, 255);
        memory.selected = gameObject;
    }

    public void OnMouseDrag() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(current.x + offset.x, current.y + offset.y, -1);
    }

    public override void Refresh() {
        redi = false;
        Debug.Log("Refresh");
        if(source != null) {
            if (lr != null) Destroy(gameObject.GetComponent<LineRenderer>());
            Vector2 center=transform.position;
            input = source.output;
            //foreach (double y in input) Debug.Log(y);
            foreach (GameObject dotto in dots) { Destroy(dotto); }
            dots = new GameObject[input.GetLength(0)];
            float maxx = 10;
            float maxy = 10;
            for (int i = 0; i < dots.Length; i++) {
                float x = (float)input[i, 0, 0];
                float y = (float)input[i, 1, 0];
                if (x > maxx) maxx = x;
                if (-x > maxx) maxx = -x;
                if (y > maxy) maxy = y;
                if (-y > maxy) maxy = -y;
            }
            for (int i = 0; i < dots.Length; i++) {
                dots[i] = Instantiate(dot, transform);
                dots[i].transform.position = new Vector3(center.x + 120 / maxx * (float)input[i, 0, 0], center.y + 135 / maxy * (float)input[i, 1, 0], -2);
            }
            if (connect) {
                lr = gameObject.AddComponent<LineRenderer>();
                Debug.Log("Line:" + lr);
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.widthMultiplier = 4f;
                lr.endColor = Color.red;
                lr.startColor = Color.red;
                lr.positionCount = dots.Length;
                Debug.Log(dots.Length);
                redi = true;
            }
        } // if(source != null)
    } // Refresh
    private void Update() {
        if(connect & redi) {
            for (int i = 0; i < dots.Length; i++) {
                //Debug.Log(i); Debug.Log(dots[i].transform.position);
                lr.SetPosition(i, dots[i].transform.position);
            }
        }
    }
    public void Unclick() {
        sr.color = Color.white;
    }
} // class
