using System;
using System.Collections.Generic;
using System.Linq;

namespace AksharaShift
{
    /// <summary>
    /// Handles conversion of Unicode Malayalam text to ML and FML legacy ASCII font formats.
    /// Contains comprehensive character mapping tables for both ML and FML fonts.
    /// </summary>
    public class MalayalamTextConverter
    {
        // ML Font mappings - Unicode Malayalam (U+0D00 to U+0D7F) to ML ASCII
        private static readonly Dictionary<char, string> MLMappings = new Dictionary<char, string>
        {
            // Vowels (Swaram)
            { 'അ', "a" }, { 'ആ', "A" }, { 'ഇ', "i" }, { 'ഈ', "I" },
            { 'ഉ', "u" }, { 'ഊ', "U" }, { 'ഋ', "R" }, { 'ഌ', "L" },
            { 'എ', "e" }, { 'ഏ', "E" }, { 'ഐ', "Y" }, { 'ഒ', "o" },
            { 'ഓ', "O" }, { 'ഔ', "W" }, { 'ഽ', "^" },

            // Consonants (Vyanjnam)
            { 'ക', "k" }, { 'ഖ', "K" }, { 'ഗ', "g" }, { 'ഘ', "G" },
            { 'ങ', "N" }, { 'ച', "c" }, { 'ഛ', "C" }, { 'ജ', "j" },
            { 'ഝ', "J" }, { 'ഞ', "~" }, { 'ട', "t" }, { 'ഠ', "T" },
            { 'ഡ', "d" }, { 'ഢ', "D" }, { 'ണ', "n" }, { 'ത', "th" },
            { 'ഥ', "Th" }, { 'ദ', "dh" }, { 'ധ', "Dh" }, { 'ന', "N" },
            { 'പ', "p" }, { 'ഫ', "P" }, { 'ബ', "b" }, { 'ഭ', "B" },
            { 'മ', "m" }, { 'യ', "y" }, { 'ര', "r" }, { 'റ', "R" },
            { 'ല', "l" }, { 'ള', "L" }, { 'ഴ', "Z" }, { 'വ', "v" },
            { 'ശ', "s" }, { 'ഷ', "S" }, { 'സ', "c" }, { 'ഹ', "h" },

            // Dependent vowel signs (Vowel Modifiers)
            { 'ാ', "aa" }, { 'ി', "i" }, { 'ീ', "ii" }, { 'ു', "u" },
            { 'ൂ', "uu" }, { 'ൃ', "r" }, { 'ൄ', "rr" }, { '൅', "l" },
            { 'െ', "e" }, { 'േ', "ee" }, { 'ൈ', "y" }, { '൉', "o" },
            { 'ോ', "o" }, { 'ോ', "o" }, { 'ൌ', "w" }, { 'ൎ', "m" },

            // Virama and Anusvara/Visarga
            { '്', "" }, // Virama (no output)
            { 'ം', "m" }, // Anusvara
            { 'ഃ', "h" }, // Visarga

            // Chillus (New independent consonants)
            { 'ൻ', "n" }, { 'ർ', "r" }, { 'ൽ', "l" }, { 'ൾ', "L" },
            { 'ൿ', "k" },

            // Numbers
            { '൦', "0" }, { '൧', "1" }, { '൨', "2" }, { '൩', "3" },
            { '൪', "4" }, { '൫', "5" }, { '൬', "6" }, { '൭', "7" },
            { '൮', "8" }, { '൯', "9" },

            // Punctuation
            { '।', "." }, { '॥', "|" }, { '؟', "?" },
        };

        // FML Font mappings - Unicode Malayalam to FML ASCII
        private static readonly Dictionary<char, string> FMLMappings = new Dictionary<char, string>
        {
            // Vowels (Swaram) - FML representation
            { 'അ', "a" }, { 'ആ', "A" }, { 'ഇ', "i" }, { 'ഈ', "I" },
            { 'ഉ', "u" }, { 'ഊ', "U" }, { 'ഋ', "R" }, { 'ഌ', "L" },
            { 'എ', "e" }, { 'ഏ', "E" }, { 'ഐ', "@" }, { 'ഒ', "o" },
            { 'ഓ', "O" }, { 'ഔ', "#" }, { 'ഽ', ":" },

            // Consonants (Vyanjnam) - FML representation
            { 'ക', "k" }, { 'ഖ', "K" }, { 'ഗ', "g" }, { 'ഘ', "G" },
            { 'ങ', "`" }, { 'ച', "c" }, { 'ഛ', "C" }, { 'ജ', "z" },
            { 'ഝ', "Z" }, { 'ഞ', "~" }, { 'ട', "t" }, { 'ഠ', "T" },
            { 'ഡ', "d" }, { 'ഢ', "D" }, { 'ണ', "N" }, { 'ത', "q" },
            { 'ഥ', "Q" }, { 'ദ', "w" }, { 'ധ', "W" }, { 'ന', "n" },
            { 'പ', "p" }, { 'ഫ', "P" }, { 'ബ', "b" }, { 'ഭ', "B" },
            { 'മ', "m" }, { 'യ', "y" }, { 'ര', "r" }, { 'റ', "f" },
            { 'ല', "l" }, { 'ള', "M" }, { 'ഴ', "Z" }, { 'വ', "v" },
            { 'ശ', "s" }, { 'ഷ', "S" }, { 'സ', "x" }, { 'ഹ', "h" },

            // Dependent vowel signs (FML)
            { 'ാ', "a" }, { 'ി', "i" }, { 'ീ', "I" }, { 'ു', "u" },
            { 'ൂ', "U" }, { 'ൃ', "r" }, { 'ൄ', "R" }, { '൅', "l" },
            { 'െ', "e" }, { 'േ', "E" }, { 'ൈ', "@" }, { '൉', "o" },
            { 'ോ', "o" }, { 'ോ', "O" }, { 'ൌ', "#" }, { 'ൎ', "-" },

            // Virama and Anusvara/Visarga
            { '്', "" }, // Virama
            { 'ം', "m" }, // Anusvara
            { 'ഃ', "H" }, // Visarga

            // Chillus
            { 'ൻ', "n" }, { 'ർ', "r" }, { 'ൽ', "l" }, { 'ൾ', "M" },
            { 'ൿ', "k" },

            // Numbers (same in FML)
            { '൦', "0" }, { '൧', "1" }, { '൨', "2" }, { '൩', "3" },
            { '൪', "4" }, { '൫', "5" }, { '൬', "6" }, { '൭', "7" },
            { '൮', "8" }, { '൯', "9" },

            // Punctuation
            { '।', "." }, { '॥', "|" }, { '؟', "?" },
        };

        // Extended consonant combinations (conjuncts)
        private static readonly Dictionary<string, string> MLConjuncts = new Dictionary<string, string>
        {
            { "ക്ക", "kk" }, { "ങ്ങ", "Ng" }, { "ച്ച", "cc" }, { "ഞ്ഞ", "~~" },
            { "ട്ട", "tt" }, { "ണ്ണ", "nn" }, { "ദ്ധ", "ddh" }, { "ന്ന", "nn" },
            { "പ്പ", "pp" }, { "ബ്ബ", "bb" }, { "മ്മ", "mm" }, { "യ്യ", "yy" },
            { "ര്ര", "rr" }, { "ല്ല", "ll" }, { "വ്വ", "vv" },
            { "ശ്ശ", "ss" }, { "സ്സ", "ss" }, { "ഹ്ഹ", "hh" },
        };

        private static readonly Dictionary<string, string> FMLConjuncts = new Dictionary<string, string>
        {
            { "ക്ക", "kk" }, { "ങ്ങ", "``" }, { "ച്ച", "cc" }, { "ഞ്ഞ", "~~" },
            { "ട്ട", "tt" }, { "ണ്ണ", "NN" }, { "ദ്ധ", "ww" }, { "ന്ന", "nn" },
            { "പ്പ", "pp" }, { "ബ്ബ", "bb" }, { "മ്മ", "mm" }, { "യ്യ", "yy" },
            { "ര്ര", "rr" }, { "ല്ല", "ll" }, { "വ്വ", "vv" },
            { "ശ്ശ", "ss" }, { "സ്സ", "xx" }, { "ഹ്ഹ", "hh" },
        };

        /// <summary>
        /// Converts Unicode Malayalam text to ML legacy ASCII format.
        /// </summary>
        /// <param name="unicodeText">Unicode Malayalam input text</param>
        /// <returns>ML ASCII legacy font compatible text</returns>
        public static string ConvertToML(string unicodeText)
        {
            if (string.IsNullOrEmpty(unicodeText))
                return unicodeText;

            return ConvertUsingMappings(unicodeText, MLMappings, MLConjuncts);
        }

        /// <summary>
        /// Converts Unicode Malayalam text to FML legacy ASCII format.
        /// </summary>
        /// <param name="unicodeText">Unicode Malayalam input text</param>
        /// <returns>FML ASCII legacy font compatible text</returns>
        public static string ConvertToFML(string unicodeText)
        {
            if (string.IsNullOrEmpty(unicodeText))
                return unicodeText;

            return ConvertUsingMappings(unicodeText, FMLMappings, FMLConjuncts);
        }

        /// <summary>
        /// Core conversion logic using character and conjunct mappings.
        /// </summary>
        private static string ConvertUsingMappings(string input, 
            Dictionary<char, string> mappings, 
            Dictionary<string, string> conjuncts)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = new System.Text.StringBuilder();
            int i = 0;

            while (i < input.Length)
            {
                bool matched = false;

                // Try to match conjuncts (two-character combinations)
                if (i < input.Length - 1)
                {
                    string twoChar = input.Substring(i, 2);
                    if (conjuncts.ContainsKey(twoChar))
                    {
                        result.Append(conjuncts[twoChar]);
                        i += 2;
                        matched = true;
                    }
                }

                // If no conjunct matched, try single character
                if (!matched)
                {
                    char currentChar = input[i];
                    
                    if (mappings.ContainsKey(currentChar))
                    {
                        result.Append(mappings[currentChar]);
                    }
                    else
                    {
                        // Keep unmapped characters as-is (English text, numbers, etc.)
                        result.Append(currentChar);
                    }
                    i++;
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Gets statistics about the conversion (for debugging).
        /// </summary>
        public static ConversionStats GetStats(string input, ConversionType type)
        {
            var stats = new ConversionStats
            {
                InputLength = input.Length,
                OutputText = type == ConversionType.ML ? ConvertToML(input) : ConvertToFML(input)
            };
            
            stats.OutputLength = stats.OutputText.Length;
            stats.MalayalamCharsFound = input.Count(c => 
                (c >= '\u0D00' && c <= '\u0D7F')); // Unicode Malayalam range

            return stats;
        }
    }

    /// <summary>
    /// Enum for conversion types.
    /// </summary>
    public enum ConversionType
    {
        ML,  // ML Font
        FML  // FML Font
    }

    /// <summary>
    /// Statistics about text conversion.
    /// </summary>
    public class ConversionStats
    {
        public int InputLength { get; set; }
        public int OutputLength { get; set; }
        public string? OutputText { get; set; }
        public int MalayalamCharsFound { get; set; }
    }
}
