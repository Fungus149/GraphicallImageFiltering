using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantEntry : MonoBehaviour {
    Constant c;
    private void Start() {
        c = GetComponentInParent<Constant>();
    }
    public void GrabValueFromEntry(string entry) {
        c.constant = float.Parse(entry);
        c.Refresh();
    }
}
