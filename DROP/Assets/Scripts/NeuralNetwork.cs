using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    [Tooltip("Input Layer Node Count")]
    public int inCount = 0;
    [Tooltip("Output Layer Node Count")]
    public int outCount = 0;
    [Tooltip("Hidden Layer Count")]
    public int layCount = 0;
    [Tooltip("Hidden Layer Node Count")]
    public int unitCount = 0;

    List<Node> nodes = new List<Node>();

    public struct Node
    {
        public float nVal;
        public List<Connection> nInConnections;
        public List<Connection> nOutConnections;

        public Node(float value, List<Connection> inCon, List<Connection> outCon)
        {
            nVal = value;
            nInConnections = inCon;
            nOutConnections = outCon;
        }
    }

    public struct Connection
    {
         Node aNode;
         Node bNode;
         float weight;

        public Connection(Node a, Node b, float w)
        {
            aNode = a;
            bNode = b;
            weight = w;
        }
        public void Set_aNode(Node a)
        {
            aNode = a;
        }
        public void Set_bNode(Node b)
        {
            bNode = b;
        }
        public void Set_Weight(float w)
        {
            weight = w;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BuildNetwork(inCount, outCount, layCount, unitCount);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    //Build our network using the parameters from the Inspector Window
    void BuildNetwork(int inputCount, int outputCount, int layerCount, int uCount)
    {
        int nCount = inputCount + layerCount * uCount + outputCount;
        //Add nodes to nodes list and start their connections
        for (int i = 0; i < nCount; i++)
        {
            Node n = new Node(0.0f, new List<Connection>(), new List<Connection>());
            //Add Backwards Connections 
            for (int j = 0; j < uCount; j++)
            {
                Connection con = new Connection(n,n, Random.Range(0, 1));
                con.Set_aNode(n);
                con.Set_bNode(n);
                con.Set_Weight(Random.Range(0, 1));
                n.nOutConnections.Add(con);
            }
            nodes.Add(n);
        }
        //Add forward Connections
        int k = 0;
        for (int i = 0; i <= nCount - outputCount + 1; i++)
        {
            //If we're assigning to anything but the last hidden layer
            if (i < nCount - uCount - outputCount)
            {
                for (int j = 0; j < uCount; j++)
                {
                    int target = inputCount + j + k;
                    nodes[i].nOutConnections[j].Set_aNode(nodes[target]);
                    Debug.Log("Node " + i + " connected to Node " + target);
                }
            }
            //If it's the last hidden layer
            else if (i < nCount - outputCount)
            {
                for (int j = 0; j < outputCount; j++)
                {
                    int target = inputCount + j + k;
                    nodes[i].nOutConnections[j].Set_aNode(nodes[target]);
                    Debug.Log("Node " + i + " connected to Node " + target);
                }
            }
            //Increment displacement every node cound within a hidden layer
            if ((i - inputCount + 1) % (uCount) == 0 && i != 0 && i != (uCount - 1))
                k += uCount;
        }

    }
    void train()
    {

    }
}
