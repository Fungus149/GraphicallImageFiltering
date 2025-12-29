using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dots : MonoBehaviour {
    GameObject pointDescriptor;
    bool isScrollLocked;
    public float x, y;
    void Start () {
        isScrollLocked = Camera.main.GetComponent<Navigation>().isScrollLocked;
        pointDescriptor = Camera.main.GetComponent<Memory>().pointDescriptor;
    }
    private void OnMouseEnter() {
        isScrollLocked = true;
        pointDescriptor.SetActive(true);
        pointDescriptor.transform.position = new Vector3(transform.position.x+37.5f, transform.position.y+25f,-3);
        pointDescriptor.GetComponentInChildren<TextMeshProUGUI>().text = $"x = {(Mathf.Round(x*100)/100).ToString()} \ny = {(Mathf.Round(y * 100) / 100).ToString()}";
    }
    private void OnMouseExit() {
        pointDescriptor.SetActive(false);
        isScrollLocked=false;
    }
}
