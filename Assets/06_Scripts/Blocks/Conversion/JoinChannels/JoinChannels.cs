using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

public class JoinChannels : Block, Memory.IClickable {
    float[,,] input;
    float[,,] output;

    int width, lastWidth, height, lastHeight;
    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        Refresh();
    }
    public override void Refresh() {
        errorBox.gameObject.SetActive(false);

        lastWidth = -1;
        lastHeight = -1;

        for (int k = 0; k < 4; k++) {
            if (!nodesIn[k].isLined || nodesIn[k].connected.data == null) { //needed when the source block has no output given yet
                if (k == 3) { // so that default alpha value is 1
                    for (int i = 0; i < width; i++) {
                        for (int j = 0; j < height; j++) {
                            output[i, j, 3] = 1;
                        } // j
                    } // i
                }
                continue;
            }

            input = nodesIn[k].connected.data;
            width = input.GetLength(0);
            height = input.GetLength(1);
            if (lastWidth == -1) { // execute only on a first iteration
                output = new float[width, height, 4];
            }

            else if (lastWidth != width || lastHeight != height) {
                Error($"Layer: {k} has different size than previous layers. All layers must have equal sizes!");
                return;
            }
            switch (input.GetLength(2)) {
                case 1:
                    for (int i = 0; i < width; i++) {
                        for (int j = 0; j < height; j++) {
                            output[i, j, k] = input[i, j, 0];
                        } // j
                    } // i
                    break;
                case 2:
                    if(k == 3) {
                        for (int i = 0; i < width; i++) {
                            for (int j = 0; j < height; j++) {
                                output[i, j, k] = input[i, j, 1];
                            } // j
                        } // i
                        break;
                    }
                    for (int i = 0; i < width; i++) {
                        for (int j = 0; j < height; j++) {
                            output[i, j, k] = input[i, j, 0];
                        } // j
                    } // i
                    break;
                case 3: // same as 4 except for alpha layer
                    if (k == 3) {
                        Error("input a may not  have 3 layers. Only 1[a] or 2[value+a] or 4[rgba]");
                        return;
                    }
                    for (int i = 0; i < width; i++) {
                        for (int j = 0; j < height; j++) {
                            output[i, j, k] = input[i, j, k];
                        } // j
                    } // i
                    break;
                case 4:
                    for (int i = 0; i < width; i++) {
                        for (int j = 0; j < height; j++) {
                            output[i, j, k] = input[i, j, k];
                        } // j
                    } // i
                    break;
                default:
                    Error($"{input.GetLength(2)} layers were given (HOW!!!).\nMaximally 4 joined in this block.");
                    return;
            } // switch
            lastWidth = width;
            lastHeight = height;
        } // k
        if (lastWidth == -1) { // really just check if output is given
            return;
        }

        nodesOut[0].data = output;

        if (nodesOut[0].isLined) {
            nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
        }
    } // Refresh
}
