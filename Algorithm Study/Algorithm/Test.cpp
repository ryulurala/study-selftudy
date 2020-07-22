#include <iostream>
#include <vector>
#include <map>

using namespace std;

int main(void)
{
    map<int, int> m;
    m[0] = 1;
    m[2] = 3;
    m[5] = 4;
    m[7] = 3;
    m[1] = 2;

    for (auto iter = m.begin(); iter != m.end(); iter++)
    {
        cout << iter->first << ", " << iter->second << endl;
    }

    return 0;
}