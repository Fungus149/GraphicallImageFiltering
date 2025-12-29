using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBEntry : MonoBehaviour {
    Arithmetic arth;
    private void Start() {
        arth = GetComponentInParent<Arithmetic>();
    }
    public void GrabValueFromEntry(string entry) {
        Debug.Log(entry);
        arth.numB = float.Parse(entry);
        arth.Refresh();
    }
}
