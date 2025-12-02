using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OperationDropdown : MonoBehaviour {
    Arithmetic arth;
    private void Start() {
        arth = GetComponentInParent<Arithmetic>();
    }
    public void GrabValueFromDropdown(int optionIndex) {
        arth.operation = GetComponentInParent<TMP_Dropdown>().options[optionIndex].text;
        arth.Refresh();
    }
}
