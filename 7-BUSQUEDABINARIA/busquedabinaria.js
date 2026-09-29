function findEle(arr, l, h, targetValue) {
    while (l <= h) {
        const mid = l + Math.floor((h - l) / 2);
        if (arr[mid] == targetValue) {
            return mid;
        } else if (arr[mid] < targetValue) {
            l = mid + 1;
        } else {
            h = mid - 1;
        }
    }
    return -1;
}

const inputArr = [12, 34, 10, 6, 40, 89, 98, 57, 19, 69];
const targetElement = 40;
const s = inputArr.length;

const idx = findEle(inputArr, 0, s - 1, targetElement);
if (idx !== -1) {
    console.log("el elemento se encuentra en posicion: " + (idx + 1));
} else {
    console.log("el elemento no se encuentra:");
}
