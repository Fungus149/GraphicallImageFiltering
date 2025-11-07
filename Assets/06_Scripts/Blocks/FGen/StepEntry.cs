using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEnntry : MonoBehaviour {
    Fgen fg;
    private void Start() {
        fg = GetComponentInParent<Fgen>();
    }
    public void GrabValueFromEntry(string entry) {
        fg.ts = double.Parse(entry);
        fg.Refresh();
    }
}
