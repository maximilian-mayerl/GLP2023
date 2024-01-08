namespace GLP4_2 {
    class NPC {
        private List<string> dialogLines;
        
        public string Name { get; private set; }

        public void Talk() { 
        }
    }

    class Position {
        public double X { get; set; }
        public double Y { get; set; }
    }

    abstract class Vehicle {
        public Position CurrentPosition { get; set; }

        public abstract Position Move(Position targetPosition);
    }

    class Airplane : Vehicle {
        public double Speed { get; protected set; }

        public Airplane(double speed) { 
        }

        public override Position Move(Position targetPosition) {
            return new Position();
        }
    }

    class Program {
        static void Main(string[] args) {
            List<int> values = new List<int>() { 1, 2, 3, 4, 5 };

            int pos = 0;
            while (pos < values.Count) {
                Console.WriteLine(values[pos]);
                pos++;
            }

            for (int i = 0; i < values.Count; i++) {
                Console.WriteLine(values[i]);
            }

            foreach (int number in values) {
                Console.WriteLine(number);
            }
        }
    }
}
