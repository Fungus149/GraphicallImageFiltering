using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEnntry : MonoBehaviour {
    Fgen fg;
    int temp;
    private void Start() {
        fg = GetComponentInParent<Fgen>();
    }
    public void GrabValueFromEntry(string entry) {
        if(!int.TryParse(entry, out temp)){
            fg.Error("Step can be an int only!");
            return;
        }
        fg.samples = temp;
        fg.Refresh();
    }
}
