using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma_Console
{
    class EnigmaMachine
    {
        private EnigmaRotor[] rotors; // rotors[0] is the right-hand (entry) rotor
        private EnigmaReflector reflector;
        private EnigmaPlugboard plugboard;
        private static readonly int ROTORS_THAT_GET_ADVANCED = 3;

        public EnigmaMachine(EnigmaRotor[] rotors, EnigmaReflector reflector, EnigmaPlugboard plugboard)
        {
            this.rotors = rotors;
            this.reflector = reflector;
            this.plugboard = plugboard;
        }

        public char CipherLetter(char letterToBeCiphered)
        {
            // The enciphering/deciphering process for the Enigma works as follows:
            // When a key is pressed, the rotors are advanced
            this.AdvanceRotors();

            // First, the input letter is passed through the plugboard
            char plugboardOutput = this.plugboard.GetPlugboardOutput(letterToBeCiphered);

            // Next, the output is passed through the rotors, starting at the right-hand side
            char rotorOutput = plugboardOutput;
            for (int i = 0; i < this.rotors.Length; i += 1)
            {
                rotorOutput = this.rotors[i].GetRotorOutput(rotorOutput);
            }

            // Once through the rotors, the output is passed through the reflector...
            char reflectorOutput = this.reflector.GetReflectorOutput(rotorOutput);

            // ...and back into the rotors, but this time traveling "backwards"
            char reflectedRotorOutput = reflectorOutput;
            for (int i = (this.rotors.Length - 1); i >= 0; i -= 1)
            {
                reflectedRotorOutput = this.rotors[i].GetReflectedOutput(reflectedRotorOutput);
            }

            // Finally, the signal re-enters the plugboard before reaching the machine's output
            char cipheredLetter = this.plugboard.GetPlugboardOutput(reflectedRotorOutput);

            return cipheredLetter;
        }

        public String CipherMessage(String message)
        {
            String rawMessage = message.ToUpper().Replace(" ", "");
            StringBuilder cipheredMessage = new StringBuilder("");

            for (int i = 0; i < rawMessage.Length; i += 1)
            {
                cipheredMessage.Append(this.CipherLetter(rawMessage[i]));
                // Standard German protocol with enigma was to add a space every 5 letters to improve readability
                if ((i + 1) %5 == 0)
                {
                    cipheredMessage.Append(" ");
                }
            }

            return cipheredMessage.ToString();
        }
        private void AdvanceRotors()
        {
            bool[] shouldThisRotorAdvance = new bool[this.rotors.Length];

            shouldThisRotorAdvance[0] = true; // The right-hand rotor always advances
            for (int i = 1; i < ROTORS_THAT_GET_ADVANCED; i += 1)
            {
                shouldThisRotorAdvance[i] = this.rotors[i - 1].DetermineIfNextRotorShouldAdvance();
            }
            
            for (int i = 0; i < ROTORS_THAT_GET_ADVANCED; i += 1)
            {
                if (shouldThisRotorAdvance[i] == true)
                {
                    this.rotors[i].AdvanceRotor();
                }
            }
        }
    }
}
