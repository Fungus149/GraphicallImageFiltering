using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Rendering.CameraUI;

public class Arithmetic : Block, Memory.IClickable {
    public string operation = "Addition";
    public float numB = 0; // just a value from entry - it shouldn't be changed in here

    float[,,] inputA;
    float[,,] inputB;
    float[,,] output;

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
            if (nodesIn[1].isLined && nodesIn[1].connected.data != null) inputB = nodesIn[1].connected.data; 
            else inputB = new float[,,] { { { numB } } }; //there is a few back and forth conversions, but, but it's the best way I can think of
            if (inputB.GetLength(0) == 1 && inputB.GetLength(1) == 1 && inputB.GetLength(2) == 1) { // when the second input is a single value
                output = new float[inputA.GetLength(0), inputA.GetLength(1), inputA.GetLength(2)];
                float floatB = inputB[0, 0, 0];
                for (int i = 0; i < inputA.GetLength(0); i++) {
                    for (int j = 0; j < inputA.GetLength(1); j++) {
                        for (int k = 0; k < inputA.GetLength(2); k++) {
                            switch (operation) {
                                case "Addition":
                                    output[i, j, k] = inputA[i, j, k] + floatB;
                                    break;
                                case "Substraction":
                                    output[i, j, k] = inputA[i, j, k] - floatB;
                                    break;
                                case "Multiplication":
                                    output[i, j, k] = inputA[i, j, k] * floatB;
                                    break;
                                case "Division":
                                    output[i, j, k] = inputA[i, j, k] / floatB;
                                    break;
                                case "Modulus":
                                    output[i, j, k] = inputA[i, j, k] % floatB;
                                    break;
                                case "Exponent":
                                    output[i, j, k] = Mathf.Pow(inputA[i, j, k], floatB);
                                    break;
                                default:
                                    output = inputA;
                                    break;
                            } // switch
                        } // k
                    } // j
                } // i
            } // array inputA and float floatB
            else if (inputA.GetLength(0) == inputB.GetLength(0) && inputA.GetLength(1) == inputB.GetLength(1) && (inputA.GetLength(2) == inputB.GetLength(2) || inputA.GetLength(2) == 1 || inputB.GetLength(2) == 1) ) {
                if (inputA.GetLength(2) == 1 && inputB.GetLength(2) != 1) { // allows to multiply an image times a mask
                    float[,,] TEMP = new float[inputA.GetLength(0), inputA.GetLength(1), 1];
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            TEMP[i, j, 0] = inputA[i, j, 0];
                        }
                    }
                    inputA = new float[inputA.GetLength(0), inputA.GetLength(1), inputB.GetLength(2)];
                    for (int i = 0; i < inputA.GetLength(0); i++) {
                        for (int j = 0; j < inputA.GetLength(1); j++) {
                            for (int k = 0; k < inputA.GetLength(2); k++) {
                                inputA[i, j, k] = TEMP[i, j, 0];
                            }
                        }
                    }
                }
                if (inputB.GetLength(2) == 1 && inputA.GetLength(2) != 1) {
                    float[,,] TEMP = new float[inputB.GetLength(0), inputB.GetLength(1), 1];
                    for (int i = 0; i < inputB.GetLength(0); i++) {
                        for (int j = 0; j < inputB.GetLength(1); j++) {
                            TEMP[i, j, 0] = inputB[i, j, 0];
                        }
                    }
                    inputB = new float[inputB.GetLength(0), inputB.GetLength(1), inputA.GetLength(2)];
                    for (int i = 0; i < inputB.GetLength(0); i++) {
                        for (int j = 0; j < inputB.GetLength(1); j++) {
                            for (int k = 0; k < inputB.GetLength(2); k++) {
                                inputB[i, j, k] = TEMP[i, j, 0];
                            }
                        }
                    }
                }
                output = new float[inputA.GetLength(0), inputA.GetLength(1), inputA.GetLength(2)];
                for (int i = 0; i < inputA.GetLength(0); i++) {
                    for (int j = 0; j < inputA.GetLength(1); j++) {
                        for (int k = 0; k < inputA.GetLength(2); k++) {
                            switch (operation) {
                                case "Addition":
                                    output[i, j, k] = inputA[i, j, k] + inputB[i, j, k];
                                    break;
                                case "Substraction":
                                    output[i, j, k] = inputA[i, j, k] - inputB[i, j, k];
                                    break;
                                case "Multiplication":
                                    output[i, j, k] = inputA[i, j, k] * inputB[i, j, k];
                                    break;
                                case "Division":
                                    output[i, j, k] = inputA[i, j, k] / inputB[i, j, k];
                                    break;
                                case "Modulus":
                                    output[i, j, k] = inputA[i, j, k] % inputB[i, j, k];
                                    break;
                                case "Exponent":
                                    output[i, j, k] = Mathf.Pow(inputA[i, j, k], inputB[i, j, k]);
                                    break;
                                default:
                                    output = inputA;
                                    break;
                            } // switch
                        } // k
                    } // j
                } // i
            } // array inputA and array inputB
            else if(inputA.GetLength(0) != inputB.GetLength(0) || inputA.GetLength(1) != inputB.GetLength(1) || inputA.GetLength(2) != inputB.GetLength(2)) {
                Error("unequal array sizes");
                return;
            }
            nodesOut[0].data = output;
            if (nodesOut[0].isLined) {
                nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
            }
        } // if(nodesIn[0].isLined)
    } // Refresh

    public void Unclick() {
        sr.color = Color.white;
    }
}
