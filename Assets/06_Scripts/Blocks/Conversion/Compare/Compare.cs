using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Compare : Block, Memory.IClickable {
    public string option = "Greater[>]";
    public float numB = 0; // just a value from entry - it shouldn't be changed in here

    float[,,] output;
    float[,,] inputA;
    float inputB;

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
        current = camuwu.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
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
            current = camuwu.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            transform.position = new Vector3(current.x + offset.x, current.y + offset.y, -5);
        }
    }
    public void OnMouseUp() {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
    public override void Refresh() {
        if (nodesIn[0].isLined && nodesIn[0].connected.data != null) { // needed when the source block has no output given yet
            inputA = nodesIn[0].connected.data;
            output = new float[inputA.GetLength(0), inputA.GetLength(1), inputA.GetLength(2)];
            if (nodesIn[1].isLined && nodesIn[1].connected.data != null) inputB = nodesIn[1].connected.data[0, 0, 0];
            else inputB = numB;
            switch (option) {
                case "Greater[>]":
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            for (int k = 0; k < inputA.GetLength(2); k++) {
                                if (inputA[i, j, k] > inputB) output[i, j, k] = 1;
                            }
                        }
                    }
                    break;
                case "Greater or equal[>=]":
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            for (int k = 0; k < inputA.GetLength(2); k++) {
                                if (inputA[i, j, k] >= inputB) output[i, j, k] = 1;
                            }
                        }
                    }
                    break;
                case "Equal[==]":
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            for (int k = 0; k < inputA.GetLength(2); k++) {
                                if (inputA[i, j, k] == inputB) output[i, j, k] = 1;
                            }
                        }
                    }
                    break;
                case "not Equal[!=]":
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            for (int k = 0; k < inputA.GetLength(2); k++) {
                                if (inputA[i, j, k] != inputB) output[i, j, k] = 1;
                            }
                        }
                    }
                    break;
                case "Less or equal[<=]":
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            for (int k = 0; k < inputA.GetLength(2); k++) {
                                if (inputA[i, j, k] <= inputB) output[i, j, k] = 1;
                            }
                        }
                    }
                    break;
                case "Less[<]":
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            for (int k = 0; k < inputA.GetLength(2); k++) {
                                if (inputA[i, j, k] < inputB) output[i, j, k] = 1;
                            }
                        }
                    }
                    break;
                default:
                    output = inputA;
                    break;
            } // switch (option)
            nodesOut[0].data = output;
            if (nodesOut[0].isLined) {
                nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
            }
        } // if (nodesIn[0].isLined && nodesIn[0].connected.data != null)
    } // Refresh

    public void Unclick() {
        sr.color = Color.white;
    }
}
