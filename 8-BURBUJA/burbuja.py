def bubbleSort(a):
    s=len(a)
    for e in range(s):
        isSwapped=False
        for e in range(0,s-e-1):
            if a[e]>a[e+1]:
                a[e],a[e+1]=a[e+1],a[e]
                isSwapped=True
        if(isSwapped==False):
            break
if __name__=="__main__":
    a=[15,16,11,13,14]
    print("antes de ordenar los elementos del array son: ")
    for e in a:
        print (e,end=' ')

    bubbleSort(a)
    print("\ndespues de ordenar los elementos del array son: ")
    for e in range (len(a)):
        print ("%d" %a[e],end=" ")
