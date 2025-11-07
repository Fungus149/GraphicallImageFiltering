using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public GameObject selected;
    public bool key_lock = false;

    public interface IClickable {
        void Unclick() { }
    }

    private void Update(){
        if (Input.GetKeyUp(KeyCode.Delete) && selected != null && !key_lock) { 
            if (selected.GetComponent<Block>() != null) Destroy(selected);
            if (selected.GetComponent<Connection>() != null) selected.GetComponent<Connection>().Destroy(); ;
        }
    }

    
}
