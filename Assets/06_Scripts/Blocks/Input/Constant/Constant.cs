using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : Block, Memory.IClickable {
    public float constant = 0;
    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        camuwu = Camera.main;
        memory = camuwu.GetComponent<Memory>();
        Refresh();
    }
    public override void Refresh() {
        errorBox.gameObject.SetActive(false);
        nodesOut[0].data = new float[,,] { { { constant } } };
        if (nodesOut[0].isLined) {
            nodesOut[0].connected.GetComponentInParent<Block>().Refresh();
        }
    } // Refresh
}
