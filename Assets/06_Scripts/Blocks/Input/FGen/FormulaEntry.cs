using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulaEntry : MonoBehaviour
{
    Fgen fg;
    private void Start() {
        fg = GetComponentInParent<Fgen>();
    }
    public void GrabValueFromEntry(string entry) {
        fg.function = entry;
        fg.Refresh();
    }
}
