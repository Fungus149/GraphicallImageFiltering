using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Block : MonoBehaviour{
    public Block source;
    public Block destination;
    public double[,,] input;
    public double[,,] output;

    public virtual void Refresh() {
        input = source.output;
        if (destination != null) { output = destination.input; }
    }
}
