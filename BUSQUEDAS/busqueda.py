def findEle (inputArr,s, targetEle):
    for k in range(s):
        if (inputArr[k] == targetEle): 
            return k

    return -1
if __name__ == "__main__":
    inputArr = [12,34,10,6,40,89,98,57,19,69]
    targetElement = 40
    s = len (inputArr)

    idx = findEle (inputArr, s, targetElement)
    if idx !=-1:
        print("el elemento se encuentra en la posicion: " +str(idx + 1))
    else:
        print("no se encuentra el elemento")