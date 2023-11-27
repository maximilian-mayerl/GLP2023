# Foliensatz 1
Im Folgenden finden Sie die Musterlösungen für die Übungsaufgaben aus dem ersten Foliensatz, `Imperative und Prozedurale Programmierung`

## Übungs 1 - Hello, C#!

> Erstellen Sie ein neues Projekt in Visual Studio und implementieren Sie Ihr eigenes Hello, World!. Geben Sie dabei den Text "Hello, C#!" aus.

```csharp
using System;

class Program {
    public static void Main(string[] args) {
        Console.WriteLine("Hello, C#!");
    }
}
```

## Übung 2 - Variablen und Operatoren

Siehe Aufgabe 2 aus dem ersten Aufgabenblatt. =D

## Übung 3 - Eingabe, Strings, Konvertierung

> Schreiben Sie ein kleines Programm, welches den Benutzer nach dessen Vor- und Nachnamen sowie dessen Alter (in Jahren) fragt. Anschließend soll das Programm einen wie folgt formattieren Text ausgeben (Werte nat ̈urlich durch die jeweiligen Nutzereingaben ersetzt):
>
> Hallo MAXIMILIAN mayerl!
>
> Du bist 33 Jahre alt. Das entspricht 0.33 Jahrhunderten!

```csharp
using System;

class Program {
    public static void Main(string[] args) {
        Console.Write("Geben Sie Ihren Vornamen ein: ");
        string firstName = Console.ReadLine();

        Console.Write("Geben Sie Ihren Nachnamen ein: ");
        string lastName = Console.ReadLine();

        Console.Write("Geben Sie Ihr Alter ein: ");
        int age = int.Parse(Console.ReadLine());

        Console.WriteLine($"Hallo {firstName.ToUpper()} {lastName.ToLower()}!");
        Console.WriteLine();
        Console.WriteLine($"Du bist {age} Jahre alt. Das entspricht {age / 100.0} Jahrhunderten!");
    }
}
```

## Übung 4 - Verzweigungen

> Schreiben Sie ein Program, welches vom Benutzer eine Ganze Zahl einließt. Falls die eingegebene Zahl gerade ist, soll das Program "Die Zahl ist gerade" ausgeben. Anderfalls soll es "Die Zahl ist ungerade" ausgeben.

```csharp
using System;

class Program {
    public static void Main(string[] args) {
        Console.Write("Geben Sie eine Ganze Zahl ein: ");
        int number = int.Parse(Console.ReadLine());

        if (number % 2 == 0) {
            Console.WriteLine("Die Zahl ist gerade.");
        }
        else {
            Console.WriteLine("Die Zahl ist ungerade.");
        }
    }
}
```

Eine weitere Möglichkeit, welche den ternären Operator verwendet:

```csharp
using System;

class Program {
    public static void Main(string[] args) {
        Console.Write("Geben Sie eine Ganze Zahl ein: ");
        int number = int.Parse(Console.ReadLine());

        Console.WriteLine(number % 2 == 0 ? "Die Zahl ist gerade." : "Die Zahl ist ungerade.");
    }
}
```

## Übung 5 - Schleifen

> Schreiben Sie die folgende while-Schleife als for-Schleife.

Original:

```csharp
int[] numbers = new int[] { 5, 10, 15, 20, 25 };
int sum = 0;

int index = 0;
while (index < numbers.Length) {
    sum += numbers[index];
    index += 1;
}

Console.WriteLine(sum);
```

Umgeschrieben zu einer for-Schleife:


```csharp
int[] numbers = new int[] { 5, 10, 15, 20, 25 };
int sum = 0;

for (int i = 0; i < numbers.Length; i++) {
    sum += numbers[i];
}

Console.WriteLine(sum);
```

## Übung 6 - Array-Methode

> Schreiben Sie eine Methode, welche zwei int-Arrays entgegennimmt, diese zu einem neuen Array vereint und das neue Array zurückgibt.

```csharp
using System;

class Program {
    public static int[] MergeArrays(int[] first, int[] second) {
        // Create the result array. Since we want to merge the two input arrays,
        // the result array will be as long as both of them combined.
        int[] result = new int[first.Length + second.Length];

        // Copy the elements of the first input array into the result.
        for (int i  = 0; i < first.Length; i++) {
            result[i] = first[i];
        }

        // Copy the elements of the second input array into the result.
        for (int i = 0; i < second.Length; i++ ) {
            // We have to add the length of the first input array here, so we
            // don't overwrite the elements we have already copied over with the
            // first loop above.
            result[i + first.Length] = second[i];
        }

        return result;
    }

    public static string GetArrayString(int[] array) {
        // Not really part of the exercise, but visualizing our result is nice. =D
       return $"{{ {string.Join(", ", array)} }}";
    }

    public static void Main(string[] args) {
        int[] someNumbers = new int[] { 1, 2, 3, 4 };
        int[] otherNumbers = new int[] { 9, 8, 7, 6 };

        int[] allNumbers = MergeArrays(someNumbers, otherNumbers);

        // Visualize the result.
        Console.WriteLine($"someNumbers == {GetArrayString(someNumbers)}");
        Console.WriteLine($"otherNumbers == {GetArrayString(otherNumbers)}");
        Console.WriteLine($"allNumbers == {GetArrayString(allNumbers)}");

    }
}
```