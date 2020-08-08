#include <iostream>

using namespace std;

int main()
{
	int num1, num2;
	int res3, res4, res5, res6;
	cin >> num1;
	cin >> num2;

	res3 = num1 * (num2 % 10);
	res4 = num1 * ((num2 / 10) % 10);
	res5 = num1 * (num2 / 100);
	res6 = num1 * num2;

	cout << res3 << endl;
	cout << res4 << endl;
	cout << res5 << endl;
	cout << res6 << endl;

	return 0;
}