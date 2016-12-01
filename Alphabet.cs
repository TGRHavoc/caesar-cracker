using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeasarCipher
{
    public class Alphabet
    {
        readonly List<char> alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList(); //Lists are much better to work with :)

        public int Length
        {
            get
            {
                return alphabet.Count;
            }
        }

        public char GetCharacterAt(int index)
        {
            int myIndex = index % (alphabet.Count-1);
            if ((index < 0 && (alphabet.Count - 1) > 0) || (index > 0 && (alphabet.Count-1) < 0))
                myIndex += (alphabet.Count - 1);
            try{
                return alphabet[myIndex];
            }catch (IndexOutOfRangeException e){
                Console.WriteLine("\nError: {0}\n\n", e.Message);
                return '\0';
            }
        }

        public int GetCharacterIndex(char character)
        {
            return alphabet.IndexOf(character);
        }
    }
}
