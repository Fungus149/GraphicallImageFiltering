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
    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        Refresh();
    }
    public override void Refresh() {
        errorBox.gameObject.SetActive(false);

        int max_power = 0;
        function = function.ToLower();
        string[] letters = function.Replace("-", "+-").Split('+');
        int[] pows = new int[letters.Length];
        float[] TEMPcoeff = new float[letters.Length];
        for (int i = 0; i < letters.Length; i++) {
            if (letters[i] != "") {
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
                    Error("polynomial is in a wrong format.\nExpected format: [multiplicand]x^[power]\nex.: 4x^2-5x+7");
                    return;
                }
                if (pows[i] > max_power) { 
                    max_power = pows[i]; 
                }
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
                output[i, 0, 0] += Mathf.Round(coeff[j] * Mathf.Pow(x[i], j) * 1000)/1000;
            //Debug.Log($"y[{i}] = {output[i, 0, 0]}");
        }
        nodesOut[0].data = output;
        if (nodesOut[0].isLined) {
            nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
        }
    } // Refresh
}
