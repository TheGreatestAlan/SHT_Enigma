using System.Collections.Generic;

namespace Enigma_Console
{
    class EnigmaReflector
    {
        /* The reflector in an Enigma machine connects pairs of letters together, e.g. if 'A' is wired to 'H', then
         * inputting 'A' into the reflector will give 'H' as an output, and 'H' will give 'A'.*/
        private Dictionary<char, char> outputMapping;
        private readonly static int ALPHABET_LENGTH = 26;
        private readonly static int CAPITAL_LETTER_CHAR_OFFSET = 65;

        public EnigmaReflector(char[] outputOrder)
        {
            this.outputMapping = new Dictionary<char, char>();
            for (int i = 0; i < ALPHABET_LENGTH; i += 1)
            {
                char currentLetter = (char)(CAPITAL_LETTER_CHAR_OFFSET + i);
                this.outputMapping.Add(currentLetter, outputOrder[i]);
            }
        }

        public char GetReflectorOutput(char input)
        {
            return this.outputMapping[input];
        }
    }
}
