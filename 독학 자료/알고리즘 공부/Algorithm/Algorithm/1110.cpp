#include <iostream>

using namespace std;

int main()
{
	int num1, num2;
	int cnt = 0;
	int first, last;
	cin >> num1;
	num2 = num1;
	while (cnt == 0 || num1 != num2) {
		//cout << "num1 = " << num << endl;
		if (num2 < 10) {
			first = 0;
			last = num2;
			//cout << "first = " << first << endl;
			//cout << "last = " << last << endl;
		}
		else {
			first = num2 / 10;
			last = num2 % 10;
			//cout << "first = "<<first << endl;
			//cout << "last = " << last << endl;
		}
		num2 = first + last;
		//cout << "num2 = " << num << endl;
		num2 = last * 10 + num2 % 10;
		//cout << "num3 = " << num << endl;
		cnt++;
	}
	cout << cnt << endl;
	return 0;
}