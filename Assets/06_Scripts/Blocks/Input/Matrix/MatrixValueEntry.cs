using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixValueEntry : MonoBehaviour {
    public float value = 0;

    Matrix matrix;
    private void Start() {
        matrix = GetComponentInParent<Matrix>();
    }
    public void GrabValueFromEntry(string entry) {
        if (!float.TryParse(entry, out value)) {
            matrix.Error("Values in matrex can be floats only!");
            return;
        }
        matrix.Refresh();
    }
}
