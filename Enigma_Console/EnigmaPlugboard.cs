using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma_Console
{
    class EnigmaPlugboard
    {
        private Dictionary<char, char> outputMapping;
        private readonly static int ALPHABET_LENGTH = 26;
        private readonly static int CAPITAL_LETTER_CHAR_OFFSET = 65;

        public EnigmaPlugboard(char[][] pairings)
        {
            this.outputMapping = new Dictionary<char, char>();
            /* Since the plugboard can be used with any number of pairings (0-13), and the default behavior is to pass
             * each letter unmodified, the output must be first be initialized with this default behavior before
             * applying the pairings */
            for (int i = 0; i < ALPHABET_LENGTH; i += 1)
            {
                char currentLetter = (char)(CAPITAL_LETTER_CHAR_OFFSET + i);
                this.outputMapping.Add(currentLetter, currentLetter);
            }

            foreach (char[] pairing in pairings)
            {
                this.outputMapping[pairing[0]] = pairing[1];
                this.outputMapping[pairing[1]] = pairing[0];
            }
        }

        public char GetPlugboardOutput(char input)
        {
            return this.outputMapping[input];
        }
    }
}
