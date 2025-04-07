using System;

interface IPointChecker
{
    bool ContainsPoint(params double[] coordinates);
}


abstract class GeometricFigure : IPointChecker
{
    protected double[] coefficients;

    public GeometricFigure(int size)
    {
        coefficients = new double[size];
    }

    public abstract void SetCoefficients(params double[] values);
    public abstract void PrintCoefficients();
    public abstract bool ContainsPoint(params double[] coordinates);

    ~GeometricFigure()
    {
        Console.WriteLine("Об'єкт знищено.");
    }
}


class Line2D : GeometricFigure
{
    public Line2D() : base(3) { }

    public override void SetCoefficients(params double[] values)
    {
        if (values.Length == 3)
        {
            for (int i = 0; i < 3; i++) coefficients[i] = values[i];
        }
    }

    public override void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнти прямої: a0 = {coefficients[0]}, a1 = {coefficients[1]}, a2 = {coefficients[2]}");
    }

    public override bool ContainsPoint(params double[] coords)
    {
        if (coords.Length == 2)
        {
            double x = coords[0];
            double y = coords[1];
            return coefficients[1] * x + coefficients[2] * y + coefficients[0] == 0;
        }
        return false;
    }
}


class Hyperplane4D : GeometricFigure
{
    public Hyperplane4D() : base(5) { }

    public override void SetCoefficients(params double[] values)
    {
        if (values.Length == 5)
        {
            for (int i = 0; i < 5; i++) coefficients[i] = values[i];
        }
    }

    public override void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнти гіперплощини: a0 = {coefficients[0]}, a1 = {coefficients[1]}, a2 = {coefficients[2]}, a3 = {coefficients[3]}, a4 = {coefficients[4]}");
    }

    public override bool ContainsPoint(params double[] coords)
    {
        if (coords.Length == 4)
        {
            double result = coefficients[0];
            for (int i = 0; i < 4; i++) result += coefficients[i + 1] * coords[i];
            return result == 0;
        }
        return false;
    }
}


class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Оберіть фігуру: 1 - Пряма, 2 - Гіперплощина");
        string choice = Console.ReadLine();

        GeometricFigure figure;

        if (choice == "1")
        {
            figure = new Line2D();
            figure.SetCoefficients(-6, 2, 3);
            figure.PrintCoefficients();
            Console.Write("Введіть точку (x y): ");
            var input = Console.ReadLine().Split();
            double x = double.Parse(input[0]);
            double y = double.Parse(input[1]);
            Console.WriteLine(figure.ContainsPoint(x, y) ? "Точка належить прямій." : "Точка не належить прямій.");
        }
        else
        {
            figure = new Hyperplane4D();
            figure.SetCoefficients(-10, 1, 2, 3, 4);
            figure.PrintCoefficients();
            Console.Write("Введіть точку (x1 x2 x3 x4): ");
            var input = Console.ReadLine().Split();
            double[] coords = Array.ConvertAll(input, double.Parse);
            Console.WriteLine(figure.ContainsPoint(coords) ? "Точка належить гіперплощині." : "Точка не належить гіперплощині.");
        }
    }
}