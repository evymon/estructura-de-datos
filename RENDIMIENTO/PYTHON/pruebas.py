import random
import time
import os

LIMITE = 60 #segundos maximos que puede tardar un metodo
inicio = 0 #aqui se guarda cuando empezo a ordenar
detenido = False #se pone en True si ya paso el minuto
porcentaje = 0 #lo que alcanzo a ordenar antes de detenerse


def seAcaboElTiempo():
    return time.time() - inicio > LIMITE #pa saber si ya paso el minuto


# 8 BURBUJA
def bubbleSort(a):
    global detenido, porcentaje
    s = len(a)
    for i in range(s):
        if seAcaboElTiempo():
            detenido = True
            porcentaje = i * 100 / s #las vueltas que alcanzo a dar
            return
        isSwapped = False
        for j in range(0, s - i - 1):
            if a[j] > a[j + 1]:
                a[j], a[j + 1] = a[j + 1], a[j]
                isSwapped = True
        if isSwapped == False:
            break


# 9 INSERCION
def insertionSort(a):
    global detenido, porcentaje
    for i in range(1, len(a)):
        if seAcaboElTiempo():
            detenido = True
            porcentaje = i * 100 / len(a)
            return
        temp = a[i]
        j = i - 1
        while j >= 0 and temp < a[j]:
            a[j + 1] = a[j]
            j = j - 1
        a[j + 1] = temp


# 10 SELECCION
def selection(a):
    global detenido, porcentaje
    for i in range(len(a)):
        if seAcaboElTiempo():
            detenido = True
            porcentaje = i * 100 / len(a)
            return
        small = i
        for j in range(i + 1, len(a)):
            if a[small] > a[j]:
                small = j
        a[i], a[small] = a[small], a[i]


# 11 QUICK SORT
def partition(a, l, h):
    pvt = a[h]
    j = l - 1
    for k in range(l, h):
        if a[k] < pvt:
            j += 1
            a[j], a[k] = a[k], a[j]
    a[j + 1], a[h] = a[h], a[j + 1]
    return j + 1

def qckSort(a):
    #se hace con una pila en vez de recursion porque con arreglos ordenados
    #o inversos la recursion se hace muy profunda y python truena
    global detenido, porcentaje
    n = len(a)
    pila = [0, n - 1] #se guardan de dos en dos, el inicio y el final
    listos = 0 #cuantos elementos ya quedaron en su lugar
    while len(pila) > 0:
        if seAcaboElTiempo():
            detenido = True
            porcentaje = listos * 100 / n
            return
        h = pila.pop()
        l = pila.pop()
        if l < h:
            pi = partition(a, l, h)
            listos += 1 #el pivote ya quedo en su lugar
            pila.append(l)
            pila.append(pi - 1)
            pila.append(pi + 1)
            pila.append(h)
        elif l == h:
            listos += 1 #si es un solo elemento ya esta en su lugar


# 12 MERGE SORT
mezclas = 0 #cuantas mezclas lleva, en total son n-1

def merge(a, l, m, r):
    a1 = m - l + 1
    a2 = r - m
    L = [0] * (a1)
    R = [0] * (a2)
    for j in range(0, a1):
        L[j] = a[l + j]
    for k in range(0, a2):
        R[k] = a[m + 1 + k]
    i = 0
    j = 0
    k = l
    while i < a1 and j < a2:
        if L[i] <= R[j]:
            a[k] = L[i]
            i = i + 1
        else:
            a[k] = R[j]
            j = j + 1
        k = k + 1
    while i < a1:
        a[k] = L[i]
        i = i + 1
        k = k + 1
    while j < a2:
        a[k] = R[j]
        j = j + 1
        k = k + 1

def mergeSort(a, l, r):
    global detenido, porcentaje, mezclas
    if detenido:
        return #si ya se detuvo ya no sigue
    if seAcaboElTiempo():
        detenido = True
        porcentaje = mezclas * 100 / (len(a) - 1)
        return
    if l < r:
        m = l + (r - l) // 2
        mergeSort(a, l, m)
        mergeSort(a, m + 1, r)
        if detenido:
            return
        merge(a, l, m, r)
        mezclas += 1


# 13 HEAP SORT
def heapify(arr, n, i):
    largest = i
    left = 2 * i + 1
    right = 2 * i + 2
    if left < n and arr[left] > arr[largest]:
        largest = left
    if right < n and arr[right] > arr[largest]:
        largest = right
    if largest != i:
        arr[i], arr[largest] = arr[largest], arr[i]
        heapify(arr, n, largest)

def heapsort(arr):
    global detenido, porcentaje
    n = len(arr)
    total = n // 2 + n - 1 #lo que se tarda en armar el monticulo y en sacar
    pasos = 0
    for i in range(n // 2 - 1, -1, -1):
        if seAcaboElTiempo():
            detenido = True
            porcentaje = pasos * 100 / total
            return
        heapify(arr, n, i)
        pasos += 1
    for i in range(n - 1, 0, -1):
        if seAcaboElTiempo():
            detenido = True
            porcentaje = pasos * 100 / total
            return
        arr[0], arr[i] = arr[i], arr[0]
        heapify(arr, i, 0)
        pasos += 1


# 14 SHELL SORT
def shellSort(inputArr):
    global detenido, porcentaje
    size = len(inputArr)
    totalGaps = 0 #pa saber cuantas vueltas de gap va a dar
    g = size // 2
    while g > 0:
        totalGaps += 1
        g = g // 2
    gapsHechos = 0
    gapSize = size // 2
    while gapSize > 0:
        for j in range(gapSize, size):
            if j % 1000 == 0 and seAcaboElTiempo(): #cada 1000 pa no checar el reloj a cada rato
                detenido = True
                porcentaje = (gapsHechos + j / size) * 100 / totalGaps
                return
            val = inputArr[j]
            k = j
            while k >= gapSize and inputArr[k - gapSize] > val:
                inputArr[k] = inputArr[k - gapSize]
                k = k - gapSize
            inputArr[k] = val
        gapsHechos += 1
        gapSize = gapSize // 2


# 15 BUCKET SORT
def insertion_sort(bukt):
    for j in range(1, len(bukt)):
        val = bukt[j]
        k = j - 1
        while k >= 0 and bukt[k] > val:
            bukt[k + 1] = bukt[k]
            k -= 1
        bukt[k + 1] = val

def bucket_sort(inputArr):
    global detenido, porcentaje
    s = len(inputArr)
    mayor = max(inputArr)
    bucketArr = [[] for _ in range(s)]
    for j in inputArr:
        bi = j * s // (mayor + 1) #como son enteros y no decimales se divide entre el mayor
        bucketArr[bi].append(j)
    hechas = 0
    for bukt in bucketArr:
        if seAcaboElTiempo():
            detenido = True
            porcentaje = hechas * 100 / s
            return
        insertion_sort(bukt)
        hechas += 1
    idx = 0
    for bukt in bucketArr:
        for j in bukt:
            inputArr[idx] = j
            idx += 1


# 16 RADIX SORT
def countingSort(arr, exp):
    s = len(arr)
    outputArray = [0] * (s)
    countArray = [0] * (10)
    for j in range(0, s):
        idx = arr[j] // exp
        countArray[idx % 10] += 1
    for j in range(1, 10):
        countArray[j] += countArray[j - 1]
    j = s - 1
    while j >= 0:
        idx = arr[j] // exp
        outputArray[countArray[idx % 10] - 1] = arr[j]
        countArray[idx % 10] -= 1
        j -= 1
    for j in range(0, len(arr)):
        arr[j] = outputArray[j]

def radixSort(arr):
    global detenido, porcentaje
    max1 = max(arr)
    digitos = len(str(max1)) #cuantas pasadas va a hacer
    hechos = 0
    exp = 1
    while max1 / exp >= 1:
        if seAcaboElTiempo():
            detenido = True
            porcentaje = hechos * 100 / digitos
            return
        countingSort(arr, exp)
        hechos += 1
        exp *= 10


def ordenar(metodo, a):
    if metodo == "Burbuja":
        bubbleSort(a)
    elif metodo == "Insercion":
        insertionSort(a)
    elif metodo == "Seleccion":
        selection(a)
    elif metodo == "Quick Sort":
        qckSort(a)
    elif metodo == "Merge Sort":
        mergeSort(a, 0, len(a) - 1)
    elif metodo == "Heap Sort":
        heapsort(a)
    elif metodo == "Shell Sort":
        shellSort(a)
    elif metodo == "Bucket Sort":
        bucket_sort(a)
    elif metodo == "Radix Sort":
        radixSort(a)


metodos = ["Burbuja", "Insercion", "Seleccion", "Quick Sort", "Merge Sort",
           "Heap Sort", "Shell Sort", "Bucket Sort", "Radix Sort"]
tamanos = [100, 1000, 10000, 100000, 1000000]
tipos = ["Aleatorio", "Inverso", "Ordenado"]

#se hacen los arreglos una sola vez pa que todos los metodos usen los mismos
print("Creando los arreglos...")
arreglos = {}
for t in tamanos:
    aleatorio = []
    for i in range(t):
        aleatorio.append(random.randint(0, t))
    inverso = []
    for i in range(t, 0, -1):
        inverso.append(i)
    ordenado = []
    for i in range(1, t + 1):
        ordenado.append(i)
    arreglos[str(t) + "Aleatorio"] = aleatorio
    arreglos[str(t) + "Inverso"] = inverso
    arreglos[str(t) + "Ordenado"] = ordenado

#el txt se guarda en la misma carpeta que este archivo
ruta = os.path.join(os.path.dirname(os.path.abspath(__file__)), "resultados.txt")
archivo = open(ruta, "w", encoding="utf-8")
archivo.write("PRUEBAS DE RENDIMIENTO\n")
archivo.write("Lenguaje: Python\n")
archivo.write("Limite por prueba: " + str(LIMITE) + " segundos\n")
archivo.write("==============================================\n")
archivo.close()

for metodo in metodos:
    archivo = open(ruta, "a", encoding="utf-8")
    archivo.write("\nMetodo: " + metodo + "\n")
    archivo.close()
    for tipo in tipos:
        for t in tamanos:
            a = arreglos[str(t) + tipo].copy() #una copia pa no ordenar el original
            detenido = False
            porcentaje = 0
            mezclas = 0
            print(metodo, "-", t, "-", tipo, "...", end=" ", flush=True)
            inicio = time.time()
            ordenar(metodo, a)
            tiempo = time.time() - inicio

            linea = "Lenguaje: Python | Metodo: " + metodo + " | Tamaño: " + str(t) + " | Tipo: " + tipo
            if detenido:
                linea = linea + " | Tiempo: mas de " + str(LIMITE) + " s | Se detuvo al " + "%.2f" % porcentaje + "%"
            else:
                linea = linea + " | Tiempo: " + "%.6f" % tiempo + " s"
            print(linea)

            #se guarda cada resultado luego luego por si se corta el programa
            archivo = open(ruta, "a", encoding="utf-8")
            archivo.write(linea + "\n")
            archivo.close()

print("\nListo, los resultados estan en:", ruta)
