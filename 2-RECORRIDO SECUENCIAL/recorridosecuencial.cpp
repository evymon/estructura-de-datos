#include <iostream>
using namespace std;
int main () {
    int arr[6] = {30, 40, 50, 60, 70, 80};
    cout << "recorrido secuencial:" << endl;
    for (int a = 0; a < 6; a++) {
        cout << arr[a] << endl;
    }
    return 0;
}