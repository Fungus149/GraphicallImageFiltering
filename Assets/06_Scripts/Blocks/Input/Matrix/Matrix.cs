using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : Block, Memory.IClickable {
    public MatrixValueEntry[,] valueEntries = new MatrixValueEntry[3,3];

    [SerializeField] MatrixValueEntry[] initialEntries;
    [SerializeField] GameObject valueEntry;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject entriesContainer;
    [SerializeField] GameObject stretchboxS;
    [SerializeField] GameObject stretchboxE;

    MatrixValueEntry[,] temp;
    float[,,] output;

    BoxCollider2D bC;
    GameObject newObject;
    float sizeX, sizeY;

    public void Start() {
        sr = bg.GetComponent<SpriteRenderer>();
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                valueEntries[i, j]=initialEntries[i+j*3];
            }
        }
        Refresh();
    }
    public override void Refresh() {
        errorBox.gameObject.SetActive(false);
        output = new float[valueEntries.GetLength(0), valueEntries.GetLength(1), 1];
        for (int i = output.GetLength(0)-1; i >= 0; i--) {
            for (int j = output.GetLength(1)-1; j >= 0; j--) {
                output[i, j, 0] = valueEntries[i,j].value;
            }
        }
        nodesOut[0].data = output;
        if (nodesOut[0].isLined) {
            nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
        }
    }
    public void Rescale(int horizontal = 0, int vertical = 0) {
        if (horizontal < 0) {
            for (int k = 0; k < valueEntries.GetLength(1); k++) {
                Destroy(valueEntries[valueEntries.GetLength(0) - 1, k].gameObject);
            }
        }
        if (vertical < 0) {
            for (int k = 0; k < valueEntries.GetLength(0); k++) {
                Destroy(valueEntries[k, valueEntries.GetLength(1) - 1].gameObject);
            }
        }
        temp = valueEntries;
        valueEntries = new MatrixValueEntry[valueEntries.GetLength(0) + horizontal, valueEntries.GetLength(1) + vertical];
        sizeX = Mathf.Min(temp.GetLength(0), valueEntries.GetLength(0));
        sizeY = Mathf.Min(temp.GetLength(1), valueEntries.GetLength(1));
        for (int i = 0; i < sizeX; i++) {
            for (int j = 0; j < sizeY; j++) {
                valueEntries[i, j] = temp[i, j];
            }
        }
        if (horizontal > 0) {
            for (int k = 0; k < valueEntries.GetLength(1); k++) {
                newObject = Instantiate(valueEntry, entriesContainer.transform);
                newObject.transform.position = new Vector3(transform.position.x + 60 * (valueEntries.GetLength(0) - 2), transform.position.y - 15 - 30 * (k - 1), transform.position.z+1);
                valueEntries[valueEntries.GetLength(0) - 1, k] = newObject.GetComponent<MatrixValueEntry>();
            }
        }
        if (vertical > 0) {
            for (int k = 0; k < valueEntries.GetLength(0); k++) {
                newObject = Instantiate(valueEntry, entriesContainer.transform);
                newObject.transform.position = new Vector3(transform.position.x + 60 * (k - 1), transform.position.y - 15 - 30 * (valueEntries.GetLength(1) - 2), transform.position.z+1);
                valueEntries[k, valueEntries.GetLength(1) - 1] = newObject.GetComponent<MatrixValueEntry>();
            }
        }
        // adjusting all visual elements:
        bg.transform.localScale = new Vector2(bg.transform.localScale.x + horizontal * 60f, bg.transform.localScale.y + vertical * 30f);
        bg.transform.position = new Vector2(bg.transform.position.x + horizontal * 30f, bg.transform.position.y + vertical * -15f);
        bC = gameObject.GetComponent<BoxCollider2D>();
        bC.size = new Vector2(bC.size.x + horizontal * 0.3f, bC.size.y + vertical * 0.25f);
        bC.offset = new Vector2(bC.offset.x + horizontal * 0.15f, bC.offset.y + vertical * -0.125f);

        stretchboxS.transform.localScale = new Vector2(stretchboxS.transform.localScale.x + horizontal * 60f, stretchboxS.transform.localScale.y);
        stretchboxS.transform.position = new Vector3(stretchboxS.transform.position.x + horizontal * 30f, stretchboxS.transform.position.y, stretchboxS.transform.position.z);

        stretchboxE.transform.localScale = new Vector2(stretchboxE.transform.localScale.x, stretchboxE.transform.localScale.y + vertical * 30f);
        stretchboxE.transform.position = new Vector3(stretchboxE.transform.position.x, stretchboxE.transform.position.y + vertical * -15f, stretchboxE.transform.position.z);

        nodesOut[0].gameObject.transform.position = new Vector2(nodesOut[0].gameObject.transform.position.x + horizontal * 60, nodesOut[0].gameObject.transform.position.y);
        
        errorBox.transform.position = new Vector2(errorBox.transform.position.x + horizontal * 60, errorBox.transform.position.y);
        
        Refresh();
    }
}
