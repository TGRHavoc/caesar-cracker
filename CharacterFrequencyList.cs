using System;
using System.Collections.Generic;

namespace CeasarCipher
{
    //Created this class so I could have better methods to handle the data (e.g. AddTo)
    class CharacterFrequencyList
    {
        //List of character frequencies
        private List<CharacterFrequency> characterFrequencies = new List<CharacterFrequency>();

        //Public property to get the frequencies
        public List<CharacterFrequency> CharacterFrequencies
        {
            get { return characterFrequencies; }
        }

        /// <summary>
        /// Get the character in this list that is most probably the character E in the plaintext
        /// </summary>
        public CharacterFrequency ProbablyE
        {
            get
            {
                Sort();
                return characterFrequencies[0];
            }
        }

        /// <summary>
        /// Get the character in this list that is most probably the character T in the plaintext
        /// </summary>
        public CharacterFrequency ProbablyT
        {
            get
            {
                Sort();
                return characterFrequencies[1];
            }
        }


        public void Sort()
        {
            characterFrequencies.Sort(delegate (CharacterFrequency x, CharacterFrequency y)
            {
                return y.Frequency.CompareTo(x.Frequency); //Sort the frequencies (largest to lowest)
            });
        }

        /// <summary>
        /// Whether or not the list contains the frequency of the character
        /// </summary>
        /// 
        /// <param name="character">The character to check</param>
        /// 
        /// <returns>True if it's in the list, False otherwise</returns>
        public bool Contains(char character)
        {
            foreach(CharacterFrequency cf in characterFrequencies)
            {
                if (cf.Character == character)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the CharacterFrequency for a character
        /// </summary>
        /// 
        /// <param name="character">The character to get the frequency of</param>
        /// 
        /// <returns>The character frequency of the character</returns>
        public CharacterFrequency GetFrequency(char character)
        {
            foreach(CharacterFrequency cf in characterFrequencies) //For each frequency in the list,
            {
                if (cf.Character == character) //If the characters are the same,
                    return cf; //return the frequency
            }

            //If we've reached here, then the list doesn't contain the character 
            CharacterFrequency toRet = new CharacterFrequency(character); //Creates a frequency of 0
            characterFrequencies.Add(toRet); //Adds to the list

            return toRet; //Returns the newly created frequency
        }

        /// <summary>
        /// Adds 1 to the frequency of the character
        /// </summary>
        /// 
        /// <param name="character">Character to add 1 to the frequency of</param>
        public void AddTo(char character)
        {
            CharacterFrequency cf = GetFrequency(character); //Get the current frequency (0 if it didn't exist)
            cf.Frequency++; //Add 1 to it
        }

    }
}
