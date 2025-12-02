using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour {
    /*
     TODO
     move more block functions to block???

     put blocks to propper size
     some error handling
     error messages
     blocks snapping

     reading images
     saving control diagrams
     saving graphs
     saving images

     split channels block
     merge channels block
     select subimage block
     convolution block
     join block
     
     make graph usable
     display values on graphs and images
     */

    public GameObject selected;
    public bool isKeyLocked = false;

    public interface IClickable {
        void Unclick() { }
    }
    private void Update(){
        if (Input.GetKeyUp(KeyCode.Delete) && selected != null && !isKeyLocked) { 
            if (selected.GetComponent<Block>() != null) Destroy(selected);
            if (selected.GetComponent<Connection>() != null) selected.GetComponent<Connection>().Destroy(); ;
        }
    }

    
}
