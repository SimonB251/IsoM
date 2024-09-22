namespace IsoMetrix.BL.StringManipulation
{
    //I left this non-static to allow for extension.
    public class StringCalculator : IStringCalculator
    {
        private const int MAX_VALUE = 1000;
        private const int DEFAULT_NUMBER = 0;
        private const string DELIMITER_PREFIX = "//";

        //The requirment doesn't specify if the comma delimiter is replaced by a custom delimiter, so I leave it here.
        private readonly List<string> _defaultDelimiters = new() { "\n", "," };

        /// <summary>
        /// Sums the numbers provided within the string, using the given delimiter, ignoring numbers larger than 1000.
        /// </summary>
        /// <param name="calculation">The string containing numbers</param>
        /// <returns>The sum of all numbers in the string.</returns>
        public int Add(string calculation)
        {
            if (!ValidateInputString(calculation))
                return DEFAULT_NUMBER;

            var delimiters   = GetDelimiters(calculation);
            var numbers      = GetNumbersString(calculation);
            var numberTokens = GetNumberTokens(delimiters, numbers);

            ValidateNegativeNumbers(numberTokens);

            //Not handling non-valid numbers due to rule 3.
            return SumValidNumbers(numberTokens);
        }

        private List<string> GetDelimiters(string calculation)
        {
            if (!AreCustomDelimitersSpecified(calculation))
                return _defaultDelimiters;

            var delimterConfiguration = calculation[DELIMITER_PREFIX.Length..calculation.IndexOf('\n')];

            var delimiters = !AreComplexDelimitersSpecified(calculation)
                 ? GetSimpleDelimiter(delimterConfiguration)
                 : GetComplexDelimiters(ref delimterConfiguration);

            return [.. delimiters, .. _defaultDelimiters];
        }

        private static List<string> GetComplexDelimiters(ref string delimterConfiguration)
        {
            var delimiters = new List<string>();
            while (true)
            {
                var startIndex = delimterConfiguration.IndexOf('[');
                var endIndex   = delimterConfiguration.IndexOf(']');

                if (startIndex == -1) break;

                var delimiter = delimterConfiguration.Substring(startIndex + 1, endIndex - 1);
                delimterConfiguration = delimterConfiguration[(endIndex + 1)..];

                delimiters.Add(delimiter);
            }

            return delimiters;
        }

        private static bool ValidateInputString(string calculation)
        {
            if (string.IsNullOrEmpty(calculation))
                return false;

            if (AreCustomDelimitersSpecified(calculation))
            {
                var newLineIndex = calculation.IndexOf('\n');
                return calculation.Length > newLineIndex + 1;
            }

            return true;
        }

        private static void ValidateNegativeNumbers(string[] numberTokens)
        {
            var negativeNumberTokens = string.Join(",", numberTokens.Where(number => number.Contains('-')));
            if (!string.IsNullOrEmpty(negativeNumberTokens))
            {
                throw new InvalidOperationException($"Negatives not allowed: {negativeNumberTokens}");
            }
        }

        private static List<string> GetSimpleDelimiter(string delimterConfiguration)
            => [delimterConfiguration];

        private static string[] GetNumberTokens(List<string> delimiters, string numbersString)
            => numbersString.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

        private static bool AreCustomDelimitersSpecified(string calculation)
            => calculation.Contains(DELIMITER_PREFIX);

        private static bool AreComplexDelimitersSpecified(string calculation)
            => calculation.Contains('[');

        private static int SumValidNumbers(string[] numberTokens)
            => numberTokens.Where(number => int.Parse(number) <= MAX_VALUE).Sum(int.Parse);

        private static string GetNumbersString(string calculation)
            => AreCustomDelimitersSpecified(calculation)
                ? calculation[calculation.IndexOf('\n')..]
                : calculation;
    }
}