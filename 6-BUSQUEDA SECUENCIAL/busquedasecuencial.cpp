#include <iostream>
#include <vector>

int findEle(const std::vector<int>& inputArr, int s, int targetEle) {
    for (int k = 0; k < s; k++) {
        if (inputArr[k] == targetEle) {
            return k;
        }
    }
    return -1;
}

int main() {
    std::vector<int> inputArr = {12, 34, 10, 6, 40, 89, 98, 57, 19, 69};
    int targetElement = 40;
    int s = inputArr.size();

    int idx = findEle(inputArr, s, targetElement);
    if (idx != -1) {
        std::cout << "el elemento se encuentra en la posicion: " << (idx + 1) << std::endl;
    } else {
        std::cout << "no se encuentra el elemento" << std::endl;
    }
    return 0;
}
