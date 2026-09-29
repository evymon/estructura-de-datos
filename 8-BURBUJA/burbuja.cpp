#include <iostream>
#include <vector>

void bubbleSort(std::vector<int>& a) {
    int s = a.size();
    for (int i = 0; i < s; i++) {
        bool isSwapped = false;
        for (int j = 0; j < s - i - 1; j++) {
            if (a[j] > a[j + 1]) {
                std::swap(a[j], a[j + 1]);
                isSwapped = true;
            }
        }
        if (isSwapped == false) {
            break;
        }
    }
}

int main() {
    std::vector<int> a = {15, 16, 11, 13, 14};
    std::cout << "antes de ordenar los elementos del array son: " << std::endl;
    for (int e : a) {
        std::cout << e << " ";
    }

    bubbleSort(a);
    std::cout << "\ndespues de ordenar los elementos del array son: " << std::endl;
    for (int e = 0; e < a.size(); e++) {
        std::cout << a[e] << " ";
    }
    return 0;
}
