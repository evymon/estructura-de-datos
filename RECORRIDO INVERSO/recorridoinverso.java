public class recorridoinverso {
public static void main(String[] args) {
    int[] numeros = {20,30, 40, 50, 60, 70,};
    int[] numerosInversos = new int[numeros.length];
    int a = 0;
    for (int b = numeros.length - 1; b >= 0; b--) {
        numerosInversos[a] = numeros[b];
        a++;
    }
    System.out.print("e recorrido inverso del array es: ");
    for (int i = 0; i < numerosInversos.length; i++) {
        System.out.print(numerosInversos[i] + " ");
    }
}
}   