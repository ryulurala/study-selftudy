#include <iostream>

using namespace std;

int main()
{
	int N, X;
	int* ary;
	cin >> N >> X;
	
	ary = new int[N];
	
	for (int i = 0; i < N; i++) {
		cin >> ary[i];
	}
	
	for (int i = 0; i < N; i++) {
		if (ary[i] < X) {
			cout << ary[i]<<" ";
		}
	}
	
	return 0;
}