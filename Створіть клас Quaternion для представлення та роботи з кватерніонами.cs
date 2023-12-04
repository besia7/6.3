using System;

public class Quaternion
{
    public double W { get; set; }  // Скалярна частина
    public double X { get; set; }  // I компонента (i)
    public double Y { get; set; }  // J компонента (j)
    public double Z { get; set; }  // K компонента (k)

    public Quaternion(double w, double x, double y, double z)
    {
        W = w;
        X = x;
        Y = y;
        Z = z;
    }

    // Перевантажені оператори для додавання кватерніонів
    public static Quaternion operator +(Quaternion a, Quaternion b)
    {
        return new Quaternion(a.W + b.W, a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    // Перевантажені оператори для віднімання кватерніонів
    public static Quaternion operator -(Quaternion a, Quaternion b)
    {
        return new Quaternion(a.W - b.W, a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    // Перевантажені оператори для множення кватерніонів
    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        double w = a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z;
        double x = a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y;
        double y = a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X;
        double z = a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W;
        return new Quaternion(w, x, y, z);
    }

    // Метод для обчислення норми (модуля) кватерніона
    public double Norm()
    {
        return Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
    }

    // Метод для обчислення спряженого кватерніона
    public Quaternion Conjugate()
    {
        return new Quaternion(W, -X, -Y, -Z);
    }

    // Метод для обчислення інверсного кватерніона
    public Quaternion Inverse()
    {
        Quaternion conjugate = Conjugate();
        double normSquared = W * W + X * X + Y * Y + Z * Z;
        if (normSquared == 0)
            throw new InvalidOperationException("Quaternion has zero norm, cannot calculate inverse.");
        double scale = 1.0 / normSquared;
        return new Quaternion(conjugate.W * scale, conjugate.X * scale, conjugate.Y * scale, conjugate.Z * scale);
    }

    // Перевантажені оператори порівняння
    public static bool operator ==(Quaternion a, Quaternion b)
    {
        return a.W == b.W && a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    }

    public static bool operator !=(Quaternion a, Quaternion b)
    {
        return !(a == b);
    }

    // Метод для конвертації кватерніона в матрицю обертання (4x4)
    public double[,] ToRotationMatrix()
    {
        double[,] matrix = new double[4, 4];
        double wx = W * X;
        double wy = W * Y;
        double wz = W * Z;
        double xx = X * X;
        double xy = X * Y;
        double xz = X * Z;
        double yy = Y * Y;
        double yz = Y * Z;
        double zz = Z * Z;

        matrix[0, 0] = 1 - 2 * (yy + zz);
        matrix[0, 1] = 2 * (xy - wz);
        matrix[0, 2] = 2 * (xz + wy);
        matrix[0, 3] = 0;

        matrix[1, 0] = 2 * (xy + wz);
        matrix[1, 1] = 1 - 2 * (xx + zz);
        matrix[1, 2] = 2 * (yz - wx);
        matrix[1, 3] = 0;

        matrix[2, 0] = 2 * (xz - wy);
        matrix[2, 1] = 2 * (yz + wx);
        matrix[2, 2] = 1 - 2 * (xx + yy);
        matrix[2, 3] = 0;

        matrix[3, 0] = 0;
        matrix[3, 1] = 0;
        matrix[3, 2] = 0;
        matrix[3, 3] = 1;

        return matrix;
    }
}