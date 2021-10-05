///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1
//	File Name:         Project1Driver.cs
//	Description:       The project driver is where the code starts running. Specifically, in the main method.
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

    /// <summary>
    /// <see cref="Project1Driver" /> is where the program starts.
    /// </summary>
    public class Project1Driver
    {
        /// <summary>
        /// Globally defined delimiters.
        /// </summary>
        public static string GlobalDelimiters = ",.!? ";

        /// <summary>
        /// Holder for the text read from the TestData files.
        /// </summary>
        public static string TextFromFile = null;

        /// <summary>
        /// The Main, it runs everything.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            // Setup the program
            Tool.Setup();

            // Form Dr. Bailes code to setup the menu
            Choices choice = (Choices)Tool.Menu.GetChoice();

            // Display the menu and monitor the users input.
            MenuHelper.MenuChoice(choice);
        }
    }
}
