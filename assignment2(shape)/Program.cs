namespace assignment2_shape_
{
    public abstract class Shape
    {
        public abstract double AreaCore { get; }
        public abstract bool IsValid();
        public double Area => IsValid() ? AreaCore : 0;
        public abstract override string ToString(); 
    }

    public class Rectangle : Shape
    {
        public double Length { get; set; }
        public double Width { get; set; }

        public override double AreaCore => Length * Width;
        public override bool IsValid() => Length > 0 && Width > 0;

        public override string ToString() =>
            $"长为{Length:F0}、宽为{Width:F0}的长方形";
    }

    public class Square : Shape
    {
        public double Side { get; set; }

        public override double AreaCore => Side * Side;
        public override bool IsValid() => Side > 0;

        public override string ToString() =>
            $"边长为{Side:F0}的正方形";
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public override double AreaCore => Math.PI * Radius * Radius;
        public override bool IsValid() => Radius > 0;

        public override string ToString() =>
            $"半径为{Radius:F0}的圆形";
    }

    public static class ShapeGenerator
    {
        private static Random _random = new Random();

        public static Shape Generate()
        {
            int type = _random.Next(0, 3);

            return type switch
            {
                0 => new Rectangle { Length = GetInt(), Width = GetInt() },
                1 => new Square { Side = GetInt() },
                2 => new Circle { Radius = GetInt() },
                _ => throw new InvalidOperationException()
            };
        }

        private static int GetInt() => _random.Next(1, 11); // 生成1~10的整数作为参数
    }

    class Program
    {
        static void Main()
        {
            var shapes = new List<Shape>();
            for (int i = 0; i < 10; i++)
            {
                shapes.Add(ShapeGenerator.Generate());
            }

            // 输出每个形状的详细信息
            foreach (var shape in shapes)
            {
                Console.WriteLine($"{shape}，面积为 {shape.Area:N2}");
            }
        }
    }
}