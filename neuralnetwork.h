#pragma once
#include <vector> 
#include <cstdlib>
#include <time.h>       /* time */

using namespace std;

class neuralnetwork//extern "C"
{

	struct Node;
	struct Connection;

	struct Node
	{
		float nVal;
		vector<Connection*> nInConnections;
		vector<Connection*> nOutConnections;

		Node(float value, vector<Connection*> inCon, vector<Connection*> outCon)
		{
			nVal = value;
			for (int i = 0; i < inCon.size(); i++)
			{
				nInConnections.push_back(inCon[i]);
			}
			for (int i = 0; i < outCon.size(); i++)
			{
				nInConnections.push_back(outCon[i]);
			}
		}
		Node()
		{
			nVal = 0;
		}
		struct Node& operator=(Node sNode) { return Node(sNode.nVal, sNode.nInConnections, sNode.nOutConnections); };
	};

	struct Connection
	{
		Node* aNode;
		Node* bNode;
		float weight;

		Connection(Node* a, Node* b, float w)
		{
			aNode = a;
			bNode = b;
			weight = w;
		}
		void Set_aNode(Node a)
		{
			aNode = &a;
		}
		void Set_bNode(Node b)
		{
			bNode = &b;
		}
		void Set_Weight(float w)
		{
			weight = w;
		}
	};

public:
	vector<Node*> nodes = vector<Node*>();

	//Build our network using the parameters from the Inspector Window
	void BuildNetwork(int inputCount, int outputCount, int layerCount, int uCount)
	{
		srand(time(NULL));
		int nCount = inputCount + layerCount * uCount + outputCount;
		//Add nodes to nodes vector and start their connections
		for (int i = 0; i < nCount; i++)
		{
			Node* n = new Node();
			n->nVal = i;
			//Add Backwards Connections 
			for (int j = 0; j < uCount; j++)
			{
				Connection* con = new Connection(n, n, (rand() % 1000) / 1000);
				*con->aNode = *n;
				*con->bNode = *n;
				int r = rand();
				float temp = ( r % 1000) / 1000.0f;
				con->Set_Weight(temp);
				if (i < inputCount)
				{
					n->nOutConnections.push_back(con);
				}
				else if (i < inputCount + uCount)
				{
					if(j<inputCount)
						n->nInConnections.push_back(con);
					n->nOutConnections.push_back(con);
				}
				else
				{
					n->nInConnections.push_back(con);
					n->nOutConnections.push_back(con);
				}
				
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
					nodes[i]->nOutConnections[j]->bNode = nodes[target];
				}
			}
			//If it's the last hidden layer
			else if (i < nCount - outputCount)
			{
				for (int j = 0; j < outputCount; j++)
				{
					int target = inputCount + j + k;
					nodes[i]->nOutConnections[j]->bNode = nodes[target];
				}
			}
			//Increment displacement every layer node cound 
			if ((i - inputCount + 1) % (uCount) == 0 && i != 0 && i != (uCount - 1))
				k += uCount;
		}

	}

	void train()
	{

	}
};