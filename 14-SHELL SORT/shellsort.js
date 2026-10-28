class ShellSort {
    static displayArr(inputArr) {
        for (const k of inputArr) {
            process.stdout.write(k + " ");
        }
        console.log();
    }

    sort(inputArr) {
        const size = inputArr.length;
        let gapSize = Math.floor(size / 2);
        while (gapSize > 0) {
            for (let j = gapSize; j < size; j++) {
                const val = inputArr[j];
                let k = j;
                while (k >= gapSize && inputArr[k - gapSize] > val) {
                    inputArr[k] = inputArr[k - gapSize];
                    k = k - gapSize;
                }
                inputArr[k] = val;
            }
            gapSize = Math.floor(gapSize / 2);
        }
        return 0;
    }
}

const inputArr = [36, 34, 43, 11, 15, 20, 28, 45];
console.log("Arreglo antes de ser ordenado: ");
ShellSort.displayArr(inputArr);
const obj = new ShellSort();
obj.sort(inputArr);
console.log("Arreglo despues de ser ordenado: ");
ShellSort.displayArr(inputArr);
