//insercion al principio
let arr1=[11,21,31,41,51];
ele=72;
console.log("Arreglo original: ",arr1.join(","));
arr1.unshift(ele);
console.log("Arreglo con insercion al principio: ",arr1.join(","));

//insercion especifica
let arr2=[11,21,31,41,51];
ele=72;
arr2.splice(3,0,ele);
console.log("Después de la inserción al final: ",arr2.join(","));

//insercion al final
let arr3=[11,21,31,41,51];
ele=72;
arr3.push(ele);
console.log("Después de la inserción al final: ",arr3.join(","));
