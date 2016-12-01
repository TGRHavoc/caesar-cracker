using System;
using System.Collections.Generic;

namespace CeasarCipher
{
    class FrequencyAnalysis
    {
        //The standard frequencies of each letter in the English alphabet (got them from https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language)
        static readonly Dictionary<char, double> freqs = new Dictionary<char, double>()
        {   { 'A', 8.167 }, { 'B', 1.492 }, { 'C', 2.782 }, { 'D', 4.253 }, { 'E', 12.702 },
            { 'F', 2.228 }, { 'G', 2.015 }, { 'H', 6.094 }, { 'I', 6.966 }, { 'J', 0.153 },
            { 'K', 0.772 }, { 'L', 4.025 }, { 'M', 2.406 }, {'N', 6.749 } , { 'O', 7.507 },
            { 'P', 1.929 }, { 'Q', 0.095 }, { 'R', 5.987 }, { 'S', 6.327 }, { 'T', 9.056 },
            { 'U', 2.758 }, { 'V', 0.978 }, { 'W', 2.361 }, { 'X', 0.150 }, { 'Y', 1.974 },
            { 'Z', 0.074 }
        }; //I'm not changing anything in the dictionary, might as well readonly

        /// <summary>
        /// Used to get the character frequencies in a string
        /// </summary>
        /// 
        /// <param name="cipherText">The text to get the frequency of the characters for</param>
        /// 
        /// <returns>CharacterFrequencyList that contains the diferent characters and their frequency in the string</returns>
        public CharacterFrequencyList AnalyseFrequency(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) //If it's null
                throw new ArgumentException(string.Format("Sorry but, \"{0}\" cannot be analyised because it's null/empty", cipherText)); //Throw an exception so they know something screwed up
            
            //Create a new list
            CharacterFrequencyList frequencies = new CharacterFrequencyList();

            int textLen = cipherText.Length;
            for(int i = 0; i< textLen; i++) //Loop through all the characters in the string
            {
                char c = cipherText[i];
                if (c == '\n' || char.IsWhiteSpace(c)) //If it's not a letter
                    continue;//Skip!

                frequencies.AddTo(c); //Increase it's frequency
            }

            return frequencies; //Return the list
        }

        /// <summary>
        /// Wanted to come up with a value that I could use to tell which frequencylist would be a better solution so,
        /// the greater the number returned from this, the better probability that the it's the solution
        /// </summary>
        /// 
        /// <param name="characterFrequencyList">Frequencies to check</param>
        /// <param name="originalText">The text that the FrequecyList is from</param>
        /// 
        /// <returns>Amount of "correctness" the FrequencyList has towards the English frequencies</returns>
        public double GetDifference(CharacterFrequencyList characterFrequencyList , string originalText)
        {
            double totalDiff = 0;
            int textLen = originalText.Length;

            foreach(CharacterFrequency cf in characterFrequencyList.CharacterFrequencies) //For each character frequency in the list
            {
                double myPercent = (((double)cf.Frequency / (double)textLen)) * 100; //Transform the int (amount of times it appears in the string) to a double (percentage of it's appearance)
                
                double englishFreq = freqs[cf.Character]; //Get the english frquency

                //Console.WriteLine("MyPercent = " + myPercent + ", English = " + englishFreq);

                //Take myPercent away from the englishFreq (if myPercent is close, the amount taken is less therefore the total is increased more)
                double differenceBetween = englishFreq - myPercent;

                if (englishFreq < myPercent)
                    differenceBetween = myPercent - englishFreq;

                totalDiff += differenceBetween;//Incease the total by the difference
            }

            return totalDiff;
        }
    }
}