using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBEntryCom : MonoBehaviour {
    Compare com;
    float temp;
    private void Start() {
        com = GetComponentInParent<Compare>();
    }
    public void GrabValueFromEntry(string entry) {
        if (!float.TryParse(entry, out temp)) {
            com.Error("Min can be a float only!");
            return;
        }
        com.numB = temp;
        com.Refresh();
    }
}
