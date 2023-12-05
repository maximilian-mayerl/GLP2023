namespace GLP2_2 {
    internal class Program {
        static void Main(string[] args) {
            // Generate the secret.
            int secretNumber = Random.Shared.Next(1, 100);
            Console.WriteLine("Die Zufallszahl wurde generiert. Viel Glück!");

            // Let the player guess until they find the secret number.
            while (true) {
                // Get next guess from the player.
                Console.Write("Ihr Versuch: ");
                int guess;
                if (!int.TryParse(Console.ReadLine(), out guess)) {
                    Console.WriteLine("Das ist keine valide Zahl. Bitte versuchen Sie es erneut.");
                    continue;
                }

                // Check if the player has won.
                if (guess == secretNumber) {
                    Console.WriteLine($"Gratulation, Sie haben gewonnen! Die gesuchte Zahl war {secretNumber}.");
                    return;
                }
                
                // Otherwise, give the player a tip.
                if (secretNumber < guess) {
                    Console.WriteLine("Die gesuchte Zahl ist kleiner.");
                }
                else {
                    Console.WriteLine("Die gesuchte Zahl ist größer.");
                }
            }
        }
    }
}