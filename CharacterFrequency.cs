using System;

namespace CeasarCipher
{
    /// <summary>
    /// Helper class, holds a character and an integer
    /// </summary>
    class CharacterFrequency
    {
        //Don't want character to change, might as well put as readonly
        private readonly char character;

        //The number of times the above character appears
        private int frequency;

        //Public property to get the character
        public char Character
        {
            get
            {
                return character;
            }
        }

        //Public property to get and set the frequency
        public int Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
            }
        }

        /// <summary>
        /// Creates a new "CharacterFrequency" object
        /// </summary>
        /// 
        /// <param name="character">Character to store the frequency of</param>
        /// <param name="frequency">Inital frequency of the character (defaults to 0)</param>
        public CharacterFrequency(char character, int frequency = 0)
        {
            this.character = character;
            this.frequency = frequency;
        }

    }
}
