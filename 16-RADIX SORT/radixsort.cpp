#include <iostream>
#include <vector>

void countingSort(std::vector<int>& arr, int exp) {
    int s = arr.size();
    std::vector<int> outputArray(s, 0);
    std::vector<int> countArray(10, 0);
    for (int j = 0; j < s; j++) {
        int idx = arr[j] / exp;
        countArray[idx % 10] += 1;
    }
    for (int j = 1; j < 10; j++) {
        countArray[j] += countArray[j - 1];
    }
    int j = s - 1;
    while (j >= 0) {
        int idx = arr[j] / exp;
        outputArray[countArray[idx % 10] - 1] = arr[j];
        countArray[idx % 10] -= 1;
        j -= 1;
    }
    for (j = 0; j < arr.size(); j++) {
        arr[j] = outputArray[j];
    }
}

void radixSort(std::vector<int>& arr) {
    int max1 = arr[0];
    for (int e : arr) {
        if (e > max1) {
            max1 = e;
        }
    }
    int exp = 1;
    while (max1 / exp >= 1) {
        countingSort(arr, exp);
        exp *= 10;
    }
}

int main() {
    std::vector<int> arr = {171, 46, 76, 91, 803, 25, 3, 67};
    std::cout << "Arreglo antes de ordenar: " << std::endl;
    for (int j = 0; j < arr.size(); j++) {
        std::cout << arr[j] << " ";
    }
    radixSort(arr);
    std::cout << "\nDespues de ordenar el arreglo: " << std::endl;
    for (int j = 0; j < arr.size(); j++) {
        std::cout << arr[j] << " ";
    }
    return 0;
}
