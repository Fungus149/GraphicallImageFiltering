using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour {
    [SerializeField] GameObject block;
    [SerializeField] GameObject controlSystem;

    Camera camuwu;
    Vector3 position;
    GameObject instance;
    private void Start() {
        camuwu = Camera.main;
    }
    public void Click() {
        position = camuwu.ScreenToWorldPoint(Input.mousePosition);
        instance = Instantiate(block, controlSystem.transform);
        //instance.transform.position = new Vector3(position.x, position.y, -1);
    }
}
