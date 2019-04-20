namespace Enigma_Console
{
    class EnigmaReflector
    {
        /* The reflector in an Enigma machine connects pairs of letters together, e.g. if 'A' is wired to 'H', then
         * inputting 'A' into the reflector will give 'H' as an output, and 'H' will give 'A'.*/
        private readonly int[] output;
        
        public EnigmaReflector(int[][] pairings)
        {
            /* In order to ensure that the reflector is constructed with the correct behavior, it is instantiated with a
             * 2-dimensional array of pairings, e.g. [[1, 12], [2, 9] ...] */
            
            foreach (int[] pairing in pairings)
            {
                this.output[pairing[0] - 1] = pairing[1];
                this.output[pairing[1] - 1] = pairing[0];
            }
        }

        public int GetReflectorOutput(int input)
        {
            return this.output[input - 1];
        }
    }
}
