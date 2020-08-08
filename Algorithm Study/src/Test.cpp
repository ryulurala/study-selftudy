#include <string>
#include <algorithm>
#include <iostream>

using namespace std;

int main()
{
    string str1 = "abcd";
    string str2 = "ad";

    if (string::npos != str1.find(str2))
        cout << "find() success" << endl;

    if (includes(str1.begin(), str1.end(), str2.begin(), str2.end()))
        cout << "includes success" << endl;

    return 0;
}