using System;

namespace Enigma_Console
{
    class Program
    {
        private static String rotorIOutput = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
        private static char[] rotorIStepover = { 'R' };
        private static String rotorIIOutput = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        private static char[] rotorIIStepover = { 'F' };
        private static String rotorIIIOutput = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        private static char[] rotorIIIStepover = { 'W' };
        private static String reflectorAOutput = "EJMZALYXVBWFCRQUONTSPIKHGD";
        static void Main(string[] args)
        {
            char[] rotorIMapping = new char[26];
            for (int i = 0; i < 26; i += 1)
            {
                rotorIMapping[i] = rotorIOutput[i];
            }
            char[] rotorIIMapping = new char[26];
            for (int i = 0; i < 26; i += 1)
            {
                rotorIIMapping[i] = rotorIIOutput[i];
            }
            char[] rotorIIIMapping = new char[26];
            for (int i = 0; i < 26; i += 1)
            {
                rotorIIIMapping[i] = rotorIIIOutput[i];
            }
            char[] reflectorAMapping = new char[26];
            for (int i = 0; i < 26; i += 1)
            {
                reflectorAMapping[i] = reflectorAOutput[i];
            }

            char[][] plugboardMapping = new char[][] { new char[] { 'A', 'D' }, 
                                                       new char[] { 'G', 'Y' } };

            EnigmaRotor rotorI = new EnigmaRotor(rotorIMapping, rotorIStepover);
            EnigmaRotor rotorII = new EnigmaRotor(rotorIIMapping, rotorIIStepover);
            EnigmaRotor rotorIII = new EnigmaRotor(rotorIIIMapping, rotorIIIStepover);
            EnigmaRotor[] rotors = { rotorI, rotorII, rotorIII };
            EnigmaReflector reflectorA = new EnigmaReflector(reflectorAMapping);
            EnigmaPlugboard plugboard = new EnigmaPlugboard(plugboardMapping);

            EnigmaMachine enigma = new EnigmaMachine(rotors, reflectorA, plugboard);

            Console.WriteLine("Welcome to SHT_Enigma, a crude emplimentation of the WWII German Enigma cipher.");
            Console.WriteLine("This machine is hardcoded to use rotors I, II, III, Reflector A, and a plugboard setting of \"AD GY\".");
            Console.WriteLine("Additionally, the initial rotor setting is hardcored at \"AAA\" and the ring setting and re-wirable reflector are not implemented");
            Console.WriteLine();
            Console.Write("Please enter a message to cipher (characters and spaces only): ");

            String messageToCipher = Console.ReadLine();

            String ciphered = enigma.CipherMessage(messageToCipher);

            Console.WriteLine("Your ciphered message is:");
            Console.WriteLine(ciphered);
        }
    }
}
