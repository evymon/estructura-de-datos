public class shellsort {
    static void displayArr(int[] inputArr) {
        for (int k : inputArr) {
            System.out.print(k + " ");
        }
        System.out.println();
    }

    int sort(int[] inputArr) {
        int size = inputArr.length;
        int gapSize = size / 2;
        while (gapSize > 0) {
            for (int j = gapSize; j < size; j++) {
                int val = inputArr[j];
                int k = j;
                while (k >= gapSize && inputArr[k - gapSize] > val) {
                    inputArr[k] = inputArr[k - gapSize];
                    k = k - gapSize;
                }
                inputArr[k] = val;
            }
            gapSize = gapSize / 2;
        }
        return 0;
    }

    public static void main(String[] args) {
        int[] inputArr = {36, 34, 43, 11, 15, 20, 28, 45};
        System.out.println("Arreglo antes de ser ordenado: ");
        shellsort.displayArr(inputArr);
        shellsort obj = new shellsort();
        obj.sort(inputArr);
        System.out.println("Arreglo despues de ser ordenado: ");
        shellsort.displayArr(inputArr);
    }
}
