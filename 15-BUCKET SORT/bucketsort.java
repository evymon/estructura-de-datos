import java.util.ArrayList;

public class bucketsort {
    static void insertion_sort(ArrayList<Double> bukt) {
        for (int j = 1; j < bukt.size(); j++) {
            double val = bukt.get(j);
            int k = j - 1;
            while (k >= 0 && bukt.get(k) > val) {
                bukt.set(k + 1, bukt.get(k));
                k -= 1;
            }
            bukt.set(k + 1, val);
        }
    }

    static void bucket_sort(double[] inputArr) {
        int s = inputArr.length;
        ArrayList<ArrayList<Double>> bucketArr = new ArrayList<>();
        for (int i = 0; i < s; i++) {
            bucketArr.add(new ArrayList<>());
        }
        for (double j : inputArr) {
            int bi = (int) (s * j);
            bucketArr.get(bi).add(j);
        }
        for (ArrayList<Double> bukt : bucketArr) {
            insertion_sort(bukt);
        }
        int idx = 0;
        for (ArrayList<Double> bukt : bucketArr) {
            for (double j : bukt) {
                inputArr[idx] = j;
                idx += 1;
            }
        }
    }

    public static void main(String[] args) {
        double[] inputArr = {0.77, 0.16, 0.38, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67};
        System.out.println("Arreglo antes de ordenar:");
        for (double e : inputArr) {
            System.out.print(e + " ");
        }
        System.out.println();
        bucket_sort(inputArr);
        System.out.println("Arreglo despues de ordenar:");
        for (double e : inputArr) {
            System.out.print(e + " ");
        }
        System.out.println();
    }
}
