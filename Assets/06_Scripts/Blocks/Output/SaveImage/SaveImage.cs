using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveImage : Block, Memory.IClickable {
    public string path = @"Assets\00_Art\SavedImages\new.png";

    Color[] cols;
    string[] imageExtensions = new string[] { ".jpeg", ".jpg",".png",".exr",".tga" };
    string[] textExtensions = new string[] { ".txt"};
    float[,,] input;
    byte[] rawData;

    Texture2D newTexture;
    string type;
    int textureWidth;
    int textureHeight;
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
        if (path.Contains('\\') && !Directory.Exists(path.Substring(0, path.LastIndexOf('\\')))) {
            Error($"Directory: \\{path.Substring(0, path.LastIndexOf('\\'))} could not be accesed");
            return;
        }

        input = nodesIn[0].connected.data;

        type = null;
        foreach (string extension in imageExtensions) {
            if (extension == Path.GetExtension(path)) type = "image";
        }
        foreach (string extension in textExtensions) {
            if (extension == Path.GetExtension(path)) type = "text";
        }
        switch (type) {
            case("image"):
                newTexture = new Texture2D(input.GetLength(0), input.GetLength(1));
                textureHeight = newTexture.height;
                textureWidth = newTexture.width;
                cols = new Color[textureWidth * textureHeight];
                switch (input.GetLength(2)) {
                    case 1:
                        for (int i = 0; i < textureWidth; i++) {
                            for (int j = 1; j <= textureHeight; j++) {
                                Debug.Log("j = " + j);
                                cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0], input[i, textureHeight - j, 0], input[i, textureHeight - j, 0], 1);
                            }
                        }
                        break;
                    case 2:
                        for (int i = 0; i < textureWidth; i++) {
                            for (int j = 1; j <= textureHeight; j++) {
                                cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0], input[i, textureHeight - j, 0], input[i, textureHeight - j, 0], input[i, textureHeight - j, 1]);
                            }
                        }
                        break;
                    case 3:
                        for (int i = 0; i < textureWidth; i++) {
                            for (int j = 1; j <= textureHeight; j++) {
                                cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0], input[i, textureHeight - j, 1], input[i, textureHeight - j, 2], 1);
                            }
                        }
                        break;
                    case 4:
                        for (int i = 0; i < textureWidth; i++) {
                            for (int j = 1; j <= textureHeight; j++) {
                                cols[(j - 1) * textureWidth + i] = new Color(input[i, textureHeight - j, 0], input[i, textureHeight - j, 1], input[i, textureHeight - j, 2], input[i, textureHeight - j, 3]);
                            }
                        }
                        break;
                    default:
                        Error($"{input.GetLength(2)} layers were given.\nOnly 1 to 4 can be displayed");
                        return;
                } // switch (input.GetLength(2))
                newTexture.SetPixels(cols);
                newTexture.Apply();
                switch (Path.GetExtension(path)) {
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
                    default:
                        Error("Unsupportes image type: "+ Path.GetExtension(path));
                        return;
                } // switch
                break;
            case("text"):
                switch (Path.GetExtension(path)) {
                    case ".txt":
                        rawData = newTexture.GetRawTextureData();
                        break;
                    default:
                        Error("Unsupportes text type: " + Path.GetExtension(path));
                        return;
                } // switch
                break;
            default:
                Error($"Unsupportes file type: {Path.GetExtension(path)}.\nSupported types: .jpeg, .jpg, .png, .exr, .tga, .txt");
                return;
        }
        if (!File.Exists(path)) { 
            File.Create(path).Dispose();
        }
        File.WriteAllBytes(path, rawData);
    } // Refresh
}
