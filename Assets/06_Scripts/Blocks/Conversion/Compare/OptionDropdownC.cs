using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionDropdownCom : MonoBehaviour {
    Compare com;
    private void Start() {
        com = GetComponentInParent<Compare>();
    }
    public void GrabValueFromDropdown(int optionIndex) {
        com.option = GetComponentInParent<TMP_Dropdown>().options[optionIndex].text;
        com.Refresh();
    }
}

