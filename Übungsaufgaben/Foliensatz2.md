# Foliensatz 2
Im Folgenden finden Sie die Musterlösungen für die Übungsaufgaben aus dem zweiten Foliensatz, `Objektorientierte Programmierung (Teil 1)`.

## Übung 1 - Felder

> Erstellen Sie eine Klasse Enemy.

```csharp
using System;

class Enemy {
    public int id;
    public int type;
    public string name;
    public uint level;
}

class Program {
    public static void Main(string[] args) {
        Enemy firstEnemy = new Enemy();
        firstEnemy.id = 1;
        firstEnemy.type = 1;
        firstEnemy.name = "Small Mob #1";
        firstEnemy.level = 2;

        Enemy secondEnemy = new Enemy();
        secondEnemy.id = 2;
        secondEnemy.type = 1;
        secondEnemy.name = "Small Mob #2";
        secondEnemy.level = 2;
    }
}
```

## Übung 2 - Properties

> Ändern Sie Ihre bestehende Enemy-Klasse so ab, dass sie anstatt public Feldern Properties verwendet.

```csharp
using System;

class Enemy {
    public int ID { get; set; }
    public int Type { get; set; }
    public string Name { get; set; }
    public uint Level { get; set; }
}

class Program {
    public static void Main(string[] args) {
        Enemy firstEnemy = new Enemy();
        firstEnemy.ID = 1;
        firstEnemy.Type = 1;
        firstEnemy.Name = "Small Mob #1";
        firstEnemy.Level = 2;

        Enemy secondEnemy = new Enemy();
        secondEnemy.ID = 2;
        secondEnemy.Type = 1;
        secondEnemy.Name = "Small Mob #2";
        secondEnemy.Level = 2;
    }
}
```

## Übung 3 - Methoden

> Implementieren Sie AddExp.

```csharp
using System;

class Player {
    public uint Exp { get; private set; }
    public uint Level { get; private set; } = 1;

    public void AddExp(uint gainedExp) {
        // Add gained EXP.
        this.Exp += gainedExp;

        // Level-up.
        uint gainedLevels = this.Exp / 1000;
        uint leftoverExp = this.Exp % 1000;

        this.Level += gainedLevels;
        this.Exp = leftoverExp;
    }
}

class Program {
    public static void TestAddExp(Player player, uint gainedExp) {
        player.AddExp(gainedExp);
        Console.WriteLine($"After gaining {gainedExp} EXP, our player is level {player.Level} and has {player.Exp}/1000 EXP for the next level-up.");
    }

    public static void Main(string[] args) {
        Player player = new Player();
        TestAddExp(player, 500);
        TestAddExp(player, 600);
        TestAddExp(player, 900);
        TestAddExp(player, 3600);
    }
}
```

## Übung 4 - Konstruktoren

> Fügen Sie ihrer Enemy-Klasse zwei Konstruktoren hinzu.

```csharp
using System;

class Enemy {
    public int ID { get; set; }
    public int Type { get; set; }
    public string Name { get; set; }
    public uint Level { get; set; }

    public Enemy(int id, int type, string name, uint level) {
        this.ID = id;
        this.Type = type;
        this.Name = name;
        this.Level = level;
    }
    public Enemy(int id, int type) : this(id, type, "", 1) {
        // Empty.
    }
}

class Program {
    public static void Main(string[] args) {
        Enemy firstEnemy = new Enemy(1, 1);
        Enemy secondEnemy = new Enemy(2, 1, "Small Mob #2", 2);
    }
}
```

## Übung 5 - Enums

> Fügen Sie das Direction-Enum hinzu und implementieren Sie eine Walk-Methode in Ihrer Enemy-Klasse.

```csharp
enum Direction {
    Up = 8,
    Right = 6,
    Down = 2,
    Left = 4
}

class Position {
    public int X { get; set; }
    public int Y { get; set; }
}

class Enemy {
    public Position Position { get; set; } = new Position();

    public void Walk(Direction direction) {
        switch (direction) {
            case Direction.Up:
                this.Position.Y -= 1;
                break;
            case Direction.Down:
                this.Position.Y += 1;
                break;
            case Direction.Left:
                this.Position.X -= 1;
                break;
            case Direction.Right:
                this.Position.X += 1;
                break;
        }
    }
}

class Program {
    public static void Main(string[] args) {
        Enemy enemy = new Enemy();

        Console.WriteLine($"Enemy at ({enemy.Position.X}, {enemy.Position.Y})");
        enemy.Walk(Direction.Up);
        Console.WriteLine($"Enemy at ({enemy.Position.X}, {enemy.Position.Y})");
        enemy.Walk(Direction.Right);
        enemy.Walk(Direction.Right);
        Console.WriteLine($"Enemy at ({enemy.Position.X}, {enemy.Position.Y})");
        enemy.Walk(Direction.Down);
        Console.WriteLine($"Enemy at ({enemy.Position.X}, {enemy.Position.Y})");
        enemy.Walk(Direction.Left);
        enemy.Walk(Direction.Left);
        Console.WriteLine($"Enemy at ({enemy.Position.X}, {enemy.Position.Y})");
    }
}
```