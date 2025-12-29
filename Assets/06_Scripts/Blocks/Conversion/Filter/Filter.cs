using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaplaceFilter : MonoBehaviour
{
    public double Gs;
    double[,,] input;
    double[,,] output;
    
    //public void ReadLaplace(string formula) {
    //    Gs = double.Parse(formula);
    //    input = transform.GetComponentInParent<Block>().input;
    //    output = new double[input.Length];
    //    for (int i = 0; i<input.Length;i++) { output[i] = input[i]*Gs; }
    //    foreach (double y in output) { Debug.Log(y); }
    //}
}
