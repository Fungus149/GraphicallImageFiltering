using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEntryRi : MonoBehaviour {
    ReadImage ri;
    private void Start() {
        ri = GetComponentInParent<ReadImage>();
    }
    public void GrabValueFromEntry(string entry) {
        ri.path = entry.Trim('\"');
        ri.Refresh();
    }
}
