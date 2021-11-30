using System;
using System.IO;

namespace EmulatorCalculator_Part4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // let me know if you can see this screen
            // In advance note: This is not humanity readable.
            // I will demo: Accept inputs from a file, perform the required operations, then output those results to a file
            // But, first thing, i want to show you my file before and after run the program by use dotnet run.
            // Math will only read left to right. Up to three digits
            
            Program p = new Program(); // This will allow me to call methods
            
            // store the input line into this varaibale
            List<string> getInputFromFile = new List<string>(); 
            
            // Divide string into parts into this varaiable from GetInputFromFile
            List<string> userList;
            
            // Call readFile Method to get the information into List<string> getInputFromFIle 
            getInputFromFile = p.readFile(getInputFromFile);

            // To assign each input into getInput varaible
            for(int i = 0; i < getInputFromFile.Count; i++)
            {
                string? getInput = getInputFromFile[i];
                userList = new List<string>();
                double result = 0.00D;
                
                // Assign individual string into List<string> userList 
                for(int j = 0; j < getInput!.Length; j++)
                {
                    if(!getInput.Substring(j, 1).Equals(" ") ) // Check if getInput do not have any spacebar
                    {
                        // Check if j variable are still under string's length then check if this string have three digits (000)
                        if(j + 2 < getInput!.Length && Char.IsNumber(getInput[j]) && Char.IsNumber(getInput[j+1]) && Char.IsNumber(getInput[j+2]) )
                        {
                            
                            userList.Add(getInput.Substring(j, 3));
                            j += 2; // Skip two loop 
                        }
                        // Check if j variable are still under string's length, then check if this string have two digits (00)
                        else if(j + 1 < getInput!.Length && Char.IsNumber(getInput[j])  && Char.IsNumber(getInput[j+1]))
                        {                        
                            
                            userList.Add(getInput.Substring(j, 2));
                            j++; //skip one loop
                        }
                        // this string is only have one digit (0)
                        else 
                        {
                            
                            userList.Add(getInput.Substring(j, 1));
                        }
                    }
                }

                // Start doing calculator
                for(int k = 0; k < userList.Count - 1; k++)
                {
                    double testNum;
                    // Start with beginning string[0] to string[2] ex:(3 + 8)
                    if(k == 0) 
                    {
                        if(Double.TryParse(userList[k], out testNum))
                        {
                            result = p.switchStatement(userList[k+1], Double.Parse(userList[k]), Double.Parse(userList[k+2]) );
                        }
                        // help to skip to string[3]
                        k++; 
                    }
                    // Start with string[3] and up  ex: (11 + 10)
                    else if( k > 2 && userList.Count > 3)
                    {
                        if(Double.TryParse(userList[k+1], out testNum))
                        {
                            result = p.switchStatement(userList[k], result, Double.Parse(userList[k+1]));
                        }
                    }
                }

                // Append the result into file
                p.writeFile(i+1, getInput, result);
            }
        }

        // Use StreamReader to get the inputs from file and store to List<string> inputFile
        public List<string> readFile(List<string> inputFile)
        {
            string? path = "./getInput.txt";
            if(File.Exists(path)) // Check if we have the file
            {
                Console.WriteLine("File Found");
                using (StreamReader sr = new StreamReader(path))
                {
                    string? line;
                    while( (line = sr.ReadLine()) != null)
                    {
                        inputFile.Add(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("File Not Found.");
            }

            return inputFile;
        }

        // Use AppendText to output the result into file
        public void writeFile(int i, string? getInput, double result)
        {
            string? path = "./getInput.txt";
            
            if(File.Exists(path)) // Check if we have the file
            {
                StreamWriter writer = File.AppendText(path);
                writer.WriteLine("\n{0}) {1} = {2}", i, getInput, result);
                writer.Close();
            }
            else 
            {
                Console.WriteLine("File Not Found");
            }

        }

        // Use Switch Statement to check our Math Operator (+, -, *, /, %)
        public double switchStatement(string? userOperator, double num1, double num2)
        {
            double totalNum = 0.00D;
            //Console.WriteLine("Test: {0} {1} {2}", userOperator, num1, num2);
            switch(userOperator)
            {
                case "+": totalNum = num1 + num2; break;
                case "-": totalNum = num1 - num2; break;
                case "*": totalNum = num1 * num2; break;
                case "/": totalNum = num1 / num2; break;
                case "%": totalNum = num1 % num2; break;
            }
            return totalNum;
        }
    }
}


// By nomral, I do have allow user to choose exit, however, when i do this code, i found lots of error that i don't except. So i focus to fix this code
// After I finish fix this code, then don't realize that i don't have time to add exit option. So this is my best code so far.