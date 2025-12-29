using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBEntryThr : MonoBehaviour {
    Threshold thr;
    private void Start() {
        thr = GetComponentInParent<Threshold>();
    }
    public void GrabValueFromEntry(string entry) {
        Debug.Log(entry);
        thr.numB = float.Parse(entry);
        thr.Refresh();
    }
}
