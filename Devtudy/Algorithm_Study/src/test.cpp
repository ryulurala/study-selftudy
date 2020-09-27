#include <string>
#include <vector>
#include <iostream>
#include <tuple>
#include <algorithm>

using namespace std;

int main()
{
    vector<int> v;
    v.push_back(5);
    v.push_back(4);
    v.push_back(2);
    v.push_back(9);
    v.push_back(1);
    v.push_back(3);

    sort(v.begin(), v.end());

    for (auto e : v)
    {
        printf("%d\n", e);
    }
    return 0;
}