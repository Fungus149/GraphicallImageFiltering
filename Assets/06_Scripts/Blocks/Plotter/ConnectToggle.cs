using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToggle : MonoBehaviour
{
    Plotter pt;

    private void Start() {
        pt = GetComponentInParent<Plotter>();
    }
    public void Toggle(bool value) {
        pt.connect=value;
        pt.Refresh();
    }
}
