using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ML.Common
{
    public sealed class RandomStringGenerator
    {
        /// <summary>
        /// Default: abcdefghijklmnopqrstuvwxyz
        /// </summary>
        public string LowerLetters { get; private set; } = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Default: ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        public string UpperLetters { get; private set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Default: 0123456789
        /// </summary>
        public string Numbers { get; private set; } = "0123456789";

        /// <summary>
        /// Default: `~!@#$%^&*()-_[{]}\\|;:'",.>/?
        /// </summary>
        public string SpecialCharacters { get; private set; } = "`~!@#$%^&*()-_[{]}\\|;:'\",.>/?";  //remove '<'

        /// <summary>
        /// Only set if not empty
        /// </summary>
        public RandomStringGenerator(string numberChars = null, string lowerLetters = null, string upperLetters = null,  string specialChars = null)
        {
            SetNumbers(numberChars);
            SetLetters(lowerLetters, upperLetters);
            SetSpecialCharacters(specialChars);
        }

        /// <summary>
        /// Only set if not empty
        /// </summary>
        public void SetSpecialCharacters(string specialCharacters)
        {
            if (!string.IsNullOrWhiteSpace(specialCharacters))
            {
                SpecialCharacters = specialCharacters;
            }
        }

        /// <summary>
        /// Only set if not empty
        /// </summary>
        public void SetLetters(string lower, string uppper)
        {
            if (!string.IsNullOrWhiteSpace(lower))
            {
                LowerLetters = lower;
            }
            if (!string.IsNullOrWhiteSpace(uppper))
            {
                UpperLetters = uppper;
            }
        }

        /// <summary>
        /// Only set if not empty
        /// </summary>
        public void SetNumbers(string numbers)
        {
            if (!string.IsNullOrWhiteSpace(numbers))
            {
                Numbers = numbers;
            }
        }

        public string GenerateNumbers(int length) => Generate(length, hasNumber: true, hasLowerLetter: false, hasUpperLetters: false, hasSpecialCharacter: false);

        public string GenerateLowerLetters(int length) => Generate(length, hasNumber: false, hasLowerLetter: true, hasUpperLetters: false, hasSpecialCharacter: false);

        public string GenerateUpperLetters(int length) => Generate(length, hasNumber: false, hasLowerLetter: false, hasUpperLetters: true, hasSpecialCharacter: false);

        public string GenerateLowerUpperLetters(int length) => Generate(length, hasNumber: false, hasLowerLetter: true, hasUpperLetters: true, hasSpecialCharacter: false);

        public string GenerateSpecialCharacters(int length) => Generate(length, hasNumber: false, hasLowerLetter: false, hasUpperLetters: false, hasSpecialCharacter: true);

        public string Generate(int length, bool hasNumber = true, bool hasLowerLetter = true, bool hasUpperLetters = true, bool hasSpecialCharacter = true)
        {
            var generate = new StringBuilder();

            var patterns = new List<string>();
            if (hasNumber) patterns.Add(Numbers);
            if (hasLowerLetter) patterns.Add(LowerLetters);
            if (hasUpperLetters) patterns.Add(UpperLetters);
            if (hasSpecialCharacter) patterns.Add(SpecialCharacters);
            if (patterns.Count == 0) patterns.Add(Numbers); //all conditions pattern is false, get number pattern as default

            for (var i = 0; i < length; i++)
            {
                byte index = (patterns.Count > 0) ? GenerateRandomDigit((byte)patterns.Count) : (byte)0;
                generate.Append(GenerateString(1, patterns[index]));
            }

            return generate.ToString();
        }

        /// <summary>
        /// RandomStringGenerator constants (LowerLetters, UpperLetters, SpecialCharacters, Numbers), maximum length of parttern is 255
        /// </summary>
        public string GenerateString(int lengh, string pattern)
        {
            var generate = new StringBuilder();

            var generatePattern = pattern.Length <= 255 ? pattern : pattern.Substring(0, 255);

            for (var i = 0; i < lengh; i++)
            {
                var roll = GenerateRandomDigit((byte)generatePattern.Length);
                generate.Append(generatePattern[roll]);
            }

            return generate.ToString();
        }

        #region Generate processing

        private RNGCryptoServiceProvider _rgnCrypto;

        private byte GenerateRandomDigit(byte numberSides = 10)
        {
            if (numberSides <= 0 || numberSides > 255)
            {
                throw new ArgumentOutOfRangeException("number sides must be between 1 and 255");
            }

            // Create a new instance of the RNGCryptoServiceProvider.
            if (_rgnCrypto == null) _rgnCrypto = new RNGCryptoServiceProvider();

            // Create a byte array to hold the random value.
            var randomNumber = new byte[1];

            do
            {
                // Fill the array with a random value.
                _rgnCrypto.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));

            return (byte)(randomNumber[0] % numberSides);
        }

        private bool IsFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            var fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }

        #endregion
    }
}
