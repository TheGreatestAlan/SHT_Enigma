using System;

namespace Enigma_Console
{
    class EnigmaRotor
    {
        private readonly int[] output;
        private readonly int[] advancePositions;
        private int currentPosition;

        public EnigmaRotor(int[] output, int[] advancePositions)
        {
            this.output = output;
            this.advancePositions = advancePositions;
            this.SetPosition(0);
        }

        /* This method allows the rotor's position to be manually set to a specific value. If using the machine "per
         * procedure", this should only be used when setting up the machine's initial configuration, prior to encrypting
         * or decrypting, and should not be used again until the process is complete */
        public void SetPosition(int newPosition)
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
            if (Array.IndexOf(this.advancePositions, this.currentPosition) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* In order to get the proper rotor stepover behavior, ensure that you call this method AFTER checking whether
         * the next rotor in line should be advanced */
        public void AdvanceRotor()
        {
            this.currentPosition += 1;

            if (this.currentPosition > 25)
            {
                this.currentPosition = 0;
            }
        }

        public int GetRotorOutput(int input)
        {
            /* The input to the rotor must be "corrected" based on the rotor's current position. For example, if the
             * rotor is set to 'A', then the 'A' output of the adjacent rotor is wired to the 'A' input of this rotor,
             * and no correction is required. However, if this rotor is set to 'D', then the 'A' output of the adjacent
             * rotor is actually wired to the 'D' input of this rotor. */
            int correctedInput = input + this.currentPosition;
            if (correctedInput > 25)
            {
                correctedInput -= 26;
            }

            return output[correctedInput - 1];
        }

        /*Once the electrical signal has reached the machine's reflector, it is then routed "backwards" through the
         * machine's rotors and to the output. This reverses the sunbtitution cypher of the rotor */
        public int GetReflectedOutput(int input)
        {
            // Just as with the non-reflected output, the input must be "corrected" based on the rotor's position
            int correctedInput = input + this.currentPosition;
            if (correctedInput > 25)
            {
                correctedInput -= 26;
            }

            /* Since the inverse of the input-output relationship is required, finding the index of the reflected input
             * in the output array will give the required result */
            return Array.IndexOf(this.output, (correctedInput - 1));
        }
    }
}
