namespace ConsoleApp1
{
    class Calculator
    {
        static void Main()
        {
            double num1 = ReadNumber("请输入第一个数字");
            string op = ReadOperator();
            double num2 = ReadNumber("请输入第二个数字", op);

            double result = Calculate(num1, num2, op);
            Console.WriteLine($"计算结果：{num1} {op} {num2} = {result}");
        }

        static double ReadNumber(string prompt)
        {
            while (true)
            {
                Console.Write($"请输入{prompt}：");
                if (double.TryParse(Console.ReadLine(), out double number))
                {
                    return number;
                }
                Console.WriteLine("输入无效，请重新输入数字。");
            }
        }

        static double ReadNumber(string prompt, string op)
        {
            while (true)
            {
                double number = ReadNumber(prompt);
                if (op == "/" && number == 0)
                {
                    Console.WriteLine("除数不能为0，请重新输入。");
                    continue;
                }
                return number;
            }
        }

        static string ReadOperator()
        {
            while (true)
            {
                Console.Write("请输入运算符（+、-、*、/）：");
                string op = Console.ReadLine().Trim();

                if (op == "+" || op == "-" || op == "*" || op == "/")
                {
                    return op;
                }
                Console.WriteLine("运算符无效，请输入 +、-、* 或 /。");
            }
        }

        static double Calculate(double num1, double num2, string op)
        {
            return op switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num1 / num2,
                _ => throw new InvalidOperationException("无效运算符")
            };
        }
    }
 }
