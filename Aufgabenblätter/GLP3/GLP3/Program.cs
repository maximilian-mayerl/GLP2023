namespace Exercise3 {
    class CombatStats {
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }

        public double CritRate { get; set; }

        public bool IsAlive {
            get {
                return this.CurrentHP > 0;
            }
        }
    }

    class Player {
        private static readonly uint[] levelExpCurve = new uint[] {
            100, 110, 121, 133, 146, 161, 177, 195, 215, 237,
            262, 289, 319, 353, 391, 433, 480, 532, 590, 655,
            728, 809, 900, 1001, 1114, 1241, 1383, 1542, 1720, 1920,
            2144, 2396, 2679, 2997, 3354, 3756, 4208, 4717, 5290, 5936,
            6664, 7485, 8412, 9458, 10640, 11976, 13486, 15194, 17127, 19316,
            21796, 24606, 27793, 31408, 35511, 40171, 45465, 51482, 58325, 66110,
            74972, 85065, 96565, 109674, 124625, 141685, 161161, 183406, 208826, 237888,
            271130, 309172, 352728, 402621, 459802, 525366, 600579, 686904, 786029, 899909,
            1030803, 1181326, 1354506, 1553851, 1783425, 2047941, 2352866, 2704544, 3110341, 3578813,
            4119904, 4745176, 5468077, 6304258, 7271943, 8392359, 9690244, 11194443, 12938602
        };

        public CombatStats Stats { get; private set; }
        public uint Level { get; private set; } = 1;
        public uint Exp { get; private set; } = 0;
        public uint ExpToNextLevel {
            get {
                // If we are at the cap, we return 0.
                if (this.Level == levelExpCurve.Length + 1) {
                    return 0;
                }

                // Otherwise, get the value from the exp curve.
                return levelExpCurve[this.Level - 1];
            }
        }

        public Player(CombatStats stats) {
            this.Stats = stats;
        }

        public void AddExp(uint gainedExp) {
            // Calculate how much EXP we currently have in total.
            uint totalExp = this.Exp + gainedExp;

            // Now loop and level up as long as we have more EXP than
            // needed for the next level.
            while (totalExp >= this.ExpToNextLevel && this.Level <= levelExpCurve.Length) {
                // Be careful here, the order of operations is important.
                // We have to subtract EXP first before increasing level,
                // or we would subtract too many EXP.
                totalExp -= this.ExpToNextLevel;
                this.Level++;
            }

            // Save leftover EXP.
            this.Exp = totalExp;
        }
    }

    class Enemy {
        public CombatStats Stats { get; private set; }
        public uint Level { get; private set; } = 1;


        public Enemy(CombatStats stats) {
            this.Stats = stats;
        }

        public int TakeDamage(Player attacker) {
            int damage = 0;

            // Base damage amount is the attacker's attack.
            damage = attacker.Stats.Attack;

            // Reduced by the target's defense.
            double defenseMultiplier = Math.Min(this.Stats.Defense / 1_500, 0.9);
            damage -= (int)(damage * defenseMultiplier);

            // Handle crit.
            // Crits deal double the damage.
            bool isCrit = Random.Shared.NextDouble() <= attacker.Stats.CritRate;
            if (isCrit) {
                damage *= 2;
            }

            // Apply damage.
            int newHP = this.Stats.CurrentHP - damage;
            if (newHP < 0) {
                newHP = 0;
            }

            this.Stats.CurrentHP = newHP;

            // Done.
            return damage;
        }
    }

    internal class Program {
        private static void RunLevelUpTest(Player player) {
            Console.WriteLine("Testing base state ...");
            Console.WriteLine($"    Test base level and exp: {player.Level == 1 && player.Exp == 0}");
            Console.WriteLine($"    Test base level-up exp: {player.ExpToNextLevel == 100}");

            Console.WriteLine("\nTesting EXP gain sequence ...");
            player.AddExp(50);
            Console.WriteLine($"    After gaining 50:");
            Console.WriteLine($"        Level should be 1, is {player.Level}: {player.Level == 1}");
            Console.WriteLine($"        Exp should be 50, is {player.Exp}: {player.Exp == 50}");
            Console.WriteLine($"        ExpToNextLevel should be 100, is {player.ExpToNextLevel}: {player.ExpToNextLevel == 100}");
            player.AddExp(160);
            Console.WriteLine($"    After gaining 160:");
            Console.WriteLine($"        Level should be 3, is {player.Level}: {player.Level == 3}");
            Console.WriteLine($"        Exp should be 0, is {player.Exp}: {player.Exp == 0}");
            Console.WriteLine($"        ExpToNextLevel should be 121, is {player.ExpToNextLevel}: {player.ExpToNextLevel == 121}");
            player.AddExp(180);
            Console.WriteLine($"    After gaining 180:");
            Console.WriteLine($"        Level should be 4, is {player.Level}: {player.Level == 4}");
            Console.WriteLine($"        Exp should be 59, is {player.Exp}: {player.Exp == 59}");
            Console.WriteLine($"        ExpToNextLevel should be 133, is {player.ExpToNextLevel}: {player.ExpToNextLevel == 133}");
            player.AddExp(10_000_000);
            Console.WriteLine($"    After gaining 10,000,000:");
            Console.WriteLine($"        Level should be 83, is {player.Level}: {player.Level == 83}");
            Console.WriteLine($"        Exp should be 491995, is {player.Exp}: {player.Exp == 491_995}");
            Console.WriteLine($"        ExpToNextLevel should be 1354506, is {player.ExpToNextLevel}: {player.ExpToNextLevel == 1_354_506}");
            player.AddExp(100_000_000);
            Console.WriteLine($"    After gaining 100,000,000:");
            Console.WriteLine($"        Level should be 100, is {player.Level}: {player.Level == 100}");
            Console.WriteLine($"        Exp should be 11880702, is {player.Exp}: {player.Exp == 11_880_702}");
            Console.WriteLine($"        ExpToNextLevel should be 0, is {player.ExpToNextLevel}: {player.ExpToNextLevel == 0}");
        }

        private static void RunSingleCombatTest(Player player, Enemy enemy) {
            while (enemy.Stats.IsAlive) {
                int damage = enemy.TakeDamage(player);
                if (damage == 0) {
                    Console.WriteLine("    The attack caused no damage. Go implement Enemy.TakeDamage()!");
                    return;
                }

                Console.WriteLine($"    Attack hits for {damage} damage, leaving the enemy with {enemy.Stats.CurrentHP} HP.");

                if (!enemy.Stats.IsAlive) {
                    Console.WriteLine("    The enemy was defeated!");
                }
            }
        }
        private static void RunCombatTest(Player player) {
            // First enemy.
            Enemy smallMob = new Enemy(new CombatStats() {
                MaxHP = 3_500,
                CurrentHP = 3_500,
                Attack = 45,
                Defense = 110,
                CritRate = 0
            });

            Console.WriteLine("Simulating combat with a small mob ...");
            RunSingleCombatTest(player, smallMob);

            // Second enemy.
            Enemy mediumMob = new Enemy(new CombatStats() {
                MaxHP = 9_200,
                CurrentHP = 9_200,
                Attack = 156,
                Defense = 376,
                CritRate = 0
            });

            Console.WriteLine("Simulating combat with a medium mob ...");
            RunSingleCombatTest(player, mediumMob);

            // Third enemy.
            Enemy boss = new Enemy(new CombatStats() {
                MaxHP = 25_000,
                CurrentHP = 25_000,
                Attack = 482,
                Defense = 962,
                CritRate = 0
            });

            Console.WriteLine("Simulating combat with a boss enemy ...");
            RunSingleCombatTest(player, boss);
        }

        static void Main(string[] args) {
            // TESTING CODE.
            // DO NOT CHANGE.

            // Create our player character.
            // This uses object initializer syntax. If you are curious:
            // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#object-initializers
            CombatStats playerStats = new CombatStats() {
                MaxHP = 15_000,
                CurrentHP = 15_000,
                Attack = 674,
                Defense = 890,
                CritRate = 0.45
            };
            Player player = new Player(playerStats);

            // Run tests.
            RunLevelUpTest(player);
            RunCombatTest(player);
        }
    }
}