using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlotController : MonoBehaviour {
    [SerializeField] GameObject dotsContainer;
    [SerializeField] GameObject asymptotesContainer;
    [SerializeField] GameObject xAxis;

    Plotter plotter;
    Camera camuwu;
    Vector2 current;
    Vector2 offset;
    float scroll;
    void Start() {
        camuwu = Camera.main;
        plotter = GetComponentInParent<Plotter>();
    }
    private void OnMouseEnter() {
        camuwu.GetComponent<Navigation>().isScrollLocked = true;
    }
    private void OnMouseOver() {
        scroll = Input.mouseScrollDelta.y;
        if (scroll != 0) {
            if (Input.GetKey(KeyCode.LeftShift)) scroll /= 10;
            plotter.Rescale(scroll/2);
        }
    }
    private void OnMouseExit() {
        camuwu.GetComponent<Navigation>().isScrollLocked = false;
    }
    public void OnMouseDown() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        offset = new Vector2(dotsContainer.transform.position.x - current.x, dotsContainer.transform.position.y - current.y);
    }
    public void OnMouseDrag() {
        current = camuwu.ScreenToWorldPoint(Input.mousePosition);
        dotsContainer.transform.position = new Vector3(current.x + offset.x, current.y + offset.y, dotsContainer.transform.position.z);
        plotter.plotOffset = new Vector2(dotsContainer.transform.position.x - transform.position.x, dotsContainer.transform.position.y - transform.position.y);
        asymptotesContainer.transform.position = new Vector3(current.x + offset.x, asymptotesContainer.transform.position.y, asymptotesContainer.transform.position.z);
        xAxis.transform.position = new Vector3(xAxis.transform.position.x, current.y + offset.y, xAxis.transform.position.z);

        plotter.Trim();
    }
}
