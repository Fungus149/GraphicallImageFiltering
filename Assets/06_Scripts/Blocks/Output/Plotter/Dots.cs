using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Dots : MonoBehaviour {
    GameObject pointDescriptor;
    public float x, y;
    void Start () {
        pointDescriptor = Camera.main.GetComponent<Memory>().pointDescriptor;
    }
    private void OnMouseEnter() {
        Camera.main.GetComponent<Navigation>().isScrollLocked = true;
        pointDescriptor.SetActive(true);
        pointDescriptor.transform.position = new Vector3(transform.position.x + 37.5f, transform.position.y + 25f, transform.position.z - 1);
        pointDescriptor.GetComponentInChildren<TextMeshProUGUI>().text = $"x = {(Mathf.Round(x*100)/100).ToString()} \ny = {(Mathf.Round(y * 100) / 100).ToString()}";
    }
    private void OnMouseExit() {
        pointDescriptor.SetActive(false);
        Camera.main.GetComponent<Navigation>().isScrollLocked = false;
    }
}