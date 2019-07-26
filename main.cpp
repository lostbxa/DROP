#include "main.h"
# include <iostream>

using namespace std;

main::main()
{

}
main::~main()
{
}

int main()
{
	neuralnetwork nn;
	int a = 0;
	int b = 0;
	int c = 0;
	int d = 0;
	int totalcount = 0;
	bool exit = false;
	while (!exit)
	{
		cout << "Enter Input Count, Output Count, Layer Count, Layer Node Count: ";
		cin >> a >> b >> c >> d;
		cout << "Input Count: " << a << " Output Count: " << b << " Layer Count: " << c << " Layer Node Count: " << d << endl;
		nn.BuildNetwork(a, b, c, d);
		totalcount = a + b + (c*d);
		cout << "Total Count: " << totalcount << endl;
		cout << "Which node would you like to see?";
		int n = 0;
		cin >> n;
		n -= 1;
		if (n < totalcount)
		{
			cout << endl << "Value: " << nn.nodes[n]->nVal << endl;
			for (int i = 0; i < nn.nodes[n]->nOutConnections.size(); i++)
			{
				cout << "Forward connection to " << nn.nodes[n]->nOutConnections[i]->bNode->nVal << " Weight: " << nn.nodes[n]->nOutConnections[i]->weight << endl;
			}
			for (int i = 0; i < nn.nodes[n]->nInConnections.size(); i++)
			{
				cout << "Backwards connection to " << nn.nodes[n]->nInConnections[i]->aNode->nVal << " Weight: " << nn.nodes[n]->nInConnections[i]->weight << endl;
			}
		}
		else
		{
			cout << "NUMBER TOO BIG";
		}

	}
	return a;
		
}