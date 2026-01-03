using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Rendering.CameraUI;

public class DisplayImage : Block, Memory.IClickable {
    public bool isConnected = false;

    [SerializeField] GameObject imageWindow;
    [SerializeField] Sprite sprite;

    Color[] cols;
    float[,,] input;
    byte[] inputType;

    Texture2D newTexture;
    int textureWidth, textureHeight;
    public virtual void Start() {
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
        inputType = nodesIn[0].connected.type;
        newTexture = new Texture2D(input.GetLength(0), input.GetLength(1));
        textureWidth = newTexture.width;
        textureHeight = newTexture.height;
        cols = new Color[textureWidth * textureHeight];
        switch (input.GetLength(2)) {
            case 1:
                for (int i = 0; i < textureWidth; i++)
                {
                    for (int j = 1; j <= textureHeight; j++)
                    {
                        Debug.Log("j = " + j);
                        cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0] * inputType[0], input[i, textureHeight - j, 0] * inputType[1], input[i, textureHeight - j, 0] * inputType[2], 1);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < textureWidth; i++)
                {
                    for (int j = 1; j <= textureHeight; j++)
                    {
                        cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0] * inputType[0], input[i, textureHeight - j, 0] * inputType[1], input[i, textureHeight - j, 0] * inputType[2], input[i, textureHeight - j, 1]);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < textureWidth; i++)
                {
                    for (int j = 1; j <= textureHeight; j++)
                    {
                        cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0], input[i, textureHeight - j, 1], input[i, textureHeight - j, 2], 1);
                    }
                }
                break;
            case 4:
                for (int i = 0; i < textureWidth; i++)
                {
                    for (int j = 1; j <= textureHeight; j++)
                    {
                        cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0], input[i, textureHeight - j, 1], input[i, textureHeight - j, 2], input[i, textureHeight - j, 3]);
                    }
                }
                break;
            default:
                Error($"{input.GetLength(2)} layers were given.\nOnly 1 to 4 can be displayed");
                return;
        } // switch (input.GetLength(2))
        newTexture.SetPixels(cols);
        newTexture.filterMode = FilterMode.Point;
        newTexture.Apply();
        imageWindow.GetComponent<SpriteRenderer>().color = Color.white;
        imageWindow.GetComponent<SpriteRenderer>().sprite = Sprite.Create(newTexture,new Rect(0.0f,0.0f, newTexture.width, newTexture.height),new Vector2(0.5f , 0.5f),100.0f);
        imageWindow.transform.localScale = new Vector3(90f / (float)newTexture.width, 80f / (float)newTexture.height, 1);
    } // Refresh
}
