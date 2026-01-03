using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinEntry : MonoBehaviour {
    Fgen fg;
    float temp;
    private void Start() {
        fg = GetComponentInParent<Fgen>();
    }
    public void GrabValueFromEntry(string entry) {
        if (!float.TryParse(entry, out temp)) {
            fg.Error("Lower can be a float only!");
            return;
        }
        fg.min = temp;
        fg.Refresh();
    }
}
