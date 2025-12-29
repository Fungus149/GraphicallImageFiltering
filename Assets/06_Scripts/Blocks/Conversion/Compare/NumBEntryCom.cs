using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBEntryCom : MonoBehaviour {
    Compare com;
    private void Start() {
        com = GetComponentInParent<Compare>();
    }
    public void GrabValueFromEntry(string entry) {
        Debug.Log(entry);
        com.numB = float.Parse(entry);
        com.Refresh();
    }
}
