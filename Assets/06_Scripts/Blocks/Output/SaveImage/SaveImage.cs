using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveImage : Block, Memory.IClickable {
    public string path = @"C:\Users\gg149\Downloads\new.png";

    Camera camuwu;
    SpriteRenderer sr;
    Texture2D newTexture;
    Vector2 offset;
    Vector2 current;
    Memory memory;

    Color[] cols;
    string[] imageExtensions = new string[] { ".jepg", ".jpg",".png",".exr",".tga" };
    string[] textExtensions = new string[] { ".txt"};
    float[,,] input;
    byte[] rawData;
    string type;

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
        if (memory.selected == gameObject)
        {
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
        type = null;
        Debug.Log(path);
        if (nodesIn[0].isLined && nodesIn[0].connected.data != null) { //needed when the source block has no output given yet
            foreach (string extension in imageExtensions)
                if (extension == Path.GetExtension(path)) type = "image";
            foreach (string extension in textExtensions)
                if (extension == Path.GetExtension(path)) type = "text";
            
            input = nodesIn[0].connected.data;
            switch (type) {
                case("image"):
                    newTexture = new Texture2D(input.GetLength(0), input.GetLength(1));
                    textureHeight = newTexture.height;
                    textureWidth = newTexture.width;

                    cols = new Color[textureWidth * textureHeight];
                    switch (input.GetLength(2))
                    {
                        case 1:
                            for (int i = 0; i < newTexture.width; i++)
                            {
                                for (int j = 0; j < newTexture.height; j++)
                                {
                                    cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 0], input[i, j, 0], 1);
                                }
                            }
                            break;
                        case 2:
                            for (int i = 0; i < newTexture.width; i++)
                            {
                                for (int j = 0; j < newTexture.height; j++)
                                {
                                    cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 0], input[i, j, 0], input[i, j, 1]);
                                }
                            }
                            break;
                        case 3:
                            for (int i = 0; i < newTexture.width; i++)
                            {
                                for (int j = 0; j < newTexture.height; j++)
                                {
                                    cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 1], input[i, j, 2], 1);
                                }
                            }
                            break;
                        case 4:
                            for (int i = 0; i < newTexture.width; i++)
                            {
                                for (int j = 0; j < newTexture.height; j++)
                                {
                                    cols[j * newTexture.width + i] = new Color(input[i, j, 0], input[i, j, 1], input[i, j, 2], input[i, j, 3]);
                                }
                            }
                            break;
                        default:
                            Error("too many layers of the image");
                            return;
                    } // switch (input.GetLength(2))
                    newTexture.SetPixels(cols);
                    newTexture.Apply();
                    switch (Path.GetExtension(path))
                    {
                        case ".png":
                            rawData = ImageConversion.EncodeToPNG(newTexture);
                            break;
                        case ".jpg":
                        case ".jpeg":
                            rawData = ImageConversion.EncodeToJPG(newTexture);
                            break;
                        case ".exr":
                            rawData = ImageConversion.EncodeToEXR(newTexture);
                            break;
                        case ".tga":
                            rawData = ImageConversion.EncodeToTGA(newTexture);
                            break;
                        case ".txt":
                            rawData = newTexture.GetRawTextureData();
                            break;
                        default:
                            Error("Unsupportes file type");
                            return;
                    } // switch
                    break;
                case("text"):
                    break;
                default:
                    Error("Unsupportes file type");
                    return;
            }
            if (!File.Exists(path)) File.Create(path);
            File.WriteAllBytes(path, rawData);
        } // if (nodesIn[0].isLined && nodesIn[0].connected.data != null)
    } // Refresh
    public void Unclick() {
        sr.color = Color.white;
    }
}
