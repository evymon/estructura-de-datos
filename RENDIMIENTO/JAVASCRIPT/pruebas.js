const fs = require("fs");

const LIMITE = 60; //segundos maximos que puede tardar un metodo
let inicio = 0; //aqui se guarda cuando empezo a ordenar
let detenido = false; //se pone en true si ya paso el minuto
let porcentaje = 0; //lo que alcanzo a ordenar antes de detenerse

function seAcaboElTiempo() {
    return (performance.now() - inicio) / 1000 > LIMITE; //pa saber si ya paso el minuto
}

// 8 BURBUJA
function bubbleSort(a) {
    const s = a.length;
    for (let i = 0; i < s; i++) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = i * 100 / s; //las vueltas que alcanzo a dar
            return;
        }
        let isSwapped = false;
        for (let j = 0; j < s - i - 1; j++) {
            if (a[j] > a[j + 1]) {
                const temp = a[j];
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
function insertionSort(a) {
    for (let i = 1; i < a.length; i++) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = i * 100 / a.length;
            return;
        }
        const temp = a[i];
        let j = i - 1;
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j = j - 1;
        }
        a[j + 1] = temp;
    }
}

// 10 SELECCION
function selection(a) {
    for (let i = 0; i < a.length; i++) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = i * 100 / a.length;
            return;
        }
        let small = i;
        for (let j = i + 1; j < a.length; j++) {
            if (a[small] > a[j]) {
                small = j;
            }
        }
        const temp = a[i];
        a[i] = a[small];
        a[small] = temp;
    }
}

// 11 QUICK SORT
function partition(a, l, h) {
    const pvt = a[h];
    let j = l - 1;
    for (let k = l; k < h; k++) {
        if (a[k] < pvt) {
            j += 1;
            const temp = a[j];
            a[j] = a[k];
            a[k] = temp;
        }
    }
    const temp = a[j + 1];
    a[j + 1] = a[h];
    a[h] = temp;
    return j + 1;
}

function qckSort(a) {
    //se hace con una pila en vez de recursion porque con arreglos ordenados
    //o inversos la recursion se hace muy profunda y truena
    const n = a.length;
    const pila = [0, n - 1]; //se guardan de dos en dos, el inicio y el final
    let listos = 0; //cuantos elementos ya quedaron en su lugar
    while (pila.length > 0) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = listos * 100 / n;
            return;
        }
        const h = pila.pop();
        const l = pila.pop();
        if (l < h) {
            const pi = partition(a, l, h);
            listos += 1; //el pivote ya quedo en su lugar
            pila.push(l);
            pila.push(pi - 1);
            pila.push(pi + 1);
            pila.push(h);
        } else if (l == h) {
            listos += 1; //si es un solo elemento ya esta en su lugar
        }
    }
}

// 12 MERGE SORT
let mezclas = 0; //cuantas mezclas lleva, en total son n-1

function merge(a, l, m, r) {
    const a1 = m - l + 1;
    const a2 = r - m;
    const L = new Array(a1).fill(0);
    const R = new Array(a2).fill(0);
    for (let j = 0; j < a1; j++) {
        L[j] = a[l + j];
    }
    for (let k = 0; k < a2; k++) {
        R[k] = a[m + 1 + k];
    }
    let i = 0;
    let j = 0;
    let k = l;
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

function mergeSort(a, l, r) {
    if (detenido) {
        return; //si ya se detuvo ya no sigue
    }
    if (seAcaboElTiempo()) {
        detenido = true;
        porcentaje = mezclas * 100 / (a.length - 1);
        return;
    }
    if (l < r) {
        const m = l + Math.floor((r - l) / 2);
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
function heapify(arr, n, i) {
    let largest = i;
    const left = 2 * i + 1;
    const right = 2 * i + 2;
    if (left < n && arr[left] > arr[largest]) {
        largest = left;
    }
    if (right < n && arr[right] > arr[largest]) {
        largest = right;
    }
    if (largest != i) {
        const temp = arr[i];
        arr[i] = arr[largest];
        arr[largest] = temp;
        heapify(arr, n, largest);
    }
}

function heapsort(arr) {
    const n = arr.length;
    const total = Math.floor(n / 2) + n - 1; //lo que se tarda en armar el monticulo y en sacar
    let pasos = 0;
    for (let i = Math.floor(n / 2) - 1; i >= 0; i--) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = pasos * 100 / total;
            return;
        }
        heapify(arr, n, i);
        pasos += 1;
    }
    for (let i = n - 1; i > 0; i--) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = pasos * 100 / total;
            return;
        }
        const temp = arr[0];
        arr[0] = arr[i];
        arr[i] = temp;
        heapify(arr, i, 0);
        pasos += 1;
    }
}

// 14 SHELL SORT
function shellSort(inputArr) {
    const size = inputArr.length;
    let totalGaps = 0; //pa saber cuantas vueltas de gap va a dar
    let g = Math.floor(size / 2);
    while (g > 0) {
        totalGaps += 1;
        g = Math.floor(g / 2);
    }
    let gapsHechos = 0;
    let gapSize = Math.floor(size / 2);
    while (gapSize > 0) {
        for (let j = gapSize; j < size; j++) {
            if (j % 1000 == 0 && seAcaboElTiempo()) { //cada 1000 pa no checar el reloj a cada rato
                detenido = true;
                porcentaje = (gapsHechos + j / size) * 100 / totalGaps;
                return;
            }
            const val = inputArr[j];
            let k = j;
            while (k >= gapSize && inputArr[k - gapSize] > val) {
                inputArr[k] = inputArr[k - gapSize];
                k = k - gapSize;
            }
            inputArr[k] = val;
        }
        gapsHechos += 1;
        gapSize = Math.floor(gapSize / 2);
    }
}

// 15 BUCKET SORT
function insertion_sort(bukt) {
    for (let j = 1; j < bukt.length; j++) {
        const val = bukt[j];
        let k = j - 1;
        while (k >= 0 && bukt[k] > val) {
            bukt[k + 1] = bukt[k];
            k -= 1;
        }
        bukt[k + 1] = val;
    }
}

function bucket_sort(inputArr) {
    const s = inputArr.length;
    let mayor = inputArr[0];
    for (const e of inputArr) {
        if (e > mayor) {
            mayor = e;
        }
    }
    const bucketArr = [];
    for (let i = 0; i < s; i++) {
        bucketArr.push([]);
    }
    for (const j of inputArr) {
        const bi = Math.floor(j * s / (mayor + 1)); //como son enteros y no decimales se divide entre el mayor
        bucketArr[bi].push(j);
    }
    let hechas = 0;
    for (const bukt of bucketArr) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = hechas * 100 / s;
            return;
        }
        insertion_sort(bukt);
        hechas += 1;
    }
    let idx = 0;
    for (const bukt of bucketArr) {
        for (const j of bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}

// 16 RADIX SORT
function countingSort(arr, exp) {
    const s = arr.length;
    const outputArray = new Array(s).fill(0);
    const countArray = new Array(10).fill(0);
    for (let j = 0; j < s; j++) {
        const idx = Math.floor(arr[j] / exp);
        countArray[idx % 10] += 1;
    }
    for (let j = 1; j < 10; j++) {
        countArray[j] += countArray[j - 1];
    }
    let j = s - 1;
    while (j >= 0) {
        const idx = Math.floor(arr[j] / exp);
        outputArray[countArray[idx % 10] - 1] = arr[j];
        countArray[idx % 10] -= 1;
        j -= 1;
    }
    for (j = 0; j < arr.length; j++) {
        arr[j] = outputArray[j];
    }
}

function radixSort(arr) {
    let max1 = arr[0]; //con un ciclo porque Math.max con un millon truena
    for (const e of arr) {
        if (e > max1) {
            max1 = e;
        }
    }
    const digitos = String(max1).length; //cuantas pasadas va a hacer
    let hechos = 0;
    let exp = 1;
    while (max1 / exp >= 1) {
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = hechos * 100 / digitos;
            return;
        }
        countingSort(arr, exp);
        hechos += 1;
        exp *= 10;
    }
}

function ordenar(metodo, a) {
    if (metodo == "Burbuja") {
        bubbleSort(a);
    } else if (metodo == "Insercion") {
        insertionSort(a);
    } else if (metodo == "Seleccion") {
        selection(a);
    } else if (metodo == "Quick Sort") {
        qckSort(a);
    } else if (metodo == "Merge Sort") {
        mergeSort(a, 0, a.length - 1);
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


const metodos = ["Burbuja", "Insercion", "Seleccion", "Quick Sort", "Merge Sort",
                 "Heap Sort", "Shell Sort", "Bucket Sort", "Radix Sort"];
const tamanos = [100, 1000, 10000, 100000, 1000000];
const tipos = ["Aleatorio", "Inverso", "Ordenado"];

//se hacen los arreglos una sola vez pa que todos los metodos usen los mismos
console.log("Creando los arreglos...");
const arreglos = {};
for (const t of tamanos) {
    const aleatorio = [];
    for (let i = 0; i < t; i++) {
        aleatorio.push(Math.floor(Math.random() * (t + 1)));
    }
    const inverso = [];
    for (let i = t; i > 0; i--) {
        inverso.push(i);
    }
    const ordenado = [];
    for (let i = 1; i <= t; i++) {
        ordenado.push(i);
    }
    arreglos[t + "Aleatorio"] = aleatorio;
    arreglos[t + "Inverso"] = inverso;
    arreglos[t + "Ordenado"] = ordenado;
}

//el txt se guarda en la misma carpeta que este archivo
const ruta = __dirname + "/resultados.txt";
fs.writeFileSync(ruta, "PRUEBAS DE RENDIMIENTO\n");
fs.appendFileSync(ruta, "Lenguaje: JavaScript\n");
fs.appendFileSync(ruta, "Limite por prueba: " + LIMITE + " segundos\n");
fs.appendFileSync(ruta, "==============================================\n");

for (const metodo of metodos) {
    fs.appendFileSync(ruta, "\nMetodo: " + metodo + "\n");
    for (const t of tamanos) {
        for (const tipo of tipos) {
            const a = arreglos[t + tipo].slice(); //una copia pa no ordenar el original
            detenido = false;
            porcentaje = 0;
            mezclas = 0;
            process.stdout.write(metodo + " - " + t + " - " + tipo + " ... ");
            inicio = performance.now();
            ordenar(metodo, a);
            const tiempo = (performance.now() - inicio) / 1000;

            let linea = "Lenguaje: JavaScript | Metodo: " + metodo + " | Tamaño: " + t + " | Tipo: " + tipo;
            if (detenido) {
                linea = linea + " | Tiempo: mas de " + LIMITE + " s | Se detuvo al " + porcentaje.toFixed(2) + "%";
            } else {
                linea = linea + " | Tiempo: " + tiempo.toFixed(6) + " s";
            }
            console.log(linea);

            //se guarda cada resultado luego luego por si se corta el programa
            fs.appendFileSync(ruta, linea + "\n");
        }
    }
}

console.log("\nListo, los resultados estan en: " + ruta);
