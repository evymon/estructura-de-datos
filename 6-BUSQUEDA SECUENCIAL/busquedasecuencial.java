public class busquedasecuencial {
    static int findEle(int[] inputArr, int s, int targetEle) {
        for (int k = 0; k < s; k++) {
            if (inputArr[k] == targetEle) {
                return k;
            }
        }
        return -1;
    }

    public static void main(String[] args) {
        int[] inputArr = {12, 34, 10, 6, 40, 89, 98, 57, 19, 69};
        int targetElement = 40;
        int s = inputArr.length;

        int idx = findEle(inputArr, s, targetElement);
        if (idx != -1) {
            System.out.println("el elemento se encuentra en la posicion: " + (idx + 1));
        } else {
            System.out.println("no se encuentra el elemento");
        }
    }
}
