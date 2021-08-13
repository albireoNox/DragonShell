using System;
using System.Text.RegularExpressions;

namespace main_cli.math.expression
{
    public class Dice
    {
        private static Regex TOKEN_PATTERN = new Regex(
            "^(?<diceCount>[0-9]*)[dD](?<diceSize>[0-9]+)$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled, 
            TimeSpan.FromSeconds(3.0));

        private Random random;

        public Dice(Random random)
        {
            this.random = random;
        }

        public int roll(string rollToken)
        {
            var match = TOKEN_PATTERN.Match(rollToken);
            if (!match.Success)
            {
                throw new FormatException($"Dice roll '{rollToken}' invalid format");
            }

            string diceCount = match.Groups["diceCount"].Value;
            string diceSize = match.Groups["diceSize"].Value;

            return roll(string.IsNullOrEmpty(diceCount) ? 1 : int.Parse(diceCount), int.Parse(diceSize));
        }

        public int roll(int diceCount, int diceSize)
        {
            int sum = 0;
            for (int i = 0; i < diceCount; i++)
            {
                sum += random.Next(1, diceSize);
            }
            return sum;
        }
    }
}
