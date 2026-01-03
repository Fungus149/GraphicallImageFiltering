using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class ReadImage : Block, Memory.IClickable {
    public string path = @"Assets\00_Art\SavedImages";

    Texture2D tex;

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
    public override void Refresh() {
        errorBox.gameObject.SetActive(false);
        if (path.Contains('\\') && !System.IO.Directory.Exists(path.Substring(0, path.LastIndexOf('\\')))) {
            Error($"Directory: \\{path.Substring(0, path.LastIndexOf('\\'))} could not be accesed");
            return;
        }

        switch (Path.GetExtension(path)) {
            case ".png":
            case ".jpg":
            case ".jpeg":
                if (System.IO.File.Exists(path)) {
                    tex = new Texture2D(2, 2);
                    tex.LoadImage(System.IO.File.ReadAllBytes(path)); // it rescales automatically
                }
                else {
                    Error($"File: {path} could not be accesed");
                    return;
                }
                break;
            default:
                Error($"Unsupportes file type: {Path.GetExtension(path)}.\nSupported types: .jpeg, .jpg, .png");
                return;
        }
        pixels = tex.GetPixels32();
        textureWidth = tex.width;
        textureHeight = tex.height;
        output = new float[textureWidth, textureHeight, 4];
        for (int i = 0; i < textureWidth; i++) {
            for (int j = textureHeight -1; j >= 0; j--) {
                Color32 tempCol = pixels[(textureHeight - j - 1) * textureWidth + i];

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
}
