#pragma once
#include <iostream>
#include <stdexcept>

class Matrix {
private:
    int n;          // Размер матрицы
    double data;   // Двумерный массив для хранения элементов матрицы

public:

    // Конструктор по умолчанию
    Matrix(int n) : n(n), data(new double* [n]) {
        for (int i = 0; i < n; ++i) {
            data[i] = new double[n];
        }
    }

    // Конструктор для пользовательской инициализации матрицы
    Matrix(int n, double[] & initialValue) : n(n), data(new double* [n]) {
        for (int i = 0; i < n; ++i) {
            data[i] = new double[n];
            for (int j = 0; j < n; ++j) {
                data[i][j] = initialValue;
            }
        }
    }

    // Конструктор копирования
    Matrix(const Matrix& other) : n(other.n), data(new double* [other.n]) {
        for (int i = 0; i < n; ++i) {
            data[i] = new double[n];
            for (int j = 0; j < n; ++j) {
                data[i][j] = other.data[i][j];
            }
        }
    }

    // Деструктор
    ~Matrix() {
        for (int i = 0; i < n; ++i) {
            delete[] data[i];
        }
        delete[] data;
    }

    // Операция присваивания
    Matrix& operator=(const Matrix& other) {
        if (this != &other) {
            for (int i = 0; i < n; ++i) {
                delete[] data[i];
            }
            delete[] data;

            n = other.n;
            data = new double* [n];
            for (int i = 0; i < n; ++i) {
                data[i] = new double[n];
                for (int j = 0; j < n; ++j) {
                    data[i][j] = other.data[i][j];
                }
            }
        }
        return *this;
    }

    // Метод для решения системы линейных уравнений
    void solveLinearEquations(double* rhs, double* solution) {
        // Здесь должен быть ваш код для решения системы уравнений
        // Например, можно использовать метод Гаусса
    }

    //// Дополнительные методы, например, для вывода матрицы
    //void print() const {
    //    for (int i = 0; i < n; ++i) {
    //        for (int j = 0; j < n; ++j) {
    //            std::cout << data[i][j] << " ";
    //        }
    //        std::cout << std::endl;
    //    }
    //}
};
