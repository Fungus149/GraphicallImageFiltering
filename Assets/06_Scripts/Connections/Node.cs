using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour,Memory.IClickable {
    //[SerializeField] bool input;
    public float width = 10f;
    public bool lined = false;

    [SerializeField] GameObject highlight;
    [SerializeField] bool input;

    GameObject first_node;
    GameObject lr;
    Memory memory;
    Connection lr_con;
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        memory = Camera.main.GetComponent<Memory>();
    }
    private void OnMouseDown() {
        first_node = memory.selected;
        //Debug.Log("click" + first_node != null);
        //if (first_node == gameObject) memory.selected = null;
        if (input){
            if (first_node != null && first_node.GetComponent<Node>() != null && !lined){
                Debug.Log("Lining");
                lined = true;
                lr = new GameObject("Line");
                lr_con = lr.AddComponent<Connection>();
                lr_con.second_node = gameObject;
                lr_con.first_node = first_node;
                GetComponentInParent<Block>().source = first_node.GetComponentInParent<Block>();
                first_node.GetComponentInParent<Block>().destination = GetComponentInParent<Block>();
                GetComponentInParent<Block>().Refresh();
            }
        } // if (input)

        if (first_node == gameObject){
            Unclick();
            memory.selected = null;
            return;
        }
        if (first_node != null) first_node.GetComponent<Memory.IClickable>().Unclick();
        memory.selected = gameObject;
        sr.color = UnityEngine.Color.magenta;
        //else {

        //    //GameObject highlight = new GameObject("highlight");
        //    //SpriteRenderer sr = highlight.AddComponent<SpriteRenderer>();
        //    //sr.sprite = Sprite.Create(Texture2D.whiteTexture, new ;
        //    //sr.color = Color.cyan;
        //    //highlight.transform.localPosition = Vector3.forward;
        //    //highlight.transform.localScale += Vector3.one;
        //}
    }

    public void Unclick(){
        if (input) sr.color = UnityEngine.Color.cyan;
        else sr.color = UnityEngine.Color.yellow;
    }
}
