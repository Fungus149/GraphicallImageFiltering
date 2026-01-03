using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBEntryThr : MonoBehaviour {
    Threshold thr;
    float temp;
    private void Start() {
        thr = GetComponentInParent<Threshold>();
    }
    public void GrabValueFromEntry(string entry) {
        if (!float.TryParse(entry, out temp)) {
            thr.Error("Min can be a float only!");
            return;
        }
        thr.numB = temp;
        thr.Refresh();
    }
}
