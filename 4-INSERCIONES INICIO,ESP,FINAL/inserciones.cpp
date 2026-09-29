#include <iostream>
#include <vector>

using namespace std;
void imprimir(const vector<int>& v) {
    for (int d : v) {
        cout << d << " ";
    }
    cout << endl;
}

int main(){
    int ele= 52;

//insercion al principio
vector<int> arr1= {11,21,31,41,51,61};
cout<<"antes: "; imprimir(arr1);
arr1.insert(arr1.begin(), ele);
cout<<"despues: "; imprimir(arr1);

//insercion especifica
vector<int> arr2= {11,21,31,41,51,61};
arr2.insert(arr2.begin()+3, ele);
cout<<"despues: "; imprimir(arr2);

//insercion al final
vector<int> arr3= {11,21,31,41,51,61};
int eleFinal= 72;
arr3.push_back(eleFinal);
cout<<"despues: "; imprimir(arr3);
return 0;
}