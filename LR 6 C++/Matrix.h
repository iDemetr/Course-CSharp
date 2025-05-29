#pragma once
#include <iostream>
#include <stdexcept>

class Matrix {
private:
    int n;          // ������ �������
    double data;   // ��������� ������ ��� �������� ��������� �������

public:

    // ����������� �� ���������
    Matrix(int n) : n(n), data(new double* [n]) {
        for (int i = 0; i < n; ++i) {
            data[i] = new double[n];
        }
    }

    // ����������� ��� ���������������� ������������� �������
    Matrix(int n, double[] & initialValue) : n(n), data(new double* [n]) {
        for (int i = 0; i < n; ++i) {
            data[i] = new double[n];
            for (int j = 0; j < n; ++j) {
                data[i][j] = initialValue;
            }
        }
    }

    // ����������� �����������
    Matrix(const Matrix& other) : n(other.n), data(new double* [other.n]) {
        for (int i = 0; i < n; ++i) {
            data[i] = new double[n];
            for (int j = 0; j < n; ++j) {
                data[i][j] = other.data[i][j];
            }
        }
    }

    // ����������
    ~Matrix() {
        for (int i = 0; i < n; ++i) {
            delete[] data[i];
        }
        delete[] data;
    }

    // �������� ������������
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

    // ����� ��� ������� ������� �������� ���������
    void solveLinearEquations(double* rhs, double* solution) {
        // ����� ������ ���� ��� ��� ��� ������� ������� ���������
        // ��������, ����� ������������ ����� ������
    }

    //// �������������� ������, ��������, ��� ������ �������
    //void print() const {
    //    for (int i = 0; i < n; ++i) {
    //        for (int j = 0; j < n; ++j) {
    //            std::cout << data[i][j] << " ";
    //        }
    //        std::cout << std::endl;
    //    }
    //}
};
