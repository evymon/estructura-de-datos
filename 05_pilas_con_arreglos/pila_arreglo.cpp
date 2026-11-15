#include <iostream>
#define MAX_SIZE 100

int stack[MAX_SIZE];
int top = -1;

void push(int item) {
    if (top == MAX_SIZE - 1) {
        std::cout << "Stack Overflow" << std::endl;
        return;
    }
    stack[++top] = item;
}

int pop() {
    if (top == -1) {
        std::cout << "Stack Underflow" << std::endl;
        return -1;
    }
    return stack[top--];
}

int peek() {
    if (top == -1) {
        std::cout << "Pila vacia" << std::endl;
        return -1;
    }
    return stack[top];
}

bool isEmpty() { return top == -1; }
bool isFull() { return top == MAX_SIZE - 1; }

int main() {
    push(10);
    push(20);
    push(30);

    std::cout << "Elemento Superior: " << peek() << std::endl;
    std::cout << "Extrae elemento: " << pop() << std::endl;
    std::cout << "Elemento Superior: " << peek() << std::endl;

    return 0;
}
