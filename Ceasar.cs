using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeasarCipher
{
    class Ceasar
    {

        Alphabet alphabet = new Alphabet();

        /// <summary>
        /// Decrypts a string using the caesar cipher method.
        /// </summary>
        /// 
        /// <param name="encrypted">The ciphered text</param>
        /// <param name="shift">The amount of shift to apply to the ciphered text</param>
        /// 
        /// <returns>"Deciphered" text using the amount of shift supplied</returns>
        public string Decrypt(string encrypted, int shift)
        {
            encrypted = encrypted.ToUpper(); //Make sure it's in uppercase, since that's what alphabet we're using

            StringBuilder decryptedText = new StringBuilder(); //Faster than concatication with 'string' data type?

            foreach (char c in encrypted)
            {
                if (c == '\n')
                { //Skip all newline characters character.
                    decryptedText.Append('\n'); // Formatting :D, make output easier to read.
                    continue;
                }

                // If the below line isn't added then you extraneous letters at the end of each line :/
                if (char.IsWhiteSpace(c)) //If it's a whitespace, skip it.
                    continue;

                int currentIndex = alphabet.GetCharacterIndex(c); //Get a number assosiated with 
                int shifted = ( currentIndex - shift ); //Get the index of the "shifted" character. (e.g. A shifted by 1 = Z)
                
                char decryptedChar = alphabet.GetCharacterAt(shifted); //Get the decrypted character using the new shifted index

                decryptedText.Append(decryptedChar); //Add it to the string builder.
            }

            return decryptedText.ToString(); //Return it
        }

        public string Encrypt(string plainText, int shift)
        {
            plainText = plainText.ToUpper(); //Translate the plaintext to uppercase so we can use the characters in out list

            StringBuilder encryptedText = new StringBuilder(); //Store the shifted text in here
            foreach (char c in plainText) //For each character in our plaintext string
            {
                if (c == '\n')
                { //If it's a new line char
                    encryptedText.Append("\n"); //Keeps the format of the original string (atleast, the newlines)
                    continue;
                }

                int currentIndex = alphabet.GetCharacterIndex(c); //Get the current index in the list the character sits at.
                int shifted = (currentIndex + shift); //Increase it, making sure we "wrap" the list

                char encryptedChar = alphabet.GetCharacterAt(shifted); //Get the character that sits on the shifted index

                encryptedText.Append(encryptedChar); //Add to out encrypted string
            }

            return encryptedText.ToString(); //Return
        }


        public string BruteForce(string cipherText)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 1; i<= 26; i++)
            {
                builder.Append("Shift amount: " + i + "\n");
                builder.Append(string.Format("Deciphered: \n{0}\n", Decrypt(cipherText, i)));

                builder.Append("=================\n\n");
            }
            return builder.ToString();
        }


    }
}
