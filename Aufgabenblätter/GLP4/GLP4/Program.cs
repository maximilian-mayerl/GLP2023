using System.Numerics;
using System;

namespace Exercise3 {
    class CombatStats {
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }

        public double CritRate { get; set; }

        public int Speed { get; set; }

        public bool IsAlive {
            get {
                return this.CurrentHP > 0;
            }
        }
    }

    abstract class Entity {
        public CombatStats Stats { get; protected set; }
        public uint Level { get; protected set; } = 1;
        public string Name { get; protected set; } = "";

        public virtual int TakeDamage(Entity attacker) {
            return 0;
        }

        public static Entity DecideNextMover(Entity firstCombatant, Entity secondCombatent) {
            double speedFactor = firstCombatant.Stats.Speed / (double)(firstCombatant.Stats.Speed + secondCombatent.Stats.Speed);

            if (Random.Shared.NextDouble() < speedFactor) {
                return firstCombatant;
            }
            else {
                return secondCombatent;
            }
        }
    }

    class Player : Entity {
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

        public Player(string name, CombatStats stats) {
            this.Name = name;
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

        public override int TakeDamage(Entity attacker) {
            int damage = 0;

            // Base damage amount is the attacker's attack.
            damage = attacker.Stats.Attack;

            // Reduced by the target's defense.
            double defenseMultiplier = Math.Min(this.Stats.Defense / 2_000, 0.8);
            damage -= (int)(damage * defenseMultiplier);

            // Handle crit.
            // Crits deal 1.5 times the damage.
            bool isCrit = Random.Shared.NextDouble() <= attacker.Stats.CritRate;
            if (isCrit) {
                damage = (int)(damage * 1.5);
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

    class Enemy : Entity {
        public Enemy(string name, CombatStats stats) {
            this.Name= name;
            this.Stats = stats;
        }

        public override int TakeDamage(Entity attacker) {
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
        private static void RunSingleCombatTest(Entity firstCombatant, Entity secondCombatant) {
            // Run combat until one of the combatants dies.
            while (firstCombatant.Stats.IsAlive && secondCombatant.Stats.IsAlive) {
                // Decide who gets to move.
                Entity attacker = Entity.DecideNextMover(firstCombatant, secondCombatant);
                Entity defender = (attacker == firstCombatant) ? secondCombatant : firstCombatant;

                // Apply damage.
                Console.Write($"    {attacker.Name} attacks ... ");
                int damage = defender.TakeDamage(attacker);

                // Check if properly implemented.
                if (damage == 0) {
                    Console.WriteLine();
                    Console.WriteLine("    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("    The attack caused no damage. Go implement TakeDamage() in all classes!");
                    Console.WriteLine("    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    return;
                }

                // Report result.
                Console.WriteLine($"hits for {damage} damage, leaving {firstCombatant.Name} with {firstCombatant.Stats.CurrentHP} HP and {secondCombatant.Name} with {secondCombatant.Stats.CurrentHP} HP.");
            }

            Console.WriteLine("The battle is over!");
        }

        static void Main(string[] args) {
            // TESTING CODE.
            // DO NOT CHANGE.

            // TEST 1: Player against a weak enemy.
            // Create our player character.
            // This uses object initializer syntax. If you are curious:
            // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#object-initializers
            Entity player = new Player("Player", new CombatStats() {
                MaxHP = 15_000,
                CurrentHP = 15_000,
                Attack = 674,
                Defense = 890,
                CritRate = 0.45,
                Speed = 500
            });
            Entity smallMob = new Enemy("Small Mob #1", new CombatStats() {
                MaxHP = 3_500,
                CurrentHP = 3_500,
                Attack = 45,
                Defense = 110,
                CritRate = 0,
                Speed = 150
            });

            Console.WriteLine("Simulating combat with a small mob ...");
            RunSingleCombatTest(player, smallMob);

            // TEST 2: Player against a boss enemy.
            player = new Player("Player", new CombatStats() {
                MaxHP = 15_000,
                CurrentHP = 15_000,
                Attack = 674,
                Defense = 890,
                CritRate = 0.45,
                Speed = 500
            });
            Entity boss = new Enemy("Scary Boss", new CombatStats() {
                MaxHP = 25_000,
                CurrentHP = 25_000,
                Attack = 482,
                Defense = 962,
                CritRate = 0,
                Speed = 900
            });

            Console.WriteLine("Simulating combat with a boss ...");
            RunSingleCombatTest(player, boss);

            // TEST 2: Two enemies against each other.
            Entity mediumMob1 = new Enemy("Medium Mob #1", new CombatStats() {
                MaxHP = 9_200,
                CurrentHP = 9_200,
                Attack = 156,
                Defense = 376,
                CritRate = 0,
                Speed = 300
            });
            Entity mediumMob2 = new Enemy("Medium Mob #2", new CombatStats() {
                MaxHP = 9_200,
                CurrentHP = 9_200,
                Attack = 156,
                Defense = 376,
                CritRate = 0,
                Speed = 300
            });

            Console.WriteLine("Simulating combat between two enemies ...");
            RunSingleCombatTest(mediumMob1, mediumMob2);
        }
    }
}