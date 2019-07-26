using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class TestNative : MonoBehaviour
{
    [Tooltip("Input Layer Node Count")]
    public int inCount = 0;
    [Tooltip("Output Layer Node Count")]
    public int outCount = 0;
    [Tooltip("Hidden Layer Count")]
    public int layCount = 0;
    [Tooltip("Hidden Layer Node Count")]
    public int unitCount = 0;


    [DllImport("NeuralNetwork")]
    private static extern void BuildNetwork(int inputCount, int outputCount, int layerCount, int uCount);
    // Start is called before the first frame update
    void Start()
    {
        BuildNetwork(inCount, outCount, layCount, unitCount);
    }

}
