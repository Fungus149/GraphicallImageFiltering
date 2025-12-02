using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTab : MonoBehaviour {
    
    [SerializeField] OpenTab[] otherButtons;
    [SerializeField] GameObject panel;

    bool isClicked = false;
    public void Click() {
        if (isClicked) { UnClick(); return; }
        foreach (OpenTab otherButton in otherButtons) {
            otherButton.UnClick();
        }
        isClicked = true;
        panel.SetActive(true);
    }
    public void UnClick() {
        isClicked = false;
        panel.SetActive(false);
    }
}
