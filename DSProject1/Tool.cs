///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1
//	File Name:         Tool.cs
//	Description:       Is a static class used manipulate strings and dialog boxes. 
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/3/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace DSProject1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;
    using Menu = UtilityNamespace.Menu;
    using MessageBox = System.Windows.Forms.MessageBox;

    /// <summary>
    /// <see cref="Tool" /> is used to manipulate text strings.
    /// </summary>
    public static class Tool
    {
        /// <summary>
        /// Gets the Menu. Menu is set in the Setup() method.
        /// </summary>
        public static Menu Menu { get; private set; }

        /// <summary>
        /// The WelcomeMessage displays a MessageBox pop-up.
        /// </summary>
        /// <param name="message">The message<see cref="String"/>.</param>
        /// <param name="caption">The caption<see cref="String"/>.</param>
        /// <param name="author">The author<see cref="String"/>.</param>
        public static void WelcomeMessage(String message, String caption, String author)
        {
            MessageBox.Show($"{DateTime.Now}\n {message}", $"{caption} - {author}", (MessageBoxButtons)MessageBoxButton.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The GoodbyeMessage prompts the user with a goodbye message with the MessageBox.
        /// </summary>
        /// <param name="message">The message<see cref="String"/>.</param>
        public static void GoodbyeMessage(String message)
        {

            MessageBox.Show($"{DateTime.Now}\n {message}", $"Goodbye and Thank You", (MessageBoxButtons)MessageBoxButton.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The Setup is used to set define how the console will look and prompt the user with a welcome message. It will also define what the menu items will say.
        /// </summary>
        public static void Setup()
        {

            // Prompt the user with a welcome message
            WelcomeMessage("Welcome to my console app!", "DS Project 1", "Joshua Trimm");

            // Set the color of the console
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();

            // Instantiate Dr. Bailes Menu class.
            Menu menu = new Menu("Menu Demo");

            // Add items to the menu
            menu = menu + "Open a file" + "Format text for output" + "Display all the words" + "Exit the program";

            // Set the menu to a global variable. 
            Menu = menu;
        }

        /// <summary>
        /// The CleanString returns a string that has been cleansed.
        /// </summary>
        /// <param name="work">The work<see cref="string"/>.</param>
        /// <returns>A cleaned <see cref="string"/>.</returns>
        public static string CleanString(ref string work)
        {

            // Remove all the whitespace
            work = work.Trim();

            // Remove any leading or trailing tabs
            work = work.Trim('\t');

            // Replace any carriage -return, new line combinations(“\r\n”) with a new line(“\n”) character.
            work = work.Replace(@"\r\n", @"\n");

            // Return the cleaned string
            return work;
        }

        /// <summary>
        /// The Tokenize method takes a string and breaks the string into words based on the delimiters parameter.
        /// </summary>
        /// <param name="original">The original<see cref="string"/>.</param>
        /// <param name="delimiters">The delimiters<see cref="string"/>.</param>
        /// <returns>A <see cref="List{String}"/> of words.</returns>
        public static List<String> Tokenize(string original, string delimiters)
        {
            // Make a shallow copy to work with. 
            string workTemp = original;

            // Instantiate a List to add the words to later
            List<string> words = new List<string>();

            // while there is still charters in the string; loop. 
            while (workTemp != string.Empty)
            {
                // Get the position of the next delimiter.
                int indexOfDelimiter = workTemp.IndexOfAny(delimiters.ToCharArray());

                // If workTemp does not start with a delimiter
                if (indexOfDelimiter > 0)
                {


                    // Add the word to the list of words
                    words.Add(workTemp.Substring(0, indexOfDelimiter));

                    // Add the delimiter as a word to the list of words
                    words.Add(workTemp.ElementAt(indexOfDelimiter).ToString());

                    // Remove the word and the delimiter
                    workTemp = workTemp.Substring(indexOfDelimiter + 1).Trim();
                }

                // If a delimiter is in index 0
                if (indexOfDelimiter == 0)
                {
                    // Add the delimiter as a string to the words list
                    words.Add(workTemp.ElementAt(0).ToString());

                    // Remove the delimiter from the workTemp string
                    workTemp = workTemp.Substring(1).Trim();
                }

                // If there are no more delimiters left
                if (indexOfDelimiter == -1)
                {
                    // add the remaining as one word to the list of words
                    words.Add(workTemp);

                    // remove the last characters from the string
                    workTemp = string.Empty;
                }
            }

            // Remove all the spaces that are listed in the List of strings. 
            words = RemoveAllSpacesFromList(words);

            // Return the List of words. 
            return words;
        }

        /// <summary>
        /// The RemoveAllSpacesFromList removes all the spaces from the list.
        /// </summary>
        /// <param name="words">The words<see cref="List{string}"/>.</param>
        /// <returns>A <see cref="List{string}"/> with no spaces in the list.</returns>
        private static List<string> RemoveAllSpacesFromList(List<string> words)
        {
            // Loop through the list of word to see if any spaces where defined as words
            for (var i = 0; i < words.Count; i++)
            {
                if (words[i] == " ")
                {
                    // Remove the space if found
                    words.RemoveAt(i);
                }
            }

            // Return the updated List of words. 
            return words;
        }

        /// <summary>
        /// The Format method takes a list of words and creates a single string. Then adds padding/margin to each side of that string.
        /// </summary>
        /// <param name="list">The list<see cref="List{string}"/>.</param>
        /// <param name="leftMargin">The leftMargin<see cref="int"/>.</param>
        /// <param name="rightMargin">The rightMargin<see cref="int"/>.</param>
        /// <returns>A formatted <see cref="String"/>.</returns>
        public static String Format(List<string> list, int leftMargin, int rightMargin)
        {
            // Create an empty string to put the list of words into
            string listTurnedIntoAString = "";

            // Define the type of punctuation that is acceptable for have a space after.
            string punctuation = ".,?!-;";

            // turn the list of strings into one string
            foreach (string item in list)
            {
                // if we find a punctuation mark remove the space before it
                if (item.IndexOfAny(punctuation.ToCharArray()) == 0 && listTurnedIntoAString != String.Empty)
                {
                    // Remove the space before the punctuation mark
                    listTurnedIntoAString = listTurnedIntoAString.Remove(listTurnedIntoAString.Length - 1);
                }

                // Add the next word to the string. 
                listTurnedIntoAString += $"{item} ";
            }

            // Number of charters between each margin
            int betweenMarginSpacing = rightMargin - leftMargin;

            // Placeholder for the a new formatted string
            string formattedString = "";


            while (listTurnedIntoAString != String.Empty)
            {
                // check if we are close to the end of the list
                if (listTurnedIntoAString.Length > betweenMarginSpacing)
                {
                    // format each line
                    formattedString += $"{listTurnedIntoAString.Substring(0, betweenMarginSpacing).PadLeft(leftMargin + betweenMarginSpacing)}\n";

                    // remove the text from the unformatted string after it has been added to the formatted string. 
                    listTurnedIntoAString = listTurnedIntoAString.Substring(betweenMarginSpacing);
                }
                else // we are at the end of the string
                {
                    // append the rest of the string to the formatted string.
                    formattedString += listTurnedIntoAString.PadLeft(leftMargin + listTurnedIntoAString.Length);

                    // declare the formatted string empty
                    listTurnedIntoAString = String.Empty;
                }
            }

            // Return the formated string
            return formattedString;
        }

        /// <summary>
        /// The WriteDashes will write the number of dashes passed in as a parameter.
        /// </summary>
        /// <param name="numberOfDashes">The numberOfDashes<see cref="int"/>.</param>
        public static void WriteDashes(int numberOfDashes)
        {
            for (int i = 0; i < numberOfDashes; i++)
            {
                Console.Write("-");
            }
        }
    }
}
