using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Threshold : Block, Memory.IClickable {
    public string option = "Greater[>]";
    public float numB = 0; // just a value from entry - it shouldn't be changed in here

    float[,,] output;
    float[,,] inputA;

    float inputB;
    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        Refresh();
    }
    public override void Refresh() {
        errorBox.gameObject.SetActive(false);
        if (!nodesIn[0].isLined || nodesIn[0].connected.data == null) { //needed when the source block has no output given yet
            return;
        }
        if (nodesIn[1].isLined && nodesIn[1].connected.data != null) {
            if (nodesIn[1].connected.data.GetLength(0) == 1 && nodesIn[1].connected.data.GetLength(1) == 1 && nodesIn[1].connected.data.GetLength(2) == 1) {
                inputB = nodesIn[1].connected.data[0, 0, 0];
            }
            else {
                Error("Threshold takes only an array and a single number");
                return;
            }
        }
        else { 
            inputB = numB; 
        }

        inputA = nodesIn[0].connected.data;
        output = new float[inputA.GetLength(0), inputA.GetLength(1), inputA.GetLength(2)];

        for (int i = 0; i < inputA.GetLength(0); i++) {
            for (int j = 0; j < inputA.GetLength(1); j++) {
                for (int k = 0; k < inputA.GetLength(2); k++) {
                    output[i, j, k] = inputA[i, j, k];
                }
            }
        }
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
    } // Refresh
}