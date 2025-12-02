using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEntry : MonoBehaviour {
    Fgen fg;
    private void Start() {
        fg = GetComponentInParent<Fgen>();
    }
    public void GrabValueFromEntry(string entry) {
        fg.max = float.Parse(entry);
        fg.Refresh();
    }
}
