#include <iostream>
#include <vector>

class ShellSort {
public:
    static void displayArr(std::vector<int>& inputArr) {
        for (int k : inputArr) {
            std::cout << k << " ";
        }
        std::cout << std::endl;
    }

    int sort(std::vector<int>& inputArr) {
        int size = inputArr.size();
        int gapSize = size / 2;
        while (gapSize > 0) {
            for (int j = gapSize; j < size; j++) {
                int val = inputArr[j];
                int k = j;
                while (k >= gapSize && inputArr[k - gapSize] > val) {
                    inputArr[k] = inputArr[k - gapSize];
                    k = k - gapSize;
                }
                inputArr[k] = val;
            }
            gapSize = gapSize / 2;
        }
        return 0;
    }
};

int main() {
    std::vector<int> inputArr = {36, 34, 43, 11, 15, 20, 28, 45};
    std::cout << "Arreglo antes de ser ordenado: " << std::endl;
    ShellSort::displayArr(inputArr);
    ShellSort obj;
    obj.sort(inputArr);
    std::cout << "Arreglo despues de ser ordenado: " << std::endl;
    ShellSort::displayArr(inputArr);
    return 0;
}
