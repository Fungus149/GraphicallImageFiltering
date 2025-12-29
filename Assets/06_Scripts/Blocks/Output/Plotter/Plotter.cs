using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Plotter : Block, Memory.IClickable {
    public Vector2 plotOffset = new Vector2(0,0);
    public bool isCumSum = false;
    public float scale = 20;

    [SerializeField] GameObject[] dot;
    [SerializeField] GameObject[] asymptote;
    [SerializeField] GameObject dotsContainer;
    [SerializeField] GameObject asymptotesContainer;
    [SerializeField] GameObject xAxis;

    GameObject[] dots = new GameObject[0];
    float[,,] input;

    Camera camuwu;
    SpriteRenderer sr;
    Vector2 offset;
    Vector2 current;
    Vector2 center; // plot center
    Memory memory;
    float maxY;
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
        if (!nodesIn[0].isLined || nodesIn[0].connected.data == null) { //needed when the source block has no output given yet
            return;
        }
        input = nodesIn[0].connected.data;
        center = transform.position;
        entries = input.GetLength(0);
        layers = input.GetLength(2);
        dots = new GameObject[entries * layers];
        if (float.IsFinite(input[0, 0, 0])) { maxY = input[0, 0, 0]; } // if first value is inf, default to maxY = 1
        else { maxY = 1; }
        if (layers > 4) { Error(); return; }
        for (int i = 0; i < entries; i++) {
            for (int j = 0; j < layers; j++) { // when you send different discrete sequences on rgba channels.
                float y = input[i, 0, j];
                if (!float.IsFinite(y)) break;
                if (y > maxY) maxY = y;
                if (-y > maxY) maxY = -y;
            }
        }
        if (maxY * 340f < entries * 150f) {
            scale = 340 / (entries - 1);
        }
        else { 
            scale = 150 / maxY;
        }
        if (scale < 1) { scale = 1; } // limiting scale
        Debug.Log($"scale = {scale}");
        for (int i = 0; i < layers; i++) {
            row = entries * i;
            for (int j = 0; j < entries; j++) {
                if (float.IsInfinity(input[j, 0, i]) || float.IsNaN(input[j, 0, i])) { // instantinate an asympthote
                    dots[row + j] = Instantiate(asymptote[i], asymptotesContainer.transform);
                    dots[row + j].transform.position = new Vector3(plotOffset.x + center.x - 170 + j * scale, center.y, -2);
                }
                else { // instantinate a dot
                    dots[row + j] = Instantiate(dot[i], dotsContainer.transform);
                    dots[row + j].GetComponent<Dots>().x = j;
                    dots[row + j].GetComponent<Dots>().y = input[j, 0, i];
                    dots[row + j].transform.position = new Vector3(plotOffset.x + center.x - 170 + j * scale, plotOffset.y + center.y + input[j, 0, i] * scale, -2);
                }
            } // j
        } // i
        Trim();
    } // Refresh
    public void Rescale(float scroll) {
        Debug.Log($"plotOffset x = {plotOffset.x} \nplotOffset y = {plotOffset.y}");
        scale += scroll;
        if (dots.Length == 0) return; // if block didn't refresh yet
        if (scale <= 0) { scale = 0.001f; } // limiting scale
        input = nodesIn[0].connected.data;
        layers = input.GetLength(2);
        entries = input.GetLength(0);
        center = transform.position;
        for (int i = 0; i < layers; i++) {
            row = entries * i;
            for (int j = 0; j < entries; j++) {
                if (dots[row + j].GetComponent<Dots>() == null) { // rescale asymptote
                    dots[row + j].transform.position = new Vector3(plotOffset.x + center.x - 170 + j * scale, dots[row + j].transform.position.y, -2);
                }
                else { // rescale dot
                    dots[row + j].transform.position = new Vector3(plotOffset.x + center.x - 170 + j * scale, plotOffset.y + center.y + input[j, 0, i] * scale, -2);
                }
            } // j
        } // i
        Trim();
    } // Rescale
    public void Trim() { // trim plot to the graph borders
        center = transform.position;
        float y;
        float x;

        y = xAxis.transform.position.y;
        if (y < center.y - 155 || y > center.y + 155) {
            xAxis.SetActive(false);
        }
        else {
            xAxis.SetActive(true);
        }
        if (dots.Length == 0) return; // if block didn't refresh yet
        foreach (GameObject dotto in dots) {
            x = dotto.transform.position.x;
            y = dotto.transform.position.y;
            if (y < center.y - 155 || y > center.y + 155 || x > center.x + 175 || x < center.x - 175) {
                dotto.SetActive(false);
            }
            else {
                dotto.SetActive(true);
            }
        }
    } // Trim
    public void Unclick() {
        sr.color = Color.white;
    }
} // class
