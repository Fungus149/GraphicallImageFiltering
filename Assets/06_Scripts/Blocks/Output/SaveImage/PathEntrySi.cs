using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEntrySi : MonoBehaviour {
    SaveImage si;
    private void Start() {
        si = GetComponentInParent<SaveImage>();
    }
    public void GrabValueFromEntry(string entry) {
        si.path = entry.Trim('\"');
        si.Refresh();
    }
}
