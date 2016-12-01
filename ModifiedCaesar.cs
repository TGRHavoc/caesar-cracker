using System;
using System.Linq;
using System.Text;

namespace CeasarCipher
{
    public class ModifiedCaesar
    {
        FrequencyAnalysis fa = new FrequencyAnalysis(); //So we can perform frequency analysis on the cipher text
        Alphabet alphabet = new Alphabet(); //The alphabet

        /// <summary>
        /// Decipher the text using the integers a and b
        /// </summary>
        /// <param name="cipherText">The cipher text</param>
        /// <param name="a">The first part of the key</param>
        /// <param name="b">The second part of the key</param>
        /// <returns>The deciphered string (assuming the key is correct)</returns>
        public string Decipher(string cipherText, int a, int b)
        {
            StringBuilder buff = new StringBuilder(); //Append the deciphered characters here

            int aInv = Inverse(a); //Find the inverse of a.
            foreach(char c in cipherText) //For each ciphered character
            {
                if (c == '\n') //If it's a new line
                {
                    buff.Append("\n"); //Keep it
                    continue; //Continue to next char
                }

                if (char.IsWhiteSpace(c)) //If it's a space..
                    continue;//Might as well skip..

                int currentIndex = alphabet.GetCharacterIndex(c); //Get the current index of the cipher character
                
                if (currentIndex - b < 0) //If it's negative 
                    currentIndex = currentIndex + 26; // Make sure it's positive and within the length of the alphabet

                int newIndex = Mod((aInv * (currentIndex - b)), alphabet.Length); //Calculate the plain text index

                buff.Append(alphabet.GetCharacterAt(newIndex)); //Add it to the string
            }

            return buff.ToString(); //Return it :D
        }
        
        /// <summary>
        /// Utility method so that negative numbers can still be MOD 
        /// </summary>
        /// <param name="a">The number to MOD</param>
        /// <param name="n">The amount to MOD by</param>
        /// <returns>The remainder of a to n</returns>
        int Mod(int a, int n)
        {
            int result = a % n;
            if ((a < 0 && n > 0) || (a > 0 && n < 0)) //If a is negative and n is postive OR a is positive and n is negative
                result += n;
            return result;
        }
        /// <summary>
        /// Get the multiplicative inverse of a given number
        /// </summary>
        /// <param name="a">The number to inverse</param>
        /// <returns>The inverse of a</returns>
        int Inverse(int a)
        {
            for (int x = 1; x < 27; x++) //For each number between 1 and 27
            {
                if ((a * x) % 26 == 1) //If we've found the multiplicative inverse
                    return x;
            }

            throw new Exception("No multiplicative inverse found!");
        }

        public string BruteForce(string cipherText)
        {
            StringBuilder buff = new StringBuilder();

            CharacterFrequencyList frequencies = fa.AnalyseFrequency(cipherText); //Get the character frequencies

            CharacterFrequency probablyE = frequencies.ProbablyE; // Character that is most likly to be E
            CharacterFrequency probablyT = frequencies.ProbablyT; // Character that is most likly to be T

            int p = alphabet.GetCharacterIndex('E'); //Get the index of E
            int q = alphabet.GetCharacterIndex('T'); //Get the index of T

            int r = alphabet.GetCharacterIndex(probablyE.Character); //Get the index of the ciphered E
            int s = alphabet.GetCharacterIndex(probablyT.Character); //Get the index of the ciphered T

            int D = Mod(p - q, 26); //Work out what D is (difference)
            int invD = Inverse(D); //Get the multiplicative inverse
            /*
            Working out :D
              a x indexOf('E') + b = indexOf ( probablyE ) MOD 26, indexOf(e) = p, indexOf( probablyE ) = r
              a x indexOf('T') + b = indexOf ( probablyT ) MOD 26, indexOf(t) = q, indexOf( probablyT ) = s
              
              D = p - q;
              
              a = D^-1 (r-s) MOD 26
              b = D^-1 (ps - qr) MOD 26 
            */
            int a = Mod(invD * (r - s), 26); //Work out the first part of the key (a)
            int b = Mod(invD * ( (p*s) - (q*r) ), 26); //Work out the second part of the key (b)

            Console.WriteLine("Found key: a={0}, b={1}", a, b);

            buff.Append(string.Format("Key: ({0},{1}) ", a, b));
            buff.Append(string.Format("\nDeciphered Text:\n{0}", Decipher(cipherText, a, b)));

            return buff.ToString(); //Return the deciphered message
        }
    }
}