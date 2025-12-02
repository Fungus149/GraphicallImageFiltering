using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Block : MonoBehaviour{
    public bool isMoveLocked;

    [SerializeField] protected NodeIn[] nodesIn; // size stands for amount of outputs
    [SerializeField] protected NodeOut[] nodesOut;
    //public double[,,,] input; // sizes stand for x,y,z and amount of outputs
    //public double[,,,] output;

    public virtual void Refresh() { // Refreshing must spread
        // do seomething
        foreach (NodeOut destination in nodesOut) { destination.GetComponentInParent<Block>().Refresh(); }
    }

    public virtual void Error(string error_message=null) { // Errors must spread
        // if(error_message != null) source of error
        foreach (NodeOut destination in nodesOut) { destination.GetComponentInParent<Block>().Error(); }
    }
}
