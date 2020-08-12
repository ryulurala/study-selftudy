#include <iostream>
#include <vector>
#include <algorithm>
#include <string>
#include <map>
#include <queue>

using namespace std;
struct cmp {
    bool operator()(pair<string, int> a, pair<string, int> b) {
        return a.second < b.second;
    }
};

int main()
{
    priority_queue<pair<string, int>, vector<pair<string, int>>, cmp> q;

    q.push(make_pair("1", 6)); // 1
    q.push(make_pair("2", 1));  // 1 2
    q.push(make_pair("3", 2));  // 1 3 2
    q.push(make_pair("4", 2)); // 1 4 3 2
    q.push(make_pair("5", 2)); // 1 4 5 3 2
    q.push(make_pair("6", 5)); // 1 6 5 4 3 2

    while (!q.empty()) {
        cout<<q.top().first<<endl;
        q.pop();
    }

    return 0;
}