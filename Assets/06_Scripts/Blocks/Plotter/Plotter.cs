using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Plotter : Block, Memory.IClickable {
    public bool isConnected = false;

    [SerializeField] GameObject[] dot;

    GameObject[] dots = new GameObject[0];

    Camera camuwu;
    LineRenderer lr;
    SpriteRenderer sr;
    Vector2 offset;
    Vector2 current;
    Memory memory;
    float[,,] input;

    int entries;
    int layers;
    int row;

    public void Start() {
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        sr = GetComponent<SpriteRenderer>();
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
        Debug.Log(memory.selected);
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
        foreach (GameObject dotto in dots) { Destroy(dotto); }
        if (lr != null) Destroy(gameObject.GetComponent<LineRenderer>());
        if (nodesIn[0].isLined && nodesIn[0].connected.data != null) { //needed when the source block has no output given yet
            input = nodesIn[0].connected.data;
            Vector2 center = transform.position;
            entries = input.GetLength(0);
            layers = input.GetLength(2);
            dots = new GameObject[entries * layers];
            float maxy = 100f;
            if (layers > 4) { Error(); return; }
            for (int i = 0; i < entries; i++) {
                for (int j = 0; j < layers; j++) { // when you send different discrete sequences for rgba.
                float y = input[i, 0, j];
                if (y > maxy) maxy = y;
                if (-y > maxy) maxy = -y;
                }
            }
            for (int i = 0; i < layers; i++) {
                row = entries * i;
                Debug.Log(row);
                Debug.Log(dot[i].name);
                for (int j = 0; j < entries; j++) {
                    dots[row+j] = Instantiate(dot[i], transform);
                    if (float.IsInfinity(input[j, 0, i]) || float.IsNaN(input[j, 0, i])) dots[row + j].transform.position = new Vector3(center.x + 120 / dots.Length * j, 165, -2); // instantinate an asympthote
                    else dots[row + j].transform.position = new Vector3(center.x - 170 + j * 20, center.y + input[j, 0, i] * 20, -2);
                }
            }
            Trim();
            if (isConnected) {
                lr = gameObject.AddComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.widthMultiplier = 4f;
                lr.endColor = Color.red;
                lr.startColor = Color.red;
                lr.positionCount = dots.Length;
            }
        } // if(source != null)
    } // Refresh
    private void Update() {
        if (isConnected) {
            for (int i = 0; i < dots.Length; i++) {
                lr.SetPosition(i, dots[i].transform.position);
            }
        }
    }
    public void Unclick() {
        sr.color = Color.white;
    }

    void Trim() { // trim plot to the graph borders
        Vector2 center = transform.position;
        float y;
        float x;
        foreach (GameObject dotto in dots) {
            //Debug.Log(dotto.name);
            x = dotto.transform.position.x;
            y = dotto.transform.position.y;
            if (/*y < center.y - 150 ||*/ y > center.y + 150 || x > center.x + 170 || x < center.x - 170) dotto.SetActive(false);
            else dotto.SetActive(true);
        }
    }
} // class
