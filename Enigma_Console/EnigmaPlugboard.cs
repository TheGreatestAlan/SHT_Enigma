using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma_Console
{
    class EnigmaPlugboard
    {
        private readonly int[] output;

        public EnigmaPlugboard(int[][] pairings)
        {
            /* Since the plugboard can be used with any number of pairings (0-13), and the default behavior is to pass
             * each letter unmodified, the output must be first be initialized with this default behavior before
             * applying the pairings */

            for (int i = 0; i <26; i++)
            {
                this.output[i] = (i + 1);
            }

            foreach (int[] pairing in pairings)
            {
                this.output[pairing[0] - 1] = pairing[1];
                this.output[pairing[1] - 1] = pairing[0];
            }
        }

        public int GetPlugboardOutput(int input)
        {
            return this.output[input - 1];
        }
    }
}
