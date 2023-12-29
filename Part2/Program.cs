namespace Part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            List<Hand> hands = new List<Hand>();

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                hands.Add(new Hand(parts[0], parts[1]));
            }

            hands.Sort();

            long sum = 0;
            for (int i = 0; i < hands.Count(); i++)
            {
                int multiplier = i + 1;
                sum += multiplier * hands[i].Bet;
            }
            Console.WriteLine(sum);
        }
    }
}