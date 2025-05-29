using System;

namespace LR_6;

public class Matrix
{
    private int n;
    private double[,] data;

    // Конструктор по умолчанию
    public Matrix(int n)
    {
        this.n = n;
        data = new double[n, n];
    }

    // Конструктор для пользовательской инициализации матрицы
    public Matrix(int n, double[] firstRow) : this(n)
    {
        if (firstRow.Length != n)
        {
            throw new ArgumentException("Длина первой строки должна совпадать с порядком матрицы.");
        }

        for (int i = 0; i < n; i++)
        {
            int shift = i;
            for (int j = 0; j < n; j++)
            {
                data[i, j] = firstRow[(j + shift) % n];
            }
        }
    }

    // Метод для решения системы линейных уравнений
    public double[] SolveLinearEquations(double[] rhs)
    {
        // Здесь должен быть ваш код для решения системы уравнений
        // Например, можно использовать метод Гаусса
        throw new NotImplementedException("Метод SolveLinearEquations не реализован.");
    }

    // Перегруженная версия виртуального метода ToString
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result += data[i, j] + " ";
            }
            result += Environment.NewLine;
        }
        return result;
    }
}
