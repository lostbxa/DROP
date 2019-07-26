#include <vector> 
#include <cstdlib>
#include <time.h>       /* time */


#define DLLExport __declspec (dllexport)

using namespace std;

extern "C"
{

	struct Node;
	struct Connection;

	struct Node
    {
        float nVal;
        vector<Connection*> nInConnections;
        vector<Connection*> nOutConnections;

        Node(float value, vector<Connection> inCon, vector<Connection> outCon)
        {
            nVal = value;
			for (int i = 0; i < inCon.size(); i++)
			{
				nInConnections.push_back(& inCon[i]);
			}
			for (int i = 0; i < outCon.size(); i++)
			{
				nInConnections.push_back(&outCon[i]);
			}
        }
		Node()
		{
			nVal = 0;
		}
	};

    struct Connection
    {
         Node* aNode;
         Node* bNode;
         float weight;

        Connection(Node a, Node b, float w)
        {
            aNode = & a;
            bNode = & b;
            weight = w;
        }
        void Set_aNode(Node a)
        {
            aNode = & a;
        }
        void Set_bNode(Node b)
        {
            bNode = & b;
        }
        void Set_Weight(float w)
        {
            weight = w;
        }
	};

	vector<Node> nodes = vector<Node>();

    //Build our network using the parameters from the Inspector Window
    DLLExport void BuildNetwork(int inputCount, int outputCount, int layerCount, int uCount)
    {
		srand(time(NULL));
        int nCount = inputCount + layerCount * uCount + outputCount;
        //Add nodes to nodes vector and start their connections
        for (int i = 0; i < nCount; i++)
        {
			Node n = Node();
            //Add Backwards Connections 
            for (int j = 0; j < uCount; j++)
            {
				Connection con = Connection(n,n, (rand() % 1000) / 1000);
                con.Set_aNode(n);
                con.Set_bNode(n);
                con.Set_Weight((rand() % 1000) / 1000);
                n.nOutConnections.push_back(&con);
            }
            nodes.push_back(n);
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
					nodes[i].nOutConnections[j]->aNode = & nodes[target];
                }
            }
            //If it's the last hidden layer
            else if (i < nCount - outputCount)
            {
                for (int j = 0; j < outputCount; j++)
                {
                    int target = inputCount + j + k;
					nodes[i].nOutConnections[j]->aNode = &nodes[target];
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
