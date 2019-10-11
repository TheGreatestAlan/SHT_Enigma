using System;
using System.Collections.Generic;

namespace Enigma_Console
{
    class EnigmaRotor
    {
        private Dictionary<char, char> outputMapping;
        private Dictionary<char, char> reflectedOutputMapping;
        private Dictionary<char, bool> advancePositions;
        private char currentPosition;
        private readonly static int ALPHABET_LENGTH = 26;
        private readonly static int CAPITAL_LETTER_CHAR_OFFSET = 65;

        public EnigmaRotor(char[] outputOrder, char[] advancePositions)
        {
            this.outputMapping = new Dictionary<char, char>();
            for (int i =0; i < ALPHABET_LENGTH; i += 1)
            {
                char currentLetter = (char) (CAPITAL_LETTER_CHAR_OFFSET + i);
                this.outputMapping.Add(currentLetter, outputOrder[i]);
            }

            this.reflectedOutputMapping = new Dictionary<char, char>();
            for (int i = 0; i < ALPHABET_LENGTH; i += 1)
            {
                char currentLetter = (char)(CAPITAL_LETTER_CHAR_OFFSET + i);
                this.reflectedOutputMapping.Add(outputOrder[i], currentLetter);
            }

            this.advancePositions = new Dictionary<char, bool>();
            for (int i = 0; i < ALPHABET_LENGTH; i += 1)
            {
                char currentLetter = (char)(CAPITAL_LETTER_CHAR_OFFSET + i);
                if (Array.IndexOf(advancePositions, currentLetter) != -1)
                {
                    this.advancePositions.Add(currentLetter, true);
                }
                else
                {
                    this.advancePositions.Add(currentLetter, false);
                }
            }
            this.SetPosition('A');
        }

        /* This method allows the rotor's position to be manually set to a specific value. If using the machine "per
         * procedure", this should only be used when setting up the machine's initial configuration, prior to encrypting
         * or decrypting, and should not be used again until the process is complete */
        public void SetPosition(char newPosition)
        {
            this.currentPosition = newPosition;
        }

        /* Each rotor has a set of toothed pawls along the right side that aligns with a ratcheting mechanism in the
         * machine. Each time a key is pressed, these mechanisms will attempt to rotate the rotor. However, each rotor
         * has a shroud on the left side that blocks the pawls of the next rotor to the left, preventing it from
         * rotating. These shrouds have a notch cut out in one or more locations, however, that will allow the mechanism
         * through in order to rotate the next rotor. */

        /* In order to get the proper behavior, this method should be called BEFORE the rotor is advanced, otherwise,
         * there will be an off-by-one error in your rotor stepover */
        public bool DetermineIfNextRotorShouldAdvance()
        {
            return this.advancePositions[this.currentPosition];
        }

        /* In order to get the proper rotor stepover behavior, ensure that you call this method AFTER checking whether
         * the next rotor in line should be advanced */
        public void AdvanceRotor()
        {
            int nextPosition = 1 + (int)this.currentPosition;

            if (nextPosition > (int)'Z')
            {
                nextPosition = (int)'A';
            }

            this.currentPosition = (char)nextPosition;
        }

        public char GetRotorOutput(char input)
        {
            /* The input to the rotor must be "corrected" based on the rotor's current position. For example, if the
             * rotor is set to 'A', then the 'A' output of the adjacent rotor is wired to the 'A' input of this rotor,
             * and no correction is required. However, if this rotor is set to 'D', then the 'A' output of the adjacent
             * rotor is actually wired to the 'D' input of this rotor. */
            int correctedInput = (int)input + ((int)this.currentPosition - CAPITAL_LETTER_CHAR_OFFSET);
            if (correctedInput > (int)'Z')
            {
                correctedInput -= ALPHABET_LENGTH;
            }

            return this.outputMapping[(char)correctedInput];
        }

        /*Once the electrical signal has reached the machine's reflector, it is then routed "backwards" through the
         * machine's rotors and to the output. This reverses the sunbtitution cypher of the rotor */
        public char GetReflectedOutput(char reflectedInput)
        {
            /* Just as with the normal input, this must be corrected for the rotor's actual position */
            int correctedReflectedInput = (int)reflectedInput + ((int)this.currentPosition - CAPITAL_LETTER_CHAR_OFFSET);
            if (correctedReflectedInput > (int)'Z')
            {
                correctedReflectedInput -= ALPHABET_LENGTH;
            }

            return this.reflectedOutputMapping[(char)correctedReflectedInput];
        }
    }
}
