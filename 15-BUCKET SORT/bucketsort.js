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
    const bucketArr = [];
    for (let i = 0; i < s; i++) {
        bucketArr.push([]);
    }
    for (const j of inputArr) {
        const bi = Math.floor(s * j);
        bucketArr[bi].push(j);
    }
    for (const bukt of bucketArr) {
        insertion_sort(bukt);
    }
    let idx = 0;
    for (const bukt of bucketArr) {
        for (const j of bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}

const inputArr = [0.77, 0.16, 0.38, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67];
console.log("Arreglo antes de ordenar:");
console.log(inputArr.join(" "));
bucket_sort(inputArr);
console.log("Arreglo despues de ordenar:");
console.log(inputArr.join(" "));
