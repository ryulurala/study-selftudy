#include <string>
#include <vector>
#include <iostream>
#include <algorithm>

using namespace std;
// expression(string) 3이상 100이하
// operand 0이상 999 이하
// 음수 X
// operator : +, -, * (가변적 개수)

long long solution(string expression)
{
    long long answer = 0;
    vector<int> num;       // num 배열
    vector<char> oper;     // oper 배열
    vector<char> diffOper; // 서로 다른 oper, 최대 3, 최소 1

    // 담기
    int k = 0;
    for (int i = 0; i < expression.length(); i++)
    {
        if (!(expression[i] >= '0' && expression[i] <= '9')) // '+', '-', '-'
        {
            oper.push_back(expression[i]);
            num.push_back(stoi(expression.substr(k, i - k)));
            k = i + 1;

            // 다른 oper 삽입
            bool isEqual = true;
            for (auto elem : diffOper)
                isEqual &= (elem != expression[i]);
            if (isEqual)
                diffOper.push_back(expression[i]);
        }
    }
    // 후 처리
    num.push_back(stoi(expression.substr(k)));

    // 계산
    do
    { // TODO
        long long result = 0;
        string s(expression);

        if (answer < result)
            answer = result;
    } while (next_permutation(diffOper.begin(), diffOper.end()));

    return answer; // 우승 시 받을 수 있는 가장 큰 금액
}