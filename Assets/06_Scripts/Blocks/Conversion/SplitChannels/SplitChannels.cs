using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitChannels : Block, Memory.IClickable {
    public float constant = 0;

    float[,,] input;
    float[,,] outputR = null;
    float[,,] outputG = null;
    float[,,] outputB = null;
    float[,,] outputA = null;
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

        input = nodesIn[0].connected.data;
        outputR = new float[input.GetLength(0), input.GetLength(1), 1];
        outputG = new float[input.GetLength(0), input.GetLength(1), 1];
        outputB = new float[input.GetLength(0), input.GetLength(1), 1];
        outputA = new float[input.GetLength(0), input.GetLength(1), 1];

        switch (input.GetLength(2)) {
            case 1:
                for (int i = 0; i < input.GetLength(0); i++) {
                    for (int j = 0; j < input.GetLength(1); j++) {
                        outputR[i, j, 0] = input[i, j, 0];
                        outputG[i, j, 0] = input[i, j, 0];
                        outputB[i, j, 0] = input[i, j, 0];
                        outputA[i, j, 0] = 1;
                    } // j
                } // i
                break;
            case 2:
                for (int i = 0; i < input.GetLength(0); i++) {
                    for (int j = 0; j < input.GetLength(1); j++) {
                        outputR[i, j, 0] = input[i, j, 0];
                        outputG[i, j, 0] = input[i, j, 0];
                        outputB[i, j, 0] = input[i, j, 0];
                        outputA[i, j, 0] = input[i, j, 1];
                    } // j
                } // i
                break;
            case 3:
                for (int i = 0; i < input.GetLength(0); i++) {
                    for (int j = 0; j < input.GetLength(1); j++) {
                        outputR[i, j, 0] = input[i, j, 0];
                        outputG[i, j, 0] = input[i, j, 1];
                        outputB[i, j, 0] = input[i, j, 2];
                        outputA[i, j, 0] = 1;
                    } // j
                } // i
                break;
            case 4:
                for (int i = 0; i < input.GetLength(0); i++) {
                    for (int j = 0; j < input.GetLength(1); j++) {
                        outputR[i, j, 0] = input[i, j, 0];
                        outputG[i, j, 0] = input[i, j, 1];
                        outputB[i, j, 0] = input[i, j, 2];
                        outputA[i, j, 0] = input[i, j, 3];
                    } // j
                } // i
                break;
            default:
                Error($"{input.GetLength(2)} layers were given.\nMaximally 4 can be split in this block.");
                return;
        }

        nodesOut[0].data = outputR;
        nodesOut[1].data = outputG;
        nodesOut[2].data = outputB;
        nodesOut[3].data = outputA;

        foreach (var node in nodesOut) {
            if (node.isLined) {
                node.connected.GetComponentInParent<Block>().Refresh();
            }
        }
    } // Refresh
}
