namespace Peek.Common
{
    using System;
    using System.Text;

    public class RandomGenerator : IRandomGenerator
    {
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private readonly Random random;

        public RandomGenerator()
        {
            this.random = new Random();
        }

        public string RandomString(int minLength = 5, int maxLength = 50)
        {
            var result = new StringBuilder();
            var length = this.random.Next(minLength, maxLength + 1);
            for (var i = 1; i <= length; i++)
            {
                result.Append(Letters[this.random.Next(0, Letters.Length)]);
                if (i % 10 == 0)
                {
                    result.Append(' ');
                }
            }

            return result.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            return this.random.Next(min, max + 1);
        }
    }
}
