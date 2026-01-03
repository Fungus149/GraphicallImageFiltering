using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBEntry : MonoBehaviour {
    Arithmetic arth;
    float temp;
    private void Start() {
        arth = GetComponentInParent<Arithmetic>();
    }
    public void GrabValueFromEntry(string entry) {
        if (!float.TryParse(entry, out temp)) {
            arth.Error("numB can be a float only!");
            return;
        }
        arth.numB = temp;
        arth.Refresh();
    }
}
