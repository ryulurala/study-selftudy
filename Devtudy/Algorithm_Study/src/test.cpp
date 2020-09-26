#include <string>
#include <vector>
#include <iostream>
#include <tuple>

using namespace std;

int main()
{
    tuple<int, int, int, int> t;
    t = make_tuple(1, 2, 3, 4);

    printf("<before>\n");
    printf("%d\n", get<0>(t));
    printf("%d\n", get<1>(t));
    printf("%d\n", get<2>(t));
    printf("%d\n", get<3>(t));

    get<2>(t) = 10;
    get<3>(t) = 20;

    printf("<after>\n");
    printf("%d\n", get<0>(t));
    printf("%d\n", get<1>(t));
    printf("%d\n", get<2>(t));
    printf("%d\n", get<3>(t));

    return 0;
}