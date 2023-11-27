namespace GLP1_3 {
    internal class Program {
        static void Main(string[] args) {
            // Punkt 1
            Console.Write("Bitte geben Sie Ihren Namen ein: ");
            string name = Console.ReadLine();

            Console.WriteLine($"Ihr Name: {name}");
            Console.WriteLine($"Ihr Name in Kleinbuchstaben: {name.ToLower()}");
            Console.WriteLine($"Ihr Name in Großbuchstaben: {name.ToUpper()}");
            Console.WriteLine($"Ihr Name enthält den Buchstaben x: {name.Contains("x")}");

            // Punkt 2
            Console.Write("Bitte geben Sie eine Ganze Zahl ein: ");
            int firstInt = int.Parse(Console.ReadLine());
            Console.Write("Bitte geben Sie eine weitere Ganze Zahl ein: ");
            int secondInt = int.Parse(Console.ReadLine());

            Console.WriteLine($"firstInt + secondInt == {firstInt + secondInt}");
            Console.WriteLine($"firstInt - secondInt == {firstInt - secondInt}");
            Console.WriteLine($"firstInt * secondInt == {firstInt * secondInt}");
            Console.WriteLine($"firstInt / secondInt == {firstInt / secondInt}");
            Console.WriteLine($"firstInt % secondInt == {firstInt % secondInt}");
            Console.WriteLine($"firstInt == secondInt == {firstInt == secondInt}");
            Console.WriteLine($"firstInt != secondInt == {firstInt != secondInt}");
            Console.WriteLine($"firstInt < secondInt == {firstInt < secondInt}");
            Console.WriteLine($"firstInt <= secondInt == {firstInt <= secondInt}");
            Console.WriteLine($"firstInt > secondInt == {firstInt > secondInt}");
            Console.WriteLine($"firstInt >= secondInt == {firstInt >= secondInt}");

            // Punkt 3
            Console.Write("Bitte geben Sie eine Dezimalzahl ein: ");
            double firstDouble = double.Parse(Console.ReadLine());
            Console.Write("Bitte geben Sie eine weitere Dezimalzahl ein: ");
            double secondDouble = double.Parse(Console.ReadLine());

            Console.WriteLine($"firstDouble + secondDouble == {firstDouble + secondDouble}");
            Console.WriteLine($"firstDouble - secondDouble == {firstDouble - secondDouble}");
            Console.WriteLine($"firstDouble * secondDouble == {firstDouble * secondDouble}");
            Console.WriteLine($"firstDouble / secondDouble == {firstDouble / secondDouble}");
            Console.WriteLine($"firstDouble % secondDouble == {firstDouble % secondDouble}");
            Console.WriteLine($"firstDouble == secondDouble == {firstDouble == secondDouble}");
            Console.WriteLine($"firstDouble != secondDouble == {firstDouble != secondDouble}");
            Console.WriteLine($"firstDouble < secondDouble == {firstDouble < secondDouble}");
            Console.WriteLine($"firstDouble <= secondDouble == {firstDouble <= secondDouble}");
            Console.WriteLine($"firstDouble > secondDouble == {firstDouble > secondDouble}");
            Console.WriteLine($"firstDouble >= secondDouble == {firstDouble >= secondDouble}");

            // Punkt 4
            bool boolTrue = true;
            bool boolFalse = false;

            Console.WriteLine($"!true == {!boolTrue}");
            Console.WriteLine($"!false == {!boolFalse}");

            Console.WriteLine($"true && true == {boolTrue && boolTrue}");
            Console.WriteLine($"true && false == {boolTrue && boolFalse}");
            Console.WriteLine($"false && true == {boolFalse && boolTrue}");
            Console.WriteLine($"false && false == {boolFalse && boolFalse}");

            Console.WriteLine($"true || true == {boolTrue || boolTrue}");
            Console.WriteLine($"true || false == {boolTrue || boolFalse}");
            Console.WriteLine($"false || true == {boolFalse || boolTrue}");
            Console.WriteLine($"false || false == {boolFalse || boolFalse}");
        }
    }
}