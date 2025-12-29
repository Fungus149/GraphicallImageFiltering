using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

public class JoinChannels : Block, Memory.IClickable {

    SpriteRenderer sr;
    Camera camuwu;
    Memory memory;
    Vector2 current;
    Vector2 offset;

    float[,,] input;
    float[,,] output;
    int width, lastWidth, height, lastHeight;
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
        lastWidth = -1;
        lastHeight = -1;
        for (int k = 0; k < 4; k++) {
            if (nodesIn[k].isLined && nodesIn[k].connected.data != null) { //needed when the source block has no output given yet
                input = nodesIn[k].connected.data;
                width = input.GetLength(0);
                height = input.GetLength(1);
                if (lastWidth == -1) { output = new float[width, height, 4]; }
                else if (lastWidth != width || lastWidth != width) {
                    Error("different layer sizes");
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
                    case 3: // same as 4 except for alpha layer
                        if (k<4) {
                            for (int i = 0; i < width; i++) {
                                for (int j = 0; j < height; j++) {
                                    output[i, j, k] = input[i, j, k];
                                } // j
                            } // i
                        }
                        else {
                            Error("Wrong amount of channels");
                            return;
                        }
                        break;
                    case 4:
                        for (int i = 0; i < width; i++) {
                            for (int j = 0; j < height; j++) {
                                output[i, j, k] = input[i, j, k];
                            } // j
                        } // i
                        break;
                    default:
                        Error("Wrong amount of channels");
                        return;
                } // switch
                lastWidth = width;
                lastHeight = height;
            } // if (nodesIn[k].isLined && nodesIn[k].connected.data != null)
            else if(k==3) { // so that default alpha value is 1
                Debug.Log("Defaulting alpha");
                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        output[i, j, 3] = 1;
                    } // j
                } // i
            }
        } // k
        if (lastWidth!=-1) { // really just check if output is given
            nodesOut[0].data = output;
            if (nodesOut[0].isLined) {
                nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
            }
        }
    } // Refresh
    public void Unclick() {
        sr.color = Color.white;
    }
}
