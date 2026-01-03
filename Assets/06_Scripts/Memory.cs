using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour {
    /*
    TODO
    matrix block

    saving control diagrams
         
    clamp block
    xyz block
    convolution block
    deconvolution block
    region growing block
    join block
    cumsum block
    select subimage block
    
    saving graphs

    Error when changing from asympthote only graph to dots only graph
    */

    public GameObject selected;
    public GameObject dataDescriptor;
    public GameObject pointDescriptor;
    public GameObject imageDisplay;
    public GameObject sequenceDisplay;
    public GameObject controlSystem;
    public bool isKeyLocked = false;
    public interface IClickable {
        void Unclick() { }
    }
    private void Update(){
        if ((Input.GetKeyUp(KeyCode.Delete) || Input.GetKeyUp(KeyCode.Backspace)) && selected != null && !isKeyLocked) { 
            if (selected.GetComponent<Block>() != null) Destroy(selected);
            if (selected.GetComponent<Connection>() != null) selected.GetComponent<Connection>().Destroy(); ;
        }
    }
}
