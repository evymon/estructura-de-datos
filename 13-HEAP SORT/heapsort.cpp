#include <iostream>
#include <vector>

void heapify(std::vector<int>& arr, int n, int i) {
    int largest = i;
    int left = 2 * i + 1;
    int right = 2 * i + 2;
    if (left < n && arr[left] > arr[largest]) {
        largest = left;
    }
    if (right < n && arr[right] > arr[largest]) {
        largest = right;
    }
    if (largest != i) {
        std::swap(arr[i], arr[largest]);
        heapify(arr, n, largest);
    }
}

void heapsort(std::vector<int>& arr) {
    int n = arr.size();
    for (int i = n / 2 - 1; i >= 0; i--) {
        heapify(arr, n, i);
    }
    for (int i = n - 1; i > 0; i--) {
        std::swap(arr[0], arr[i]);
        heapify(arr, i, 0);
    }
}

int main() {
    std::vector<int> lista = {12, 11, 13, 5, 6, 7};
    std::cout << "Antes de ordenar los elementos del array son: ";
    for (int e : lista) {
        std::cout << e << " ";
    }
    heapsort(lista);
    std::cout << "\n\nDespues de ordenar el arreglo: ";
    for (int e : lista) {
        std::cout << e << " ";
    }
    return 0;
}
