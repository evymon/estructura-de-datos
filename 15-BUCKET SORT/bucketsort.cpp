#include <iostream>
#include <vector>

void insertion_sort(std::vector<double>& bukt) {
    for (int j = 1; j < bukt.size(); j++) {
        double val = bukt[j];
        int k = j - 1;
        while (k >= 0 && bukt[k] > val) {
            bukt[k + 1] = bukt[k];
            k -= 1;
        }
        bukt[k + 1] = val;
    }
}

void bucket_sort(std::vector<double>& inputArr) {
    int s = inputArr.size();
    std::vector<std::vector<double>> bucketArr(s);
    for (double j : inputArr) {
        int bi = (int)(s * j);
        bucketArr[bi].push_back(j);
    }
    for (std::vector<double>& bukt : bucketArr) {
        insertion_sort(bukt);
    }
    int idx = 0;
    for (std::vector<double>& bukt : bucketArr) {
        for (double j : bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}

int main() {
    std::vector<double> inputArr = {0.77, 0.16, 0.38, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67};
    std::cout << "Arreglo antes de ordenar:" << std::endl;
    for (double e : inputArr) {
        std::cout << e << " ";
    }
    std::cout << std::endl;
    bucket_sort(inputArr);
    std::cout << "Arreglo despues de ordenar:" << std::endl;
    for (double e : inputArr) {
        std::cout << e << " ";
    }
    std::cout << std::endl;
    return 0;
}
