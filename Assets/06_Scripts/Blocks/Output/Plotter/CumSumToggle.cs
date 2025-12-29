using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumSumToggle : MonoBehaviour {
    Plotter pt;

    private void Start() {
        pt = GetComponentInParent<Plotter>();
    }
    public void Toggle(bool value) {
        pt.isCumSum = value;
        pt.Refresh();
    }
}
