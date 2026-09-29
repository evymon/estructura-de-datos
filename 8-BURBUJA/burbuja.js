function bubbleSort(a) {
    const s = a.length;
    for (let i = 0; i < s; i++) {
        let isSwapped = false;
        for (let j = 0; j < s - i - 1; j++) {
            if (a[j] > a[j + 1]) {
                [a[j], a[j + 1]] = [a[j + 1], a[j]];
                isSwapped = true;
            }
        }
        if (isSwapped == false) {
            break;
        }
    }
}

const a = [15, 16, 11, 13, 14];
console.log("antes de ordenar los elementos del array son: ");
for (const e of a) {
    process.stdout.write(e + " ");
}

bubbleSort(a);
console.log("\ndespues de ordenar los elementos del array son: ");
for (let e = 0; e < a.length; e++) {
    process.stdout.write(a[e] + " ");
}
