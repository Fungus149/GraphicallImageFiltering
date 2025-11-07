using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using JetBrains.Annotations;

public class Fgen : Block, Memory.IClickable{
    
    public double min = 0;
    public double max = 10;
    public double ts = 1;
    public string function = "2x";

    SpriteRenderer sr;
    Camera camuwu;
    Memory memory;
    Vector2 current;
    Vector2 offset;

    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        camuwu = Camera.main;
        memory=camuwu.GetComponent<Memory>();
        Refresh();
    }
    public void OnMouseDown() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        offset = new Vector2(transform.position.x - current.x, transform.position.y - current.y);

        if (memory.selected == gameObject)
        {
            Unclick();
            memory.selected = null;
            return;
        }
        if (memory.selected != null) memory.selected.GetComponent<Memory.IClickable>().Unclick();
        sr.color = new Color32(255, 200, 255, 255);
        memory.selected = gameObject;
    }

    public void OnMouseDrag() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(current.x + offset.x, current.y + offset.y, -1);
    }
    public override void Refresh() {
        int max_power = 0;
        function = function.ToLower();
        string[] letters = function.Replace("-", "+-").Split('+');
        //int[] pows = new int[letters.Length];
        //double[] TEMPcoeff = new double[letters.Length];
        //for (int i = 0; i < letters.Length; i++) {
        //    if (letters[i].IndexOf("x") == 0) letters[i] = "1" + letters[i];
        //    if (!letters[i].Contains("x")) { letters[i] += "x^0"; pows[i] = 0; }
        //    else if (!letters[i].Contains("^")) { letters[i] += "^1"; pows[i] = 1; }
        //    pows[i] = int.Parse(letters[i].Remove(0, 1 + letters[i].IndexOf("^")));
        //    TEMPcoeff[i] = double.Parse(letters[i].Remove(letters[i].IndexOf("^") - 1, 3));
        //    if (pows[i] > max_power) max_power = pows[i];
        //}
        int[] pows = new int[letters.Length];
        double[] TEMPcoeff = new double[letters.Length];
        for (int i = 0; i < letters.Length; i++) {
            if (letters[i] != "") {
                int minus = 0;
                if (letters[i][0] == '-') minus = 1;
                if (letters[i].IndexOf("x") == minus) letters[i] = letters[i].Insert(minus, "1");
                if (!letters[i].Contains("x")) { letters[i] += "x^0"; pows[i] = 0; }
                else if (!letters[i].Contains("^")) { letters[i] += "^1"; pows[i] = 1; }
                pows[i] = int.Parse(letters[i].Remove(0, 1 + letters[i].IndexOf("^")));
                TEMPcoeff[i] = double.Parse(letters[i].Remove(letters[i].IndexOf("^") - 1, 3));
                if (pows[i] > max_power) max_power = pows[i];
            }
        }
        double[] coeff = new double[max_power + 1];
        for (int i = 0; i < pows.Length; i++) { coeff[pows[i]] = TEMPcoeff[i]; }
        output = new double[Convert.ToInt16((max - min) / ts) + 1,2,1];
        output[0,0,0] = min;
        for (int i = 1; i < output.GetLength(0); i++) {
            output[i,0,0] = output[i - 1,0,0] + ts;
        }
        for (int i = 0; i < output.GetLength(0); i++) {
            for (int j = 0; j < coeff.Length; j++)
                output[i,1,0] += coeff[j] * Math.Pow(output[i,0,0], j);
        }
        if (destination != null) {
            destination.input = output;
            destination.Refresh();
        }
    } // Refresh
    public void Unclick() {
        sr.color = Color.white;
    }
} // class
