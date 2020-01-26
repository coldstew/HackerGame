using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Hacker : MonoBehaviour
{    
    private const string menuHint = "You may type menu at any time.";

    private string[] level1Passwords = { "books", "aisle", "self", "password", "font", "borrow"};
    private string[] level2Passwords = {"prisoner", "handcuffs", "peanuts", "mountain", "donkey"};
    private string[] level3Passwords = {"ring", "deutsch", "pistol", "cannonball", "variable"};
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    private string password;

    void Start()
    {
        ShowMainMenu ();
    }

    void ShowMainMenu ()
    {    
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen ();
        Terminal.WriteLine ("What would you like to hack into?");
        Terminal.WriteLine ("Press 1 for the local library");
        Terminal.WriteLine ("Press 2 for the police station");
        Terminal.WriteLine ("Press 2 for NASA!");
        Terminal.WriteLine ("Enter your selection:");
    }
    
    void OnUserInput(string input)
    {
        if ( input == "menu")
        {
            ShowMainMenu ();
        }
        else if (input == "exit")
        {
            Application.Quit(0);
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input =="2" || input=="3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input); 
            AskForPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Please select a level Mr Bonds");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
            Terminal.WriteLine(menuHint);
        }
    }
    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    private void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Please enter your password");
                Terminal.WriteLine(menuHint);
                break;
        }
    }

    private void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Sorry, wrong password!");
            Terminal.WriteLine(menuHint);
        }
    }

    private void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    private void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    ________
   /       //
  /       //
 /_______//
(_______(/
");
                break;
            case 2:
                Terminal.WriteLine("Get up and do some FITNESS!");
                Terminal.WriteLine(@"
─█▀▀──▀──▀█▀──█▀▀▄──▄▀▀──▄▀▀──▄▀▀─
─█────█───█───█──█──█────█────█───
─█▀▀──█───█───█──█──█▀▀───▀▄───▀▄─
─█────█───█───█──█──█──────█────█─
─█────█───█───█──█──▀▄▄──▄▄▀──▄▄▀─
");
                break;
            case 3:
                Terminal.WriteLine("You just hacked THE NASA! Congrats!");
                Terminal.WriteLine(@"
  /\     |\**/|      
 /  \    \ == /
 |  |     |  |
 |  |     |  |
/ == \    \  /
|/**\|     \/
        ");
                break;
            default:
                Debug.LogError("Something is going wrong...");
                break;
        }
        Terminal.WriteLine("WELL DONE!");
    }
}
