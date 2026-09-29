public class burbuja {
    static void bubbleSort(int[] a) {
        int s = a.length;
        for (int i = 0; i < s; i++) {
            boolean isSwapped = false;
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

    public static void main(String[] args) {
        int[] a = {15, 16, 11, 13, 14};
        System.out.println("antes de ordenar los elementos del array son: ");
        for (int e : a) {
            System.out.print(e + " ");
        }

        bubbleSort(a);
        System.out.println("\ndespues de ordenar los elementos del array son: ");
        for (int e = 0; e < a.length; e++) {
            System.out.print(a[e] + " ");
        }
    }
}
