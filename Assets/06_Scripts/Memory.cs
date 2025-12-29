using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour {
    /*
     TODO
     Multiple otputs from input!!!!

     put blocks to propper size
     some error handling
     error messages
     blocks snapping

     saving control diagrams
     saving graphs

     clamp block
     xyz block
     select subimage block
     convolution block
     deconvolution block
     region growing block
     join block
     
     make graph usable
     display values on graphs and images
     display data on node click
     */

    public GameObject selected;
    public GameObject pointDescriptor;
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
