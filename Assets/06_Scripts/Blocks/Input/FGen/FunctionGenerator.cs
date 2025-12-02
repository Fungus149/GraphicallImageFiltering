using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using JetBrains.Annotations;

public class Fgen : Block, Memory.IClickable {
    public float min = 0;
    public float max = 1;
    public int samples = 10;
    public string function = "2x";

    float[,,] output;
    float[] x;
    float ts = 10;

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
        int max_power = 0;
        function = function.ToLower();
        string[] letters = function.Replace("-", "+-").Split('+');
        int[] pows = new int[letters.Length];
        float[] TEMPcoeff = new float[letters.Length];
        for (int i = 0; i < letters.Length; i++) {
            if (letters[i] != "") {
                //Debug.Log(i);
                int minus = 0;
                if (letters[i][0] == '-') minus = 1;
                if (letters[i].IndexOf("x") == minus) letters[i] = letters[i].Insert(minus, "1");
                if (!letters[i].Contains("x")) { letters[i] += "x^0"; pows[i] = 0; }
                else if (!letters[i].Contains("^")) { letters[i] += "^1"; pows[i] = 1; }
                pows[i] = int.Parse(letters[i].Remove(0, 1 + letters[i].IndexOf("^")));
                try {
                    TEMPcoeff[i] = float.Parse(letters[i].Remove(letters[i].IndexOf("^") - 1, 3));
                }
                catch (Exception) {
                    Error();
                    return;
                }
                if (pows[i] > max_power) max_power = pows[i];
            }
        }
        float[] coeff = new float[max_power + 1];
        for (int i = 0; i < pows.Length; i++) { coeff[pows[i]] = TEMPcoeff[i]; }
        output = new float[samples, 1, 1];
        x = new float[output.GetLength(0)];
        if (samples == 1) { 
            x[0] = min + (max - min) / 2.0f;
            //Debug.Log($"x[{0}] = {x[0]}");
        }
        else {
            x[0] = min;
            ts = (max - min) / ((float)samples - 1);
            for (int i = 1; i < x.Length; i++) {
                x[i] = x[i - 1] + ts;
                //Debug.Log($"x[{i}] = {x[i]}");
            }
        }
        for (int i = 0; i < output.GetLength(0); i++) {
            for (int j = 0; j < coeff.Length; j++)
                output[i, 0, 0] += coeff[j] * Mathf.Pow(x[i], j);
            //Debug.Log($"y[{i}] = {output[i, 0, 0]}");
        }
        nodesOut[0].data = output;
        if (nodesOut[0].isLined) {
            nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
        }
    } // Refresh
    public void Unclick() {
        sr.color = Color.white;
    }
}
