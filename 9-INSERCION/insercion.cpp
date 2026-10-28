#include <iostream>
#include <vector>

void insertionSort(std::vector<int>& a) {
    for (int i = 1; i < a.size(); i++) {
        int temp = a[i];
        int j = i - 1;
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j = j - 1;
        }
        a[j + 1] = temp;
    }
}

void printArr(std::vector<int>& a) {
    for (int i = 0; i < a.size(); i++) {
        std::cout << a[i] << " ";
    }
}

int main() {
    std::vector<int> a = {70, 15, 2, 51, 60};
    std::cout << "Antes de ordenar los elementos del arreglo: " << std::endl;
    printArr(a);
    insertionSort(a);
    std::cout << "\nDespues de arreglos los elementos del arreglo: " << std::endl;
    printArr(a);
    return 0;
}
