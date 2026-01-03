using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDataVisualization : MonoBehaviour {
    public void CloseVisualization() {
        transform.parent.parent.gameObject.SetActive(false);
        Camera.main.GetComponent<Memory>().dataDescriptor = null;
    }
}
