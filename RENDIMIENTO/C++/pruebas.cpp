#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include <chrono>
#include <cstdlib>
#include <cstdio>
#include <ctime>

const int LIMITE = 60; //segundos maximos que puede tardar un metodo
std::chrono::steady_clock::time_point inicio; //aqui se guarda cuando empezo a ordenar
bool detenido = false; //se pone en true si ya paso el minuto
double porcentaje = 0; //lo que alcanzo a ordenar antes de detenerse

bool seAcaboElTiempo() {
    std::chrono::duration<double> pasado = std::chrono::steady_clock::now() - inicio;
    return pasado.count() > LIMITE; //pa saber si ya paso el minuto
}

// 8 BURBUJA
void bubbleSort(std::vector<int>& a) {
    int s = a.size();
    for (int i = 0; i < s; i++) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = i * 100.0 / s; //las vueltas que alcanzo a dar
            return;
        }
        bool isSwapped = false;
        for (int j = 0; j < s - i - 1; j++) {
            if (a[j] > a[j + 1]) {
                int temp = a[j];
                a[j] = a[j + 1];
                a[j + 1] = temp;
                isSwapped = true;
            }
        }
        if (isSwapped == false) {
            break;
        }
    }
}

// 9 INSERCION
void insertionSort(std::vector<int>& a) {
    for (int i = 1; i < a.size(); i++) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = i * 100.0 / a.size();
            return;
        }
        int temp = a[i];
        int j = i - 1;
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j = j - 1;
        }
        a[j + 1] = temp;
    }
}

// 10 SELECCION
void selection(std::vector<int>& a) {
    for (int i = 0; i < a.size(); i++) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = i * 100.0 / a.size();
            return;
        }
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

// 11 QUICK SORT
int partition(std::vector<int>& a, int l, int h) {
    int pvt = a[h];
    int j = l - 1;
    for (int k = l; k < h; k++) {
        if (a[k] < pvt) {
            j += 1;
            int temp = a[j];
            a[j] = a[k];
            a[k] = temp;
        }
    }
    int temp = a[j + 1];
    a[j + 1] = a[h];
    a[h] = temp;
    return j + 1;
}

void qckSort(std::vector<int>& a) {
    //se hace con una pila en vez de recursion porque con arreglos ordenados
    //o inversos la recursion se hace muy profunda y truena
    int n = a.size();
    std::vector<int> pila; //se guardan de dos en dos, el inicio y el final
    pila.push_back(0);
    pila.push_back(n - 1);
    int listos = 0; //cuantos elementos ya quedaron en su lugar
    while (pila.size() > 0) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = listos * 100.0 / n;
            return;
        }
        int h = pila.back();
        pila.pop_back();
        int l = pila.back();
        pila.pop_back();
        if (l < h) {
            int pi = partition(a, l, h);
            listos += 1; //el pivote ya quedo en su lugar
            pila.push_back(l);
            pila.push_back(pi - 1);
            pila.push_back(pi + 1);
            pila.push_back(h);
        } else if (l == h) {
            listos += 1; //si es un solo elemento ya esta en su lugar
        }
    }
}

// 12 MERGE SORT
int mezclas = 0; //cuantas mezclas lleva, en total son n-1

void merge(std::vector<int>& a, int l, int m, int r) {
    int a1 = m - l + 1;
    int a2 = r - m;
    std::vector<int> L(a1, 0);
    std::vector<int> R(a2, 0);
    for (int j = 0; j < a1; j++) {
        L[j] = a[l + j];
    }
    for (int k = 0; k < a2; k++) {
        R[k] = a[m + 1 + k];
    }
    int i = 0;
    int j = 0;
    int k = l;
    while (i < a1 && j < a2) {
        if (L[i] <= R[j]) {
            a[k] = L[i];
            i = i + 1;
        } else {
            a[k] = R[j];
            j = j + 1;
        }
        k = k + 1;
    }
    while (i < a1) {
        a[k] = L[i];
        i = i + 1;
        k = k + 1;
    }
    while (j < a2) {
        a[k] = R[j];
        j = j + 1;
        k = k + 1;
    }
}

void mergeSort(std::vector<int>& a, int l, int r) {
    if (detenido) {
        return; //si ya se detuvo ya no sigue
    }
    if (seAcaboElTiempo()) {
        detenido = true;
        porcentaje = mezclas * 100.0 / (a.size() - 1);
        return;
    }
    if (l < r) {
        int m = l + (r - l) / 2;
        mergeSort(a, l, m);
        mergeSort(a, m + 1, r);
        if (detenido) {
            return;
        }
        merge(a, l, m, r);
        mezclas += 1;
    }
}

// 13 HEAP SORT
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
        int temp = arr[i];
        arr[i] = arr[largest];
        arr[largest] = temp;
        heapify(arr, n, largest);
    }
}

void heapsort(std::vector<int>& arr) {
    int n = arr.size();
    int total = n / 2 + n - 1; //lo que se tarda en armar el monticulo y en sacar
    int pasos = 0;
    for (int i = n / 2 - 1; i >= 0; i--) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = pasos * 100.0 / total;
            return;
        }
        heapify(arr, n, i);
        pasos += 1;
    }
    for (int i = n - 1; i > 0; i--) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = pasos * 100.0 / total;
            return;
        }
        int temp = arr[0];
        arr[0] = arr[i];
        arr[i] = temp;
        heapify(arr, i, 0);
        pasos += 1;
    }
}

// 14 SHELL SORT
void shellSort(std::vector<int>& inputArr) {
    int size = inputArr.size();
    int totalGaps = 0; //pa saber cuantas vueltas de gap va a dar
    int g = size / 2;
    while (g > 0) {
        totalGaps += 1;
        g = g / 2;
    }
    int gapsHechos = 0;
    int gapSize = size / 2;
    while (gapSize > 0) {
        for (int j = gapSize; j < size; j++) {
            if (j % 1000 == 0 && seAcaboElTiempo()) { //cada 1000 pa no checar el reloj a cada rato
                detenido = true;
                porcentaje = (gapsHechos + (double)j / size) * 100.0 / totalGaps;
                return;
            }
            int val = inputArr[j];
            int k = j;
            while (k >= gapSize && inputArr[k - gapSize] > val) {
                inputArr[k] = inputArr[k - gapSize];
                k = k - gapSize;
            }
            inputArr[k] = val;
        }
        gapsHechos += 1;
        gapSize = gapSize / 2;
    }
}

// 15 BUCKET SORT
void insertion_sort(std::vector<int>& bukt) {
    for (int j = 1; j < bukt.size(); j++) {
        int val = bukt[j];
        int k = j - 1;
        while (k >= 0 && bukt[k] > val) {
            bukt[k + 1] = bukt[k];
            k -= 1;
        }
        bukt[k + 1] = val;
    }
}

void bucket_sort(std::vector<int>& inputArr) {
    int s = inputArr.size();
    int mayor = inputArr[0];
    for (int e : inputArr) {
        if (e > mayor) {
            mayor = e;
        }
    }
    std::vector<std::vector<int>> bucketArr(s);
    for (int j : inputArr) {
        int bi = (int)((long long)j * s / (mayor + 1)); //como son enteros y no decimales se divide entre el mayor, va en long long porque se pasa de int
        bucketArr[bi].push_back(j);
    }
    int hechas = 0;
    for (std::vector<int>& bukt : bucketArr) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = hechas * 100.0 / s;
            return;
        }
        insertion_sort(bukt);
        hechas += 1;
    }
    int idx = 0;
    for (std::vector<int>& bukt : bucketArr) {
        for (int j : bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}

// 16 RADIX SORT
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
    int digitos = std::to_string(max1).size(); //cuantas pasadas va a hacer
    int hechos = 0;
    int exp = 1;
    while (max1 / exp >= 1) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = hechos * 100.0 / digitos;
            return;
        }
        countingSort(arr, exp);
        hechos += 1;
        exp *= 10;
    }
}

void ordenar(std::string metodo, std::vector<int>& a) {
    if (metodo == "Burbuja") {
        bubbleSort(a);
    } else if (metodo == "Insercion") {
        insertionSort(a);
    } else if (metodo == "Seleccion") {
        selection(a);
    } else if (metodo == "Quick Sort") {
        qckSort(a);
    } else if (metodo == "Merge Sort") {
        mergeSort(a, 0, a.size() - 1);
    } else if (metodo == "Heap Sort") {
        heapsort(a);
    } else if (metodo == "Shell Sort") {
        shellSort(a);
    } else if (metodo == "Bucket Sort") {
        bucket_sort(a);
    } else if (metodo == "Radix Sort") {
        radixSort(a);
    }
}

int main() {
    std::vector<std::string> metodos = {"Burbuja", "Insercion", "Seleccion", "Quick Sort", "Merge Sort",
                                        "Heap Sort", "Shell Sort", "Bucket Sort", "Radix Sort"};
    std::vector<int> tamanos = {100, 1000, 10000, 100000, 1000000};
    std::vector<std::string> tipos = {"Aleatorio", "Inverso", "Ordenado"};

    //se hacen los arreglos una sola vez pa que todos los metodos usen los mismos
    std::cout << "Creando los arreglos..." << std::endl;
    srand(time(0));
    std::vector<std::vector<std::vector<int>>> arreglos(tamanos.size(), std::vector<std::vector<int>>(3)); //[tamano][tipo]
    for (int x = 0; x < tamanos.size(); x++) {
        int t = tamanos[x];
        std::vector<int> aleatorio(t);
        std::vector<int> inverso(t);
        std::vector<int> ordenado(t);
        for (int i = 0; i < t; i++) {
            aleatorio[i] = (rand() * 32768 + rand()) % (t + 1); //se juntan dos rand porque uno solo nomas llega a 32767
            inverso[i] = t - i;
            ordenado[i] = i + 1;
        }
        arreglos[x][0] = aleatorio;
        arreglos[x][1] = inverso;
        arreglos[x][2] = ordenado;
    }

    //el txt se guarda en la carpeta desde donde se corre el programa
    std::string ruta = "resultados.txt";
    std::ofstream archivo(ruta);
    archivo << "PRUEBAS DE RENDIMIENTO\n";
    archivo << "Lenguaje: C++\n";
    archivo << "Limite por prueba: " << LIMITE << " segundos\n";
    archivo << "==============================================\n";
    archivo.close();

    for (std::string metodo : metodos) {
        archivo.open(ruta, std::ios::app);
        archivo << "\nMetodo: " << metodo << "\n";
        archivo.close();
        for (int x = 0; x < tamanos.size(); x++) {
            int t = tamanos[x];
            for (int y = 0; y < tipos.size(); y++) {
                std::string tipo = tipos[y];
                std::vector<int> a = arreglos[x][y]; //una copia pa no ordenar el original
                detenido = false;
                porcentaje = 0;
                mezclas = 0;
                std::cout << metodo << " - " << t << " - " << tipo << " ... " << std::flush;
                inicio = std::chrono::steady_clock::now();
                ordenar(metodo, a);
                std::chrono::duration<double> pasado = std::chrono::steady_clock::now() - inicio;
                double tiempo = pasado.count();

                char texto[50]; //pa poner los decimales
                std::string linea = "Lenguaje: C++ | Metodo: " + metodo + " | Tamaño: " + std::to_string(t) + " | Tipo: " + tipo;
                if (detenido) {
                    snprintf(texto, 50, "%.2f", porcentaje);
                    linea = linea + " | Tiempo: mas de " + std::to_string(LIMITE) + " s | Se detuvo al " + texto + "%";
                } else {
                    snprintf(texto, 50, "%.6f", tiempo);
                    linea = linea + " | Tiempo: " + texto + " s";
                }
                std::cout << linea << std::endl;

                //se guarda cada resultado luego luego por si se corta el programa
                archivo.open(ruta, std::ios::app);
                archivo << linea << "\n";
                archivo.close();
            }
        }
    }

    std::cout << "\nListo, los resultados estan en: " << ruta << std::endl;
    return 0;
}
