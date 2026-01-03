using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEntry : MonoBehaviour {
    Fgen fg;
    float temp;
    private void Start() {
        fg = GetComponentInParent<Fgen>();
    }
    public void GrabValueFromEntry(string entry) {
        if (!float.TryParse(entry, out temp)) {
            fg.Error("Upper can be a float only!");
            return;
        }
        fg.max = temp;
        fg.Refresh();
    }
}
