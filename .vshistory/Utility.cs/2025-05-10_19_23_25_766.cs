using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public static class Utility
    {
        /*  GetIntInRange() 
*************************************************
Purpose: Takes input from user over and over until
input is an integer falling within the range specified
during function call
*************************************************
@Algorithm:
Get user input
Check if input is greater than minimum value and
less that maximum value
Loop until input is correct
Return input 
*************************************************
@Param
Receives 2 parameters:
int minValue: the minimum allowed value for the 
return
int maxValue: the maximum allowed value for the 
return

*************************************************
@Exceptions
None
*************************************************
@Returns
Returns an integer greater than minValue and less
er than maxValue

*************************************************
@Examples

int choice; //Needs to be between 1 and 6
choice = GetIntInRange(1,6)

int month; //Needs to be between 1 and 12
month = GetIntInRange(1,12)

*************************************************
@Pseudocode
 Declare bool isInRange, set to false
 Declare int userInput
 Begin do while loop: while (isInRange = false)
     Call function IntParse with Console.ReadLine() as argument
     Set userInput to return value
     Set isInRange to (userInput >= minValue && userInput <= maxValue
     Begin if statement: if (isInRange= false)
         Output error message
     End of if statement
 End of do while

 Return userInput
************************************************* 
*/
        public static int GetIntInRange(int minValue, int maxValue)
        {
            bool isInRange = false;
            int userInput;
            do
            {
                userInput = IntParse(Console.ReadLine());
                isInRange = (userInput >= minValue && userInput <= maxValue);
                if (!isInRange)
                {
                    Console.WriteLine("Your input was not within a reasonable range.");
                }
            } while (!isInRange);
            return userInput;
        }
        public static double GetDoubleInRange(double minValue, double maxValue, string inputTooLowMessage = "Your input was not within a reasonable range: too low", string inputTooHighMessage = "Your input was not within a reasonable range: too high")
        {
            bool isInRange = false;
            double userInput;
            do
            {
                userInput = DoubleParse(Console.ReadLine());
                isInRange = (userInput >= minValue && userInput <= maxValue);
                if (userInput < minValue)
                {
                    Console.WriteLine(inputTooLowMessage);
                }
                else if (userInput > maxValue)
                {
                    Console.WriteLine(inputTooHighMessage)     ;
                }
                
            } while (!isInRange);
            return userInput;
        } //functions identically to GetIntInRange, but with doubles

        /* IntParse()
           *************************************************
           Purpose: Receives input from user and attempts to convert
           to integer. If successful, returns converted int without prompting user.
           If unsuccessful, prompts user for input and loops until input is correct.
           *************************************************
           @Algorithm:
           Receives user input under Dynamic data type
           Attempts a TryParse to integer, immediately returns value if succesful
           If integer TryParse is unsuccessful, loops until valid integer is given
           Returns userInput
           *************************************************
           @Param
           Receives 1 parameters:
           dynamic userInput: Holds parameter of unknown data 
           type that needs to be converted to integer

           *************************************************
           @Exceptions
           None
           *************************************************
           @Returns
           Returns an integer
           *************************************************
           @Examples

           int number; //Needs to be an integer
           number = IntParse(Console.ReadLine());

           int year; //Needs to be an integer
           year = IntParse(Console.ReadLine());
           *************************************************
           @Pseudocode
            Declare bool isNum;
            Declare int validInt;
            Set isNum to result of int.TryParse operation on userInput with out of validInt
            Begin while loop: while (isNum is false)
                Output error message
                Assign userInput to Console.ReadLine()
                Set isNum to result of int.TryParse operation on userInput with out of validInt
            End of while loop
            return validInt
           ************************************************* 
           */
        public static int IntParse(dynamic userInput)
        {
            //Takes an input, repeatedly attempts to convert to integer
            bool isNum;
            int validInt;

            isNum = int.TryParse(userInput, out validInt);
            while (isNum == false)// loops until correct input is given
            {
                Console.WriteLine("Invalid input. Please input a valid integer");
                userInput = Console.ReadLine();
                isNum = int.TryParse(userInput, out validInt);
            }
            return validInt; // Matches function data type
        }
        /* DoubleParse()
   *************************************************
   Purpose: Receives input from user and attempts to convert
   to double. If successful, returns converted int without prompting user.
   If unsuccessful, prompts user for input and loops until input is correct.
   *************************************************
   @Algorithm:
   Receives user input under Dynamic data type
   Attempts a TryParse to double, immediately returns value if succesful
   If double TryParse is unsuccessful, loops until valid double is given
   Returns userInput
   *************************************************
   @Param
   Receives 1 parameters:
   dynamic userInput: Holds parameter of unknown data 
   type that needs to be converted to integer

   *************************************************
   @Exceptions
   None
   *************************************************
   @Returns
   Returns an integer
   *************************************************
   @Examples

   int number; 
   number = DoubleParse(Console.ReadLine());

   int year; 
   year = DoubleParse(Console.ReadLine());
   *************************************************
   @Pseudocode
    Declare bool isNum;
    Declare double validDouble;
    Set isNum to result of double.TryParse operation on userInput with out of validDouble
    Begin while loop: while (isNum is false)
        Output error message
        Assign userInput to Console.ReadLine()
        Set isNum to result of double.TryParse operation on userInput with out of validDouble
    End of while loop
    return validDouble
   ************************************************* 
   */
        public static double DoubleParse(dynamic userInput)
        {
            //Takes an input, repeatedly attempts to convert to double
            bool isNum;
            double validDouble;

            isNum = double.TryParse(userInput, out validDouble);
            while (isNum == false)// loops until correct input is given
            {
                Console.WriteLine("Invalid input. Please input a valid integer");
                userInput = Console.ReadLine();
                isNum = double.TryParse(userInput, out validDouble);
            }
            return validDouble; // Matches function data type
        }
        /*GetUserConsent() 
          *************************************************
          Purpose: Guarantees a valid boolean value from user
          *************************************************
          @Algorithm:
          Prompt user for string
                Set boolean to check if string is a yes or no
                loop until it is
          return equality of userInput with Y or YES
          *************************************************
          @Param
          string message: displays the condition for which the user hand to return their consent (i.e. the question they're being asked)
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          Boolean: user consent (yes or no)
          *************************************************
          @Pseudocode
          Declare bool isBoolean, set to false
          Declare string userInput
          Begin do while loop:
                    Output message
                    Set userInput to return value of GetNonNullString()
                    Set isBoolean to uppercase user input = Y or Yes or N or NO
                    if (!isBoolean)
                        Output error message
          while (!isBoolean)
          return (uppercase userInput =  Y or Yes)
          ************************************************* 
          */
        public static bool GetUserConsent(string message, bool Animate = false)
        {
            bool isBoolean = false;
            string userInput;

            do
            {
                if (Animate)
                {
                    AnimateWrite(message);
                }
                else
                {
                    Console.WriteLine(message);
                }
                userInput = GetNonNullString();
                isBoolean = (userInput.ToUpper() == "Y" || userInput.ToUpper() == "YES" || userInput.ToUpper() == "N" || userInput.ToUpper() == "NO");
                if (!isBoolean)
                {
                    if (Animate)
                    {
                        AnimateWrite("Invalid input. Please try again.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            } while (!isBoolean);

            return (userInput.ToUpper() == "Y" || userInput.ToUpper() == "YES"); //Anything other than y or yes will return false.
        }
        /*  GetNonNullString
          *************************************************
          Purpose: Takes input from user over and over until
          input is a string that is not empty nor null
          *************************************************
          @Algorithm:
          Get user input
          Check if input is null or empty
          Loop until input is correct
          Return input 
          *************************************************
          @Param
          Receives no parameters
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          Returns a string that isn't null nor empty
          *************************************************
          @Examples

          int word; //needs to be a word, cannot be null
          word = GetNonNullString()

          int favoriteFood; //Cannot be null
          favoriteFood = GetNonNullString()

          *************************************************
          @Pseudocode
           Declare string userInput, set to null

           Return userInput, set to null
           Begin while loop: while (string.IsNullOrEmpty(userInput)
               Set userInput to Console.ReadLine()
               Begin if statement: if (userInput is null or empty)
                   Output error message
               End of if statement
           End of while loop
          ************************************************* 
          */
        public static string GetNonNullString()
        {
            string userInput = null;
            while (string.IsNullOrEmpty(userInput))
            {
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Your input cannot be null.");
                }
            }
            return userInput;
        }
        public static void AnimateWrite(string toWrite)
        {
            for (int i = 0; i < toWrite.Length; i++)
            {
                Console.Write(toWrite[i]);
                Thread.Sleep(1);
            }
            Console.Write("\n");

        } //Functions exactly like Console.WriteLine(), but waits 1 millisecond between each character.
        public static void PrintHeader()
        {
            string seperator = "****************************";
            AnimateWrite(seperator);
            Console.WriteLine("");
            Console.WriteLine("Welcome to Programming 2 - Project - Winter 2025");
            Console.WriteLine("");
            Console.WriteLine("Created by Jimmy Rashed, 6291812, on May 15th, 2025");
            Console.WriteLine("");
            AnimateWrite(seperator);
            Console.ReadKey();
            Console.Clear();
        }
        public static void PrintGameHeader()
        {
            Console.WriteLine("▀█████████▄   ▄█          ▄████████  ▄████████    ▄█   ▄█▄      ▄█    ▄████████  ▄████████    ▄█   ▄█▄ \r\n  ███    ███ ███         ███    ███ ███    ███   ███ ▄███▀     ███   ███    ███ ███    ███   ███ ▄███▀ \r\n  ███    ███ ███         ███    ███ ███    █▀    ███▐██▀       ███   ███    ███ ███    █▀    ███▐██▀   \r\n ▄███▄▄▄██▀  ███         ███    ███ ███         ▄█████▀        ███   ███    ███ ███         ▄█████▀    \r\n▀▀███▀▀▀██▄  ███       ▀███████████ ███        ▀▀█████▄        ███ ▀███████████ ███        ▀▀█████▄    \r\n  ███    ██▄ ███         ███    ███ ███    █▄    ███▐██▄       ███   ███    ███ ███    █▄    ███▐██▄   \r\n  ███    ███ ███▌    ▄   ███    ███ ███    ███   ███ ▀███▄     ███   ███    ███ ███    ███   ███ ▀███▄ \r\n▄█████████▀  █████▄▄██   ███    █▀  ████████▀    ███   ▀█▀ █▄ ▄███   ███    █▀  ████████▀    ███   ▀█▀ \r\n             ▀                                   ▀         ▀▀▀▀▀▀                            ▀         ");
        }
        public static void NewPage()
        {
            Console.Clear();
            PrintGameHeader();
        }
        public static bool DoesFileExist(string file)
        {
            if (File.Exists(file))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
