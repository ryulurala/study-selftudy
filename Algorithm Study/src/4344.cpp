#include <iostream>
#include <cmath>

using namespace std;

int main()
{
	int testCnt;
	int** test;
	cin >> testCnt;
	cout.precision(3);
	cout.setf(ios::fixed);

	test = new int* [testCnt];
	for (int cnt, i = 0; i < testCnt; i++) {
		cin >> cnt;
		test[i] = new int[cnt + 1];
		test[i][0] = cnt;
		for (int j = 1; j <= cnt; j++) {
			cin >> test[i][j];
		}
	}

	for (int i = 0; i < testCnt; i++) {
		double avg = 0;
		int cnt = 0;
		for (int j = 1; j <= test[i][0]; j++) {
			avg += test[i][j];
		}
		avg /= test[i][0];

		for (int j = 1; j <= test[i][0]; j++) {
			if (test[i][j] > avg) {
				cnt++;
			}
		}
		double res = round((double)cnt / test[i][0] * 100000) /1000;
		cout << res << "%" << endl;
	}

	for (int i = 0; i < testCnt; i++) {
		delete[] test[i];
	}
	delete[] test;

	return 0;
}