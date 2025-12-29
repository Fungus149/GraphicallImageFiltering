using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;


public class Navigation : MonoBehaviour{
    public GameObject bg;
    public float velocity;
    public bool isScrollLocked = false;

    Vector2 current;
    Vector2 last;
    float cam_size;
    bool click = true;
    private void Start() {
         cam_size = GetComponent<Camera>().orthographicSize;
    }
    void Update(){
        if (Input.GetMouseButton(2)) {
            if (click) { last = Input.mousePosition; click = false; }
            current = Input.mousePosition;
            bg.transform.position += new Vector3(cam_size * 0.001f * velocity * (current.x - last.x), cam_size * 0.001f * velocity * (current.y - last.y), 0);
            last = current;
        }
        else click = true;
        if (!isScrollLocked) {
            cam_size -= Input.mouseScrollDelta.y * 50;
            if (cam_size < 50) { cam_size = 50; }
            GetComponent<Camera>().orthographicSize = cam_size;
        }
    }
}
