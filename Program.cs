using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CeasarCipher
{
    class Program
    {
        //Create a Ceasar object so I can decipher the cipher text
        Ceasar ceasar = new Ceasar();

        //Create an object so I can ecipher the modified caesar cipher.
        ModifiedCaesar modCeasar = new ModifiedCaesar();

        static void Main(string[] args)
        {
            new Program(); //Start my program

            //string cipherText = File.ReadAllText("ceasarShiftEnhancedEncoding.txt");

            //
            //mc.Decipher(cipherText);
           // mc.BruteForce(cipherText);

            //Console.ReadLine();
        }

        public Program()
        {
            int choice = -1;  //Current selected choice. 0=exit, 1=brute, 2=modified, 3=show help

            //I want to run the below code at least once, time for a do while :D
            do
            {
                //Prompt user for input
                Console.WriteLine("Please select a choice.");
                Console.WriteLine("\t0 - Exit");
                Console.WriteLine("\t1 - Brute force caesar cipher");
                Console.WriteLine("\t2 - Crack the modified caesar cipher");
                Console.WriteLine("\t3 - Encrypt file contents using the caesar cipher");
                Console.WriteLine("\t4 - Help");

                string line = Console.ReadLine(); //Read input

                //Try parse will return false if it cannot change a string to an int
                if (!int.TryParse(line, out choice) || !( choice >= 0 && choice <= 4) )
                { //If string entered isn't a number or isn't one of the choices

                    Console.WriteLine("\nSorry, \"{0}\" isn't a valid option. Please try again.\n\n", line); //Tell them it's not valid
                    choice = -1;//Make sure loop can reset
                    continue;//Move on...
                }

                if (choice == 1)  //Both the brute force and the freq analysis need the same arguments, might as well put them under the same IF statement
                {
                    //Show another prompt, telling the user they can just enter nothing.
                    Console.WriteLine("\n\nPlease type the name of the file to open (leave blank for the default value)");
                    Console.WriteLine("Please make sure that the file exists in the same directory as this program.");
                    Console.WriteLine("Default = \"caesarShiftEncodedText.txt\"");

                    string filename = Console.ReadLine();
                    if (string.IsNullOrEmpty(filename)) //if they just pressed enter/entered nothing
                        filename = "caesarShiftEncodedText.txt";//use default file.

                    if (!File.Exists(filename)) //If the file doesn't exist
                    {
                        //Tell them
                        Console.WriteLine("\nSorry, the file \"{0}\" doesn't exist in the same directory of this program\n\n", filename);
                        continue; //I know that choice isn't 0, i can just reset the loop
                    }

                    //The file must exist otherwise, we wouldn't be here
                    string cipherText = File.ReadAllText(filename); //Read all the text in the file, store it in the string

                    //Perform bruteforce
                    BruteForce(cipherText);
                    //else //They want to also perform the frequency analysis
                        //PerformFreqAnalysis(cipherText);

                }
                else if (choice == 2)
                {
                    //Show another prompt, telling the user they can just enter nothing.
                    Console.WriteLine("\n\nPlease type the name of the file to open (leave blank for the default value)");
                    Console.WriteLine("Please make sure that the file exists in the same directory as this program.");
                    Console.WriteLine("Default = \"ceasarShiftEnhancedEncoding.txt\"");

                    string filename = Console.ReadLine();
                    if (string.IsNullOrEmpty(filename)) //if they just pressed enter/entered nothing
                        filename = "ceasarShiftEnhancedEncoding.txt";//use default file.

                    if (!File.Exists(filename)) //If the file doesn't exist
                    {
                        //Tell them
                        Console.WriteLine("\nSorry, the file \"{0}\" doesn't exist in the same directory of this program\n\n", filename);
                        continue; //I know that choice isn't 0, i can just reset the loop
                    }

                    string cipherText = File.ReadAllText(filename); //Read all the text in the file, store it in the string
                    ModifiedCaesar(cipherText);

                }
                else if (choice == 3)//If they want to encrypt the text in a file
                {
                    Console.WriteLine("\n\nPlease type the name of the text file you want to encrypt.");
                    Console.WriteLine("Please make sure that the file exists and has text inside.");
                    Console.WriteLine("Default = \"plainText.txt\"");

                    string filename = Console.ReadLine();
                    if (string.IsNullOrEmpty(filename))
                        filename = "plainText.txt";

                    if (!File.Exists(filename))
                    {
                        Console.WriteLine("Sorry but the file \"{0}\" doesn't exist in the same directory as this program.", filename);
                        continue;
                    }

                    int shift;
                    Console.WriteLine("Please enter the amount of shift to encrypt the text by.");
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out shift))
                    {
                        //Couldn't parse to a string.
                        Console.WriteLine("Sorry, \"{0}\" isn't a valid shift amount. It must be a whole integer.", input);
                        continue;
                    }
                    string plainText = File.ReadAllText(filename);

                    EncryptText(plainText, shift);
                }
                else if(choice == 4) //If they want to show the help text
                {
                    Help();
                }
            } while (choice != 0); //While the choice isn't "exit"
        }

        /// <summary>
        /// Encrypts a given string with the given shift amount
        /// </summary>
        /// <param name="plainText">The text to encrypt</param>
        /// <param name="shift">The amount of shift to use when encrypting</param>
        void EncryptText(string plainText, int shift)
        {
            var stopwatch = Stopwatch.StartNew(); //Create a stopwatch object, used to calculate execution time

            StringBuilder toWriteToFile = new StringBuilder();

            //Format the data for the file.
            toWriteToFile.Append("Your encrypted text is shown below:\n");
            toWriteToFile.Append(string.Format("Shift used: {0}\n\nEncrypted Text:\n", shift));
            toWriteToFile.Append(ceasar.Encrypt(plainText, shift));

            toWriteToFile.Append(string.Format("\n\nExecution time {0}", stopwatch.Elapsed.Milliseconds));
            stopwatch.Stop(); //Stop the stopwatch, the heavy lifting is done.

            //Show a prompt for the output file.
            Console.WriteLine("\nPlease enter a filename to output the contents to (leave blank for default value)");
            Console.WriteLine("This will overwrite any existing file.");
            Console.WriteLine("Default = \"encryptedText.txt\"\n\n");

            string filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename)) //If they just pressed enter (they want the default file)
                filename = "encryptedText.txt";

            File.WriteAllText(filename, toWriteToFile.ToString()); //Write all the text to the file

            //Tell the user we're done
            Console.WriteLine("All encrypted text has been outputted to \"{0}\".\nTotal time take was {1}ms\n\n", filename, stopwatch.Elapsed.Milliseconds);
        }

        /// <summary>
        /// Prints help to the console, telling the user what each option does
        /// </summary>
        void Help()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("0 - Exit the program");
            Console.WriteLine("1 - Brute force the caesar cipher (outputs all posibilities to a file)");
            Console.WriteLine("2 - Crack the modified caesar cipher");
            Console.WriteLine("3 - Encrypt text file using the caesar cipher");
            Console.WriteLine("4 - Shows this help text");
            Console.WriteLine("========================================\n");
        }

        void ModifiedCaesar(string cipherText)
        {
            var stopwatch = Stopwatch.StartNew(); //Create a stopwatch object, used to calculate execution time
            StringBuilder toWriteToFile = new StringBuilder(); //Used to store what I'm going to write to the file.

            toWriteToFile.Append(modCeasar.BruteForce(cipherText)); //Decipher the text and add to the string builder

            toWriteToFile.Append(string.Format("\nExecution Time: {0}ms", stopwatch.Elapsed.Milliseconds)); //Append execution time to output.
            stopwatch.Stop();

            //Ask user where they want to save file to.
            Console.WriteLine("\nPlease enter a filename to output the contents to (leave blank for default value)");
            Console.WriteLine("This will overwrite any existing file.");
            Console.WriteLine("Default = \"caesarEnhancedPlainText.txt\"\n\n");

            string filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename)) //if they just pressed enter/entered nothing
                filename = "caesarEnhancedPlainText.txt";//use default file.

            File.WriteAllText(filename, toWriteToFile.ToString());//Write all data to file specified

            Console.WriteLine("Deciphered solution written to file \"{0}\"\nTotal Time Taken: {1}ms\n\n", filename, stopwatch.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// Decipher the encryptedText using all possible shifts (1-25) and outputs them to a file
        /// </summary>
        /// <param name="encryptedText">The ciphered text to brute force</param>
        void BruteForce(string encryptedText)
        {
            var stopwatch = Stopwatch.StartNew(); //Create a stopwatch object, used to calculate execution time
            StringBuilder toWriteToFile = new StringBuilder(); //Used to store what I'm going to write to the file.

            //Write all posible solutions to builder.
            toWriteToFile.Append(ceasar.BruteForce(encryptedText));

            //Add the execution time to the end of the file.
            toWriteToFile.Append(string.Format("\n\nExecution Time: {0}ms", stopwatch.Elapsed.TotalMilliseconds));

            //Stop the stopwatch, the main stuff have passed
            stopwatch.Stop();

            //Show prompt for output file to user.
            Console.WriteLine("\nPlease enter a filename to output the contents to (leave blank for default value)");
            Console.WriteLine("This will overwrite any existing file.");
            Console.WriteLine("Default = \"caesarShiftPlainText.txt\"\n\n");

            string filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename)) //if they just pressed enter/entered nothing
                filename = "caesarShiftPlainText.txt";//use default file.

            //Write all the text in the stringbuilder to the file given
            File.WriteAllText(filename, toWriteToFile.ToString());

            //Tell user that i've done and where to find the output
            Console.WriteLine("All possible solutions written to file \"{0}\"\nTotal Time Taken: {1}ms\n\n", filename, stopwatch.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// Cycle thorugh all possible cipher shifts and find the most probable solution using frequency analysis.
        /// NOT USED :(
        /// </summary>
        /// <param name="encryptedText">The ciphered text</param>
        //void PerformFreqAnalysis(string encryptedText)
        //{
        //    var stopwatch = Stopwatch.StartNew();//Create a stopwatch object, used to calculate execution time

        //    StringBuilder toWriteToFile = new StringBuilder();//Used to store what I'm going to write to the file.

        //    double bestDiff = -1; //best difference we've found so far
        //    int shift = 0; //The amount of shift we applied to get the best solution
        //    string deciphered = "Didn't find a match :("; //Store the best match. Better than running the decrypt method again.
        //    //For each possible shift
        //    for (int i = 1; i <= 25; i++)
        //    {
        //        string shifted = ceasar.Decrypt(encryptedText, i); //Get the deciphered text

        //        CharacterFrequencyList myList = freqAn.AnalyseFrequency(shifted); //Get the frequencies of the characters
        //        double myDiff = freqAn.GetDifference(myList, shifted); //Get the difference the deciphered frequencies has from the english frquencies

        //        if (bestDiff == -1) //If i've just started this loop
        //            bestDiff = myDiff; //Set the best to the first shift

        //        if (myDiff > bestDiff)//If we've found a better match
        //        {
        //            bestDiff = myDiff; //Store the better match to check against over shifts
        //            shift = i; //Store the shift amount we used to get the best match
        //            deciphered = shifted;
        //        }
        //    }

        //    //Tell user we've got the best solution
        //    Console.WriteLine("I've calculated the best solution...");

        //    //Make sure the information we want to write to the file is in the stringbuilder
        //    toWriteToFile.Append("Best Solution :D\n");
        //    toWriteToFile.Append(string.Format("Shift Amount: {0}\n", shift));
        //    toWriteToFile.Append(string.Format("Probablity of correctness: {0}%\n\n", bestDiff));
        //    toWriteToFile.Append(string.Format("Solution:\n{0}", deciphered));
        //    toWriteToFile.Append(string.Format("\n\nExecution Time: {0}ms", stopwatch.Elapsed.TotalMilliseconds));

        //    stopwatch.Stop(); //Stop the stopwatch, the main execution has ended

        //    //Prompt the user to enter an output file name
        //    Console.WriteLine("\nPlease enter a filename to output the contents to (leave blank for default value)");
        //    Console.WriteLine("This will overwrite any existing file.");
        //    Console.WriteLine("Default = \"caesarShiftFreqAnalysis.txt\"\n\n");

        //    string filename = Console.ReadLine();
        //    if (string.IsNullOrEmpty(filename)) //if they just pressed enter/entered nothing
        //        filename = "caesarShiftFreqAnalysis.txt";//use default file.

        //    //Write all the content of stringbuilder to file
        //    File.WriteAllText(filename, toWriteToFile.ToString());

        //    //Tell user we've finished
        //    Console.WriteLine("The best probable solution was written to the file \"{0}\"\nTotal Time Taken: {1}ms\n\n", filename, stopwatch.Elapsed.TotalMilliseconds);
        //}
    }
}