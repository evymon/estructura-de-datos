#include <iostream>
using namespace std;

int main() {
    int ar[] = {30, 40, 50, 60, 70, 80};
    int n = sizeof(ar) / sizeof(ar[0]);

    cout << "El recorrido inverso del array es: ";
    for (int d = n - 1; d >= 0; d--) {
        cout << ar[d] << " ";
    }
    cout << endl;

    return 0;
}