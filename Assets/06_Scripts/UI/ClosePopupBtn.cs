using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopupBtn : MonoBehaviour {
    public void ClosePopup() {
        transform.parent.gameObject.SetActive(false);
    }
}
