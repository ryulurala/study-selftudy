#include <iostream>
#include <string>
#include <vector>

using namespace std;

int main()
{
    string c= "c";
    string s = "C";
    bool check = string::npos != s.find(c);

    cout<<check<<endl;
    return 0;
}