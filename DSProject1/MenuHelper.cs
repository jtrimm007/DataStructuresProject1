///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1
//	File Name:         MenuHelper.cs
//	Description:       MenuHelper is a helper class to add functionality to the MenuDemo provided by Dr. Bailes. 
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/3/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace DSProject1
{
    using MenuClassDemo;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// <see cref="MenuHelper" /> add functionality to Dr. Bailes MenuDemo package.
    /// </summary>
    public class MenuHelper
    {
        /// <summary>
        /// A global storage place to put lines of text when reading by the line from a document...
        /// </summary>
        public static List<string> FileReadByLine = new List<string>();

        /// <summary>
        /// The DisplayListOfWords takes in a list of words, formates the output and writes the results to the console.
        /// </summary>
        /// <param name="listOfWords">The listOfWords<see cref="List{string}"/>.</param>
        public static void DisplayListOfWords(List<string> listOfWords)
        {
            // Set the count for the number to display beside the word, showing what number in the list it is - not the index number.
            int count = 1;

            // Loop through the list of words
            foreach (var word in listOfWords)
            {
                // Display each word
                Console.WriteLine("{0}. {1}", count.ToString().PadLeft(5).PadRight(3), word);
                count++;
            }
        }

        /// <summary>
        /// The HeadingForWordCount is used to formate the header section of DisplayListOfWords.
        /// </summary>
        public static void HeadingForWordCount()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" {0} {1}", "Count", "Word");
            Console.Write(" ");
            Tool.WriteDashes(5);
            Console.Write(" ");
            Tool.WriteDashes(7);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        /// <summary>
        /// The MenuChoice monitors the users menu selection.
        /// </summary>
        /// <param name="choice">The choice<see cref="Choices"/>.</param>
        public static void MenuChoice(Choices choice)
        {
            // While loop pulled from Dr. Bailes MenuDemoDriver
            while (choice != Choices.QUIT)
            {
                // check what option the user chose
                switch (choice)
                {
                    case Choices.OPEN: // user chose to select a file to open, #1

                        // Prompt the user with a open dialog box and save their selected files text
                        OpenFileAndSaveText();

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        break;

                    case Choices.EDIT: // user chose to formate the text for output, #2

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Format text for output");


                        // Check to see if a file's text has been saved
                        if (Project1Driver.TextFromFile == null || Project1Driver.TextFromFile == String.Empty)
                        {
                            // Prompt the user with a open dialog box and save their selected files text
                            OpenFileAndSaveText();
                        }



                        // Tokenize the text that was read in. 
                        var tokenizeString = Tool.Tokenize(Project1Driver.TextFromFile, Project1Driver.GlobalDelimiters);

                        // Prompt user to enter left margin
                        Console.WriteLine("Enter left margin: ");
                        string leftMargin = Console.ReadLine();

                        // Prompt user to enter right margin
                        Console.WriteLine("Enter right margin: ");
                        string rightMargin = Console.ReadLine();

                        if(Int32.Parse(rightMargin) <= Int32.Parse(leftMargin))
                        {
                            Console.WriteLine("The Right Margin cannot be greater or equal too than the Left Margin. Please come back and try again. Press enter to continue.");
                            Console.ReadKey();
                            break;
                        }

                        // Put the string back together and add padding
                        var formattedString = Tool.Format(tokenizeString, Int32.Parse(leftMargin), Int32.Parse(rightMargin));

                        //Display the string
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(formattedString);

                        Console.ReadKey();
                        break;

                    case Choices.CLOSE: // user chose to display all the words, #3

                        // Check if a file has already been read in
                        if (Project1Driver.TextFromFile != null || Project1Driver.TextFromFile != String.Empty)
                        {
                            // Turn the text form the file into a list of words
                            var listOfWords = Tool.Tokenize(Project1Driver.TextFromFile, Project1Driver.GlobalDelimiters);

                            // Display the heading for the word count
                            HeadingForWordCount();

                            // Display all the words in the list
                            DisplayListOfWords(listOfWords);
                        }
                        else // if the text fill holder is empty
                        {
                            // Prompt the user to select a text file
                            OpenFileAndSaveText();

                            // Turn the text form the file into a list of words
                            var listOfWords = Tool.Tokenize(Project1Driver.TextFromFile, Project1Driver.GlobalDelimiters);

                            // Display the heading for the word count
                            HeadingForWordCount();

                            // Display all the words in the list
                            DisplayListOfWords(listOfWords);
                        }
                        Console.ReadKey();
                        break;
                }  // end of switch


                choice = (Choices)Tool.Menu.GetChoice();
            }  // end of while

            // Display the Goodbye message when the user selects to leave
            Tool.GoodbyeMessage("Goodbye, Goober!");
        }

        /// <summary>
        /// The OpenFileAndSaveText prompts the user with an open dialog box. After the user chooses a text file, the text is gathered from the file, saved to a global variable for later use and displayed in the console.
        /// </summary>
        public static void OpenFileAndSaveText()
        {
            // Instantiate the dialog box.
            OpenFileDialog openFileDialog = OpenDialogBox();

            // If the user click cancel, close the dialog box
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            // Instantiate a StreamRead for later use
            StreamReader streamReader = null;

            try
            {
                // Get the file chosen from the dialog box
                streamReader = new StreamReader(openFileDialog.FileName);

                // Read the files text to the end and put it in a global static variable
                Project1Driver.TextFromFile = streamReader.ReadToEnd();

                Console.Write("Please specify delimiters: ");
                Project1Driver.GlobalDelimiters = Console.ReadLine();

                // Turn the text form the file that was opened into a list of words
                List<string> fileContentTurnedIntoList = Tool.Tokenize(Project1Driver.TextFromFile, Project1Driver.GlobalDelimiters);


            }
            catch (Exception error)
            {
                Console.WriteLine(error); // if something happens display the exception
            }
            finally
            {
                streamReader.Close(); // close the StreamReader
            }
        }

        /// <summary>
        /// The OpenDialogBox opens a open/save dialog box for the user to choose a text file.
        /// </summary>
        /// <returns>A <see cref="OpenFileDialog"/>.</returns>
        private static OpenFileDialog OpenDialogBox()
        {
            // Instantiate a Open/save dialog box and define the initial directory, types of files it can open, and title. 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = $@"{Application.StartupPath}.\.\TestData";
            openFileDialog.Title = "Select a Text File";
            openFileDialog.Filter = "text files|*.txt";
            return openFileDialog;
        }
    }
}
