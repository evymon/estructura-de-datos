#include <iostream>
#include <vector>

void selection(std::vector<int>& a) {
    for (int i = 0; i < a.size(); i++) {
        int small = i;
        for (int j = i + 1; j < a.size(); j++) {
            if (a[small] > a[j]) {
                small = j;
            }
        }
        int temp = a[i];
        a[i] = a[small];
        a[small] = temp;
    }
}

void printArr(std::vector<int>& a) {
    for (int i = 0; i < a.size(); i++) {
        std::cout << a[i] << " ";
    }
}

int main() {
    std::vector<int> a = {65, 26, 13, 23, 12};
    std::cout << "Arreglo antes de ser ordenado: " << std::endl;
    printArr(a);
    selection(a);
    std::cout << "\nArreglo despues de ser ordenado: " << std::endl;
    selection(a);
    printArr(a);
    return 0;
}
