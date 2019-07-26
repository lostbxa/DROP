using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceTest : MonoBehaviour
{
    int input = 2;
    int output = 2;
    int uCount = 3;
    int nCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        int k = 0;
        for (int i = 0; i <= nCount - output + 1; i++)
        {
            Debug.Log("i: " + i);

            if (i < nCount - uCount - output)
            {
                for (int j = 0; j < uCount; j++)
                {
                    int target = input + j + k;
                    Debug.Log("j : " + j + " Target: " + target);
                    //nodes[i].nOutConnections[j].Set_aNode(nodes[inputCount - i + j]);
                }
            }
            else if (i < nCount - output)
            {
                for (int j = 0; j < output; j++)
                {
                    int target = input + j + k;
                    Debug.Log("j : " + j + " Target: " + target);
                    //nodes[i].nOutConnections[j].Set_aNode(nodes[inputCount - i + j]);
                }
            }
            if ((i - input + 1) % (uCount) == 0 && i != 0)
                k += uCount;
        }
    }
    void transfer(ref int i)
    {
        output = i;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
