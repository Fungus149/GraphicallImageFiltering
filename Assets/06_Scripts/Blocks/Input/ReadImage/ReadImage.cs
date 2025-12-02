using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Windows;

public class ReadImage : Block, Memory.IClickable {
    public bool isConnected = false;

    [SerializeField] Sprite sprite;

    Camera camuwu;
    SpriteRenderer sr;
    Vector2 offset;
    Vector2 current;
    Memory memory;

    Color32[] pixels;
    float[,,] output;

    int textureWidth;
    int textureHeight;

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
        pixels = sprite.texture.GetPixels32();
        textureWidth = sprite.texture.width;
        textureHeight = sprite.texture.height;
        output = new float[textureWidth, textureHeight, 4];
        for (int i = 0; i < textureWidth; i++) {
            for (int j = textureHeight - 1; j >= 0; j--) {
                Color32 tempCol = pixels[j * textureWidth + i];
                output[i, j, 0] = tempCol.r / 255f;
                output[i, j, 1] = tempCol.g / 255f;
                output[i, j, 2] = tempCol.b / 255f;
                output[i, j, 3] = tempCol.a / 255f;
            }
        }
        nodesOut[0].data = output;
        if (nodesOut[0].isLined) {
            nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
        }
    } // Refresh
    public void Unclick() {
        sr.color = Color.white;
    }
}
