#include <iostream>

using namespace std;

int main()
{
	int n;
	cin >> n;
	
	for (int i = 0; i < n; i++) {
		if (i % 2 != 0) {
			printf("¹Ú");
		}
		else {
			printf("¼ö");
		}
	}
	return 0;
}

