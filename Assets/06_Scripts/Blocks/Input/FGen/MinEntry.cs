using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinEntry : MonoBehaviour {
    Fgen fg;
    private void Start() {
        fg = GetComponentInParent<Fgen>();
    }
    public void GrabValueFromEntry(string entry) {
        fg.min = float.Parse(entry);
        fg.Refresh();
    }
}
