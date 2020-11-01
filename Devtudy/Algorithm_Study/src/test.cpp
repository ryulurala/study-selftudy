#include <string>
#include <vector>
#include <iostream>
#include <tuple>
#include <algorithm>

using namespace std;

int jjj(int i){
    return i--;
}

int kkk(int i){
    return i++;
}


class A{
public:
    int a;
    A(){
        cout<<"hi"<<endl;
    }
    ~A(){
        cout<<"bye"<<endl;
    }

};

int main()
{
    int i=0;
    cout<< i++ <<" "<< i-- <<" "<< i <<endl;
    int k=3;

    cout<< --k <<" "<<k++<<" "<< k--<<endl;

    i=jjj(-2);
    cout<<i<<endl;
    cout<<jjj(i)<<kkk(i)<<endl;

    A a;
    cout<<sizeof(A)<<endl;
    A();
}