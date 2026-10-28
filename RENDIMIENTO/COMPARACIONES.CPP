#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include <cstdlib>
#include <cstdio>

// aqui se guardan todos los resultados de los txt, cada posicion es una prueba
std::vector<std::string> lenguajes; //lenguaje de cada prueba
std::vector<std::string> metodos; //metodo de cada prueba
std::vector<int> tamanos; //tamaño del arreglo
std::vector<std::string> tipos; //aleatorio, inverso u ordenado
std::vector<double> tiempos; //el tiempo en segundos, -1 si paso del minuto
std::vector<double> porcentajes; //lo que alcanzo a ordenar si se detuvo

// las opciones que puede escoger el usuario
std::vector<std::string> listaLenguajes = {"Python", "Java", "JavaScript", "C++", "C#"};
std::vector<std::string> listaMetodos = {"Burbuja", "Insercion", "Seleccion", "Quick Sort", "Merge Sort",
                                         "Heap Sort", "Shell Sort", "Bucket Sort", "Radix Sort"};
std::vector<int> listaTamanos = {100, 1000, 10000, 100000, 1000000};
std::vector<std::string> listaTipos = {"Aleatorio", "Inverso", "Ordenado"};

// saca lo que esta despues de los ": " en un pedazo como "Tipo: Inverso"
std::string valorDe(std::string pedazo) {
    int pos = pedazo.find(": ");
    return pedazo.substr(pos + 2);
}

// lee un archivo de resultados y guarda cada linea de prueba
void leerArchivo(std::string ruta) {
    std::ifstream archivo(ruta);
    if (!archivo.is_open()) {
        std::cout << "No se pudo abrir " << ruta << std::endl;
        return;
    }
    std::string linea;
    while (std::getline(archivo, linea)) {
        if (linea.size() > 0 && linea[linea.size() - 1] == '\r') {
            linea = linea.substr(0, linea.size() - 1); //quitarle el enter de windows
        }
        if (linea.find("Lenguaje: ") != 0 || linea.find(" | ") == std::string::npos) {
            continue; //no es linea de prueba, nimodo
        }

        // partir la linea en pedazos separados por " | "
        std::vector<std::string> pedazos;
        int inicio = 0;
        int pos = linea.find(" | ");
        while (pos != std::string::npos) {
            pedazos.push_back(linea.substr(inicio, pos - inicio));
            inicio = pos + 3;
            pos = linea.find(" | ", inicio);
        }
        pedazos.push_back(linea.substr(inicio));

        lenguajes.push_back(valorDe(pedazos[0]));
        metodos.push_back(valorDe(pedazos[1]));
        tamanos.push_back(std::atoi(valorDe(pedazos[2]).c_str()));
        tipos.push_back(valorDe(pedazos[3]));

        std::string tiempo = valorDe(pedazos[4]);
        if (tiempo.find("mas de") == 0) {
            tiempos.push_back(-1); //no termino en el minuto
            std::string detuvo = pedazos[5]; //"Se detuvo al 0.88%"
            int espacio = detuvo.rfind(' ');
            porcentajes.push_back(std::atof(detuvo.substr(espacio + 1).c_str()));
        } else {
            tiempos.push_back(std::atof(tiempo.c_str()));
            porcentajes.push_back(100);
        }
    }
    archivo.close();
}

// pa ordenar: los que se pasaron del minuto van al final y el que avanzo menos es el peor
double valorParaOrdenar(int i) {
    if (tiempos[i] < 0) {
        return 1000 + (100 - porcentajes[i]);
    }
    return tiempos[i];
}

// rellena con espacios pa que las columnas se vean derechitas
std::string rellenar(std::string texto, int ancho) {
    while (texto.size() < ancho) {
        texto = texto + " ";
    }
    return texto;
}

std::string textoTiempo(int i) {
    char buffer[50];
    if (tiempos[i] < 0) {
        std::sprintf(buffer, "mas de 60 s (llego al %.2f%%)", porcentajes[i]);
    } else {
        std::sprintf(buffer, "%.6f s", tiempos[i]);
    }
    return buffer;
}

std::string nombrePrueba(int i) {
    return rellenar(lenguajes[i], 11) + rellenar(metodos[i], 12) + rellenar(std::to_string(tamanos[i]), 8) + rellenar(tipos[i], 10);
}

// pide un numero entre 0 y max
int pedirOpcion(int max) {
    int op;
    std::cout << "Opcion: ";
    std::cin >> op;
    while (std::cin.fail() || op < 0 || op > max) {
        if (std::cin.eof()) {
            return 0; //ya no hay nada que leer
        }
        std::cin.clear();
        std::cin.ignore(1000, '\n');
        std::cout << "Opcion no valida, intenta otra vez: ";
        std::cin >> op;
    }
    return op;
}

// muestra una lista de textos y regresa cual escogio (0 = todos)
int escoger(std::string titulo, std::vector<std::string> opciones) {
    std::cout << "\n" << titulo << std::endl;
    std::cout << "  0. Todos" << std::endl;
    for (int i = 0; i < opciones.size(); i++) {
        std::cout << "  " << i + 1 << ". " << opciones[i] << std::endl;
    }
    return pedirOpcion(opciones.size());
}

// grafica de barras con # (el mas tardado tiene la barra mas larga)
void graficar(std::vector<int> pruebas) {
    double mayor = 0;
    for (int k = 0; k < pruebas.size(); k++) {
        int i = pruebas[k];
        if (tiempos[i] < 0) {
            mayor = 60; //si alguno se paso, el tope es el minuto
        } else if (tiempos[i] > mayor) {
            mayor = tiempos[i];
        }
    }
    if (mayor == 0) {
        mayor = 1; //pa no dividir entre cero
    }

    std::cout << "\n--- GRAFICA (entre mas larga la barra, mas tardo) ---" << std::endl;
    for (int k = 0; k < pruebas.size(); k++) {
        int i = pruebas[k];
        int largo;
        if (tiempos[i] < 0) {
            largo = 40;
        } else {
            largo = tiempos[i] / mayor * 40;
        }
        if (largo < 1) {
            largo = 1; //que se vea aunque sea un cuadrito
        }
        std::string barra = "";
        for (int j = 0; j < largo; j++) {
            barra = barra + "#";
        }
        std::cout << rellenar(std::to_string(k + 1) + ".", 5) << nombrePrueba(i) << "|" << rellenar(barra, 41) << textoTiempo(i) << std::endl;
    }
}

// opcion 1: el usuario escoge los filtros y se comparan las pruebas que queden
void comparacionPersonalizada() {
    int len = escoger("Que lenguaje quieres comparar?", listaLenguajes);
    int met = escoger("Que metodo?", listaMetodos);

    std::vector<std::string> textosTamanos;
    for (int i = 0; i < listaTamanos.size(); i++) {
        textosTamanos.push_back(std::to_string(listaTamanos[i]));
    }
    int tam = escoger("Que tamano de arreglo?", textosTamanos);
    int tip = escoger("Que tipo de arreglo?", listaTipos);

    // juntar las pruebas que cumplen con lo que escogio
    std::vector<int> pruebas;
    for (int i = 0; i < tiempos.size(); i++) {
        if (len != 0 && lenguajes[i] != listaLenguajes[len - 1]) continue;
        if (met != 0 && metodos[i] != listaMetodos[met - 1]) continue;
        if (tam != 0 && tamanos[i] != listaTamanos[tam - 1]) continue;
        if (tip != 0 && tipos[i] != listaTipos[tip - 1]) continue;
        pruebas.push_back(i);
    }

    if (pruebas.size() == 0) {
        std::cout << "\nNo hay pruebas con esa combinacion" << std::endl;
        return;
    }

    // ordenar de la mas rapida a la mas lenta con burbuja
    for (int a = 0; a < pruebas.size(); a++) {
        for (int b = 0; b < pruebas.size() - a - 1; b++) {
            if (valorParaOrdenar(pruebas[b]) > valorParaOrdenar(pruebas[b + 1])) {
                int temp = pruebas[b];
                pruebas[b] = pruebas[b + 1];
                pruebas[b + 1] = temp;
            }
        }
    }

    graficar(pruebas);

    int mejor = pruebas[0];
    int peor = pruebas[pruebas.size() - 1];
    int terminaron = 0;
    for (int k = 0; k < pruebas.size(); k++) {
        if (tiempos[pruebas[k]] >= 0) {
            terminaron++;
        }
    }

    std::cout << "\n--- RESUMEN ---" << std::endl;
    std::cout << "Pruebas comparadas: " << pruebas.size() << std::endl;
    std::cout << "Terminaron antes del minuto: " << terminaron << std::endl;
    std::cout << "No terminaron: " << pruebas.size() - terminaron << std::endl;
    std::cout << "MEJOR: " << metodos[mejor] << " en " << lenguajes[mejor] << " (" << tamanos[mejor] << ", "
              << tipos[mejor] << ") con " << textoTiempo(mejor) << std::endl;
    std::cout << "PEOR:  " << metodos[peor] << " en " << lenguajes[peor] << " (" << tamanos[peor] << ", "
              << tipos[peor] << ") con " << textoTiempo(peor) << std::endl;
}

// busca la posicion de la prueba con esos datos, -1 si no esta
int buscarPrueba(std::string len, std::string met, int tam, std::string tip) {
    for (int i = 0; i < tiempos.size(); i++) {
        if (lenguajes[i] == len && metodos[i] == met && tamanos[i] == tam && tipos[i] == tip) {
            return i;
        }
    }
    return -1;
}

// dibuja las barras de las victorias
void graficarVictorias(std::vector<std::string> nombres, std::vector<int> victorias, int total) {
    int mayor = 1;
    for (int i = 0; i < victorias.size(); i++) {
        if (victorias[i] > mayor) {
            mayor = victorias[i];
        }
    }
    for (int i = 0; i < nombres.size(); i++) {
        int largo = victorias[i] * 40 / mayor; //el que gano mas tiene 40 cuadritos
        std::string barra = "";
        for (int j = 0; j < largo; j++) {
            barra = barra + "#";
        }
        std::cout << rellenar(nombres[i], 12) << "|" << barra << " " << victorias[i] << " de " << total << std::endl;
    }
}

// opcion 2: cuenta cuantas veces cada lenguaje fue el mas rapido
void victoriasPorLenguaje() {
    std::vector<int> victorias = {0, 0, 0, 0, 0};
    int total = 0;
    // para cada metodo, tamaño y tipo se ve que lenguaje gano
    for (int m = 0; m < listaMetodos.size(); m++) {
        for (int t = 0; t < listaTamanos.size(); t++) {
            for (int p = 0; p < listaTipos.size(); p++) {
                int ganador = -1;
                for (int l = 0; l < listaLenguajes.size(); l++) {
                    int i = buscarPrueba(listaLenguajes[l], listaMetodos[m], listaTamanos[t], listaTipos[p]);
                    if (i == -1) continue;
                    if (ganador == -1 || valorParaOrdenar(i) < valorParaOrdenar(ganador)) {
                        ganador = i;
                    }
                }
                if (ganador != -1) {
                    for (int l = 0; l < listaLenguajes.size(); l++) {
                        if (lenguajes[ganador] == listaLenguajes[l]) {
                            victorias[l]++;
                        }
                    }
                    total++;
                }
            }
        }
    }
    std::cout << "\n--- VECES QUE CADA LENGUAJE FUE EL MAS RAPIDO ---" << std::endl;
    std::cout << "(misma prueba: mismo metodo, tamano y tipo)" << std::endl;
    graficarVictorias(listaLenguajes, victorias, total);
}

// opcion 3: cuenta cuantas veces cada metodo fue el mas rapido
void victoriasPorMetodo() {
    std::vector<int> victorias = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    int total = 0;
    // para cada lenguaje, tamaño y tipo se ve que metodo gano
    for (int l = 0; l < listaLenguajes.size(); l++) {
        for (int t = 0; t < listaTamanos.size(); t++) {
            for (int p = 0; p < listaTipos.size(); p++) {
                int ganador = -1;
                for (int m = 0; m < listaMetodos.size(); m++) {
                    int i = buscarPrueba(listaLenguajes[l], listaMetodos[m], listaTamanos[t], listaTipos[p]);
                    if (i == -1) continue;
                    if (ganador == -1 || valorParaOrdenar(i) < valorParaOrdenar(ganador)) {
                        ganador = i;
                    }
                }
                if (ganador != -1) {
                    for (int m = 0; m < listaMetodos.size(); m++) {
                        if (metodos[ganador] == listaMetodos[m]) {
                            victorias[m]++;
                        }
                    }
                    total++;
                }
            }
        }
    }
    std::cout << "\n--- VECES QUE CADA METODO FUE EL MAS RAPIDO ---" << std::endl;
    std::cout << "(mismo lenguaje, tamano y tipo)" << std::endl;
    graficarVictorias(listaMetodos, victorias, total);
}

int main() {
    // cada lenguaje tiene su carpeta con su resultados.txt
    leerArchivo("PYTHON/resultados.txt");
    leerArchivo("JAVA/resultados.txt");
    leerArchivo("JAVASCRIPT/resultados.txt");
    leerArchivo("C++/resultados.txt");
    leerArchivo("C#/resultados.txt");

    if (tiempos.size() == 0) {
        std::cout << "No se leyo ningun resultado, corre el programa desde la carpeta RENDIMIENTO" << std::endl;
        return 0;
    }
    std::cout << "Se leyeron " << tiempos.size() << " pruebas" << std::endl;

    int opcion = -1;
    while (opcion != 0) {
        std::cout << "\n==============================================" << std::endl;
        std::cout << "      COMPARACION DE METODOS DE ORDENAMIENTO" << std::endl;
        std::cout << "==============================================" << std::endl;
        std::cout << "1. Comparar (escoges lenguaje, metodo, tamano y tipo)" << std::endl;
        std::cout << "2. Que lenguaje gano mas veces" << std::endl;
        std::cout << "3. Que metodo gano mas veces" << std::endl;
        std::cout << "0. Salir" << std::endl;
        opcion = pedirOpcion(3);

        if (opcion == 1) {
            comparacionPersonalizada();
        } else if (opcion == 2) {
            victoriasPorLenguaje();
        } else if (opcion == 3) {
            victoriasPorMetodo();
        }
    }
    std::cout << "Adios!" << std::endl;
    return 0;
}
