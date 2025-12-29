using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionDropdownThr : MonoBehaviour {
    Threshold thr;
    private void Start() {
        thr = GetComponentInParent<Threshold>();
    }
    public void GrabValueFromDropdown(int optionIndex) {
        thr.option = GetComponentInParent<TMP_Dropdown>().options[optionIndex].text;
        thr.Refresh();
    }
}
