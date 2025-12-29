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
        if (nodesIn[0].isLined && nodesIn[0].connected.data != null) { //needed when the source block has no output given yet
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
                    Error("Wrong amount of channels");
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
        }
        //if (nodesIn[0].isLined && nodesIn[0].connected.data != null) { //needed when the source block has no output given yet
        //    input = nodesIn[0].connected.data;

        //    outputR = new float[input.GetLength(0), input.GetLength(1), 3];
        //    outputG = new float[input.GetLength(0), input.GetLength(1), 3];
        //    outputB = new float[input.GetLength(0), input.GetLength(1), 3];
        //    outputA = new float[input.GetLength(0), input.GetLength(1), 1];

        //    switch (input.GetLength(2)) {
        //        case 1:
        //            for (int i = 0; i < input.GetLength(0); i++) {
        //                for (int j = 0; j < input.GetLength(1); j++) {
        //                    outputR[i, j, 0] = input[i, j, 0];
        //                    outputG[i, j, 1] = input[i, j, 0];
        //                    outputB[i, j, 2] = input[i, j, 0];
        //                    outputA[i, j, 0] = 1;
        //                } // j
        //            } // i
        //            break;
        //        case 2:
        //            for (int i = 0; i < input.GetLength(0); i++) {
        //                for (int j = 0; j < input.GetLength(1); j++) {
        //                    outputR[i, j, 0] = input[i, j, 0];
        //                    outputG[i, j, 1] = input[i, j, 0];
        //                    outputB[i, j, 2] = input[i, j, 0];
        //                    outputA[i, j, 0] = input[i, j, 1];
        //                } // j
        //            } // i
        //            break;
        //        case 3:
        //            for (int i = 0; i < input.GetLength(0); i++) {
        //                for (int j = 0; j < input.GetLength(1); j++) {
        //                    outputR[i, j, 0] = input[i, j, 0];
        //                    outputG[i, j, 1] = input[i, j, 1];
        //                    outputB[i, j, 2] = input[i, j, 2];
        //                    outputA[i, j, 0] = 1;
        //                } // j
        //            } // i
        //            break;
        //        case 4:
        //            for (int i = 0; i < input.GetLength(0); i++) {
        //                for (int j = 0; j < input.GetLength(1); j++) {
        //                    outputR[i, j, 0] = input[i, j, 0];
        //                    outputG[i, j, 1] = input[i, j, 1];
        //                    outputB[i, j, 2] = input[i, j, 2];
        //                    outputA[i, j, 0] = input[i, j, 3];
        //                } // j
        //            } // i
        //            break;
        //        default:
        //            Error("Wrong amount of channels");
        //            return;
        //    }

        //    nodesOut[0].data = outputR;
        //    nodesOut[1].data = outputG;
        //    nodesOut[2].data = outputB;
        //    nodesOut[3].data = outputA;

        //    foreach (var node in nodesOut) {
        //        if (node.isLined) {
        //            node.connected.GetComponentInParent<Block>().Refresh();
        //        }
        //    }
        //}
    } // Refresh
    public void Unclick() {
        sr.color = Color.white;
    }
}
