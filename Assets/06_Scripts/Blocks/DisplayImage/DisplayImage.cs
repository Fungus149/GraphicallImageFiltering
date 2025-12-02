using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

public class DisplayImage : Block, Memory.IClickable {
    public bool isConnected = false;

    [SerializeField] GameObject imageWindow;
    [SerializeField] Sprite sprite;

    Color[] cols;
    Camera camuwu;
    SpriteRenderer sr;
    Texture2D newTexture;
    Vector2 offset;
    Vector2 current;
    Memory memory;

    float[,,] input;

    // Start is called before the first frame update
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
            newTexture = new Texture2D(input.GetLength(0), input.GetLength(1));
            cols = new Color[newTexture.width * newTexture.height];
            switch (input.GetLength(2)) {
                case 1: 
                    for (int i = 0; i < newTexture.width; i++) {
                        for (int j = 0; j < newTexture.height; j++) {
                            cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 0], input[i, j, 0], input[i, j, 0]);
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < newTexture.width; i++) {
                        for (int j = 0; j < newTexture.height; j++) {
                            cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 0], input[i, j, 0], input[i, j, 1]);
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < newTexture.width; i++) {
                        for (int j = 0; j < newTexture.height; j++) {
                            cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 1], input[i, j, 2], 1);
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < newTexture.width; i++) {
                        for (int j = 0; j < newTexture.height; j++) {
                            cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 1], input[i, j, 2], input[i, j, 3]);
                        }
                    }
                    break;
                default: { Error("too many layers of the image"); return; }
            }
            //for (int i = 0; i < newTexture.width; i++) {
            //    for (int j = 0; j < newTexture.height; j++) {
            //        cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 1], input[i, j, 2], input[i, j, 3]);
            //        //newTexture.SetPixel(i, j, new Color(input[i, j, 0], input[i, j, 1], input[i, j, 2], input[i, j, 3]));
            //    }
            //}
            newTexture.SetPixels(cols);
            newTexture.filterMode = FilterMode.Point;
            newTexture.Apply();
            imageWindow.GetComponent<SpriteRenderer>().color = Color.white;
            imageWindow.GetComponent<SpriteRenderer>().sprite = Sprite.Create(newTexture,new Rect(0.0f,0.0f, newTexture.width, newTexture.height),new Vector2(0.5f , 0.5f),100.0f);
            imageWindow.transform.localScale = new Vector3(90f / (float)newTexture.width, 80f / (float)newTexture.height, 1);
        }
    }
    public void Unclick() {
        sr.color = Color.white;
    }
}
