# Foliensatz 3
Im Folgenden finden Sie die Musterlösungen für die Übungsaufgaben aus dem dritten Foliensatz, `Objektorientierte Programmierung (Teil 2)`.

## Übung 1 - Klassenhierarchie

> Erstellen Sie eine Entity-Klasse und davon abgeleitete Klassen für konkrete Entity-Typen in Ihrem Spiel.

```csharp
class Position {
    public int X { get; set; }
    public int Y { get; set; }
}

class Entity {
    public Position Position { get; set; } = new Position();
}

class PlayerEntity : Entity {
    public int Level { get; private set; }
}

class EnemyEntity : Entity {
    public void AttackPlayer(PlayerEntity target) {
        // ToDo: Implement.
    }
}
```

## Übung 2 - Virtuelle Methoden

> Fügen Sie Ihrer Entity-Klasse eine virtuelle Spawn-Methode hinzu und überschreiben Sie diese in den abgeleiteten Klassen.

```csharp
class Position {
    public int X { get; set; }
    public int Y { get; set; }
}

class Entity {
    public Position Position { get; set; } = new Position();

    public virtual void Spawn() {
        Console.WriteLine("Entity is spawning.");
    }
}

class PlayerEntity : Entity {
    public int Level { get; private set; }

    public override void Spawn() { 
        Console.WriteLine("Player is spawning."); 
    }
}

class EnemyEntity : Entity {
    public void AttackPlayer(PlayerEntity target) {
        // ToDo: Implement.
    }

    public override void Spawn() {
        Console.WriteLine("Enemy is spawning.");
    }
}

class Program {
    public static void Main(string[] args) {
        Entity player = new PlayerEntity();
        Entity firstEnemy = new EnemyEntity();
        Entity secondEnemy = new EnemyEntity();

        firstEnemy.Spawn();
        secondEnemy.Spawn();
        player.Spawn();
    }
}
```

## Übung 3 - Collections

> Erstellen Sie eine Liste von doubles und lesen Sie Zahlen vom Benutzer ein. Summieren Sie dann die Zahlen.

```csharp
class Program {
    public static void Main(string[] args) {
        // Create list.
        List<double> numbers = new List<double>();

        // Read numbers from the user until they enter something
        // that is not a number.
        while (true) {
            Console.Write("Geben Sie die nächste Zahl ein: ");
            string input = Console.ReadLine();

            // Try to parse the input as a string.
            // If it worked, add the entered number to the list
            // and continue. Otherwise, end the loop.
            double number;
            if (double.TryParse(input, out number)) {
                numbers.Add(number);
            }
            else {
                break;
            }
        }

        // Add the numbers in the list and output the sum.
        double sum = 0;
        foreach (double number in numbers) {
            sum += number;
        }
        Console.WriteLine($"Die Summe Ihrer Zahlen ist: {sum}");
    }
}
```