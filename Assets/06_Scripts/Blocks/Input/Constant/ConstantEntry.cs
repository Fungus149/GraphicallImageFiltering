using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantEntry : MonoBehaviour {
    Constant c;
    float temp;
    private void Start() {
        c = GetComponentInParent<Constant>();
    }
    public void GrabValueFromEntry(string entry) {
        if (!float.TryParse(entry, out temp)) {
            c.Error("Min can be a float only!");
            return;
        }
        c.constant = temp;
        c.Refresh();
    }
}
