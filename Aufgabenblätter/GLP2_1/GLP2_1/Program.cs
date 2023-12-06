namespace GLP2_1 {
    internal class Program {
        static void Main(string[] args) {
            // Ask the user for a positive integer.
            int max;
            Console.Write("Please enter a positive integer: ");
            if (!int.TryParse(Console.ReadLine(), out max)) {
                Console.WriteLine("That was not a valid number.");
                return;
            }

            // Iterate all numbers from 1 to max.
            for (int i  = 1; i <= max; i++) {
                // Print depending on divisibility.
                if (i % 5 == 0 && i % 3 == 0) {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 3 == 0) {
                    Console.WriteLine("Fizz");
                }
                else if (i % 5 == 0) {
                    Console.WriteLine("Buzz");
                }
                else {
                    Console.WriteLine(i);
                }
            }
        }
    }
}