using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchbox : MonoBehaviour {
    [SerializeField] bool isHorizontal;

    Camera cam;
    Matrix matrix;
    int change;
    private void Start() {
        cam = Camera.main;
        matrix = GetComponentInParent<Matrix>();
    }
    private void OnMouseDrag() {
        Vector2 current = cam.ScreenToWorldPoint(Input.mousePosition);
        if (isHorizontal) {
            change = (int)((current.x - transform.position.x) / 60);
            if (change != 0 && matrix.valueEntries.GetLength(0) + change >=3) {
                transform.position = new Vector3(transform.position.x + change * 60, transform.position.y, transform.position.z);
                matrix.Rescale(change, 0);
            }
            return;
        }
        change = (int)((current.y - transform.position.y) / 30);
        //Debug.Log($"change = {-change}");
        if (change != 0 && matrix.valueEntries.GetLength(1) - change >= 3) {
            transform.position = new Vector3(transform.position.x, transform.position.y + change * 30, transform.position.z);
            matrix.Rescale(0,-change);
        }
    }
}
