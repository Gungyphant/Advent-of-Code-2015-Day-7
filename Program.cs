using System.Runtime.InteropServices;

namespace AOC2015Day7
{
    class Program
    {
        static string PartOne(string data) // This function will always terminate (unless there are circular references), but in the worst case takes n (n + 1) / 2 line attempts to solve
        {
            Dictionary<string, int> variables = new Dictionary<string, int>();
            Queue<string> unprocessedCommands = new Queue<string>();
            foreach (string line in data.Split("\n"))
            {
                if (line != "")
                {
                    unprocessedCommands.Enqueue(line);
                }
            }
            while (unprocessedCommands.Count > 0)
            {
                //Console.WriteLine(unprocessedCommands.Count);
                Queue<string> originalUnprocessedCommands = new Queue<string>(unprocessedCommands);
                unprocessedCommands = new Queue<string>();
                foreach (string line in originalUnprocessedCommands)
                {
                    string[] parts = line.Split(" -> ");
                    string statement = parts[0];
                    string resultantVariable = parts[1];

                    string[] statementParts = statement.Split(" ");

                    if (statementParts.Length == 1) // Assignment
                    {
                        int value;
                        try
                        {
                            value = Convert.ToInt32(statement);
                        }
                        catch (FormatException) // It was assigning a variable, not a value
                        {
                            if (!variables.ContainsKey(statement))
                            {
                                unprocessedCommands.Enqueue(line);
                                continue;
                            }
                            else
                            {
                                value = variables[statement];
                            }
                        }
                        variables[resultantVariable] = value;
                    }
                    else if (statementParts.Length == 2) // ~ (NOT)
                    {
                        string inputVariable = statementParts[1];
                        if (!variables.ContainsKey(inputVariable)) // Variable is not defined; come back later
                        {
                            unprocessedCommands.Enqueue(line);
                        }
                        else
                        {
                            variables[resultantVariable] = ~variables[inputVariable];
                        }
                    }
                    else if (statementParts.Length == 3)
                    {
                        string input1 = statementParts[0];
                        string operation = statementParts[1];
                        string input2 = statementParts[2];
                        switch (operation)
                        {
                            case "AND": // &
                                if (input1 == "1") // Additional possibility not listed in the puzzle text
                                {
                                    if (!variables.ContainsKey(input2))
                                    {
                                        unprocessedCommands.Enqueue(line);
                                    }
                                    else
                                    {
                                        variables[resultantVariable] = 1 & variables[input2];
                                    }
                                }
                                else
                                {
                                    // Both vars
                                    if (!(variables.ContainsKey(input1) && variables.ContainsKey(input2)))
                                    {
                                        unprocessedCommands.Enqueue(line);
                                    }
                                    else
                                    {
                                        variables[resultantVariable] = variables[input1] & variables[input2];
                                    }
                                }
                                break;
                            case "OR": // |
                                // Both vars
                                if (!(variables.ContainsKey(input1) && variables.ContainsKey(input2)))
                                {
                                    unprocessedCommands.Enqueue(line);
                                }
                                else
                                {
                                    variables[resultantVariable] = variables[input1] | variables[input2];
                                }
                                break;

                            case "LSHIFT": // <<
                                // input1 var, input2 int
                                if (!variables.ContainsKey(input1))
                                {
                                    unprocessedCommands.Enqueue(line);
                                }
                                else
                                {
                                    variables[resultantVariable] = variables[input1] << Convert.ToInt32(input2);
                                }
                                break;
                            case "RSHIFT": // >>
                                // input1 var, input2 int
                                if (!variables.ContainsKey(input1))
                                {
                                    unprocessedCommands.Enqueue(line);
                                }
                                else
                                {
                                    variables[resultantVariable] = variables[input1] >> Convert.ToInt32(input2);
                                }
                                break;
                            default:
                                throw new Exception(line);
                        }
                    }
                    else
                    {
                        throw new Exception(line);
                    }
                    if (variables.ContainsKey("a"))
                    {
                        return Convert.ToString(variables["a"]);
                    }
                }
            }
            throw new Exception("No a found");
        }
        static string PartTwo(string data)
        {
            Dictionary<string, int> variables = new Dictionary<string, int>();
            variables["b"] = Convert.ToInt32(PartOne(data));
            Queue<string> unprocessedCommands = new Queue<string>();
            foreach (string line in data.Split("\n"))
            {
                if (line != "")
                {
                    unprocessedCommands.Enqueue(line);
                }
            }
            while (unprocessedCommands.Count > 0)
            {
                //Console.WriteLine(unprocessedCommands.Count);
                Queue<string> originalUnprocessedCommands = new Queue<string>(unprocessedCommands);
                unprocessedCommands = new Queue<string>();
                foreach (string line in originalUnprocessedCommands)
                {
                    string[] parts = line.Split(" -> ");
                    string statement = parts[0];
                    string resultantVariable = parts[1];

                    string[] statementParts = statement.Split(" ");

                    if (statementParts.Length == 1) // Assignment
                    {
                        if (resultantVariable == "b")
                        {
                            continue;
                        }
                        int value;
                        try
                        {
                            value = Convert.ToInt32(statement);
                        }
                        catch (FormatException) // It was assigning a variable, not a value
                        {
                            if (!variables.ContainsKey(statement))
                            {
                                unprocessedCommands.Enqueue(line);
                                continue;
                            }
                            else
                            {
                                value = variables[statement];
                            }
                        }
                        variables[resultantVariable] = value;
                    }
                    else if (statementParts.Length == 2) // ~ (NOT)
                    {
                        string inputVariable = statementParts[1];
                        if (!variables.ContainsKey(inputVariable)) // Variable is not defined; come back later
                        {
                            unprocessedCommands.Enqueue(line);
                        }
                        else
                        {
                            variables[resultantVariable] = ~variables[inputVariable];
                        }
                    }
                    else if (statementParts.Length == 3)
                    {
                        string input1 = statementParts[0];
                        string operation = statementParts[1];
                        string input2 = statementParts[2];
                        switch (operation)
                        {
                            case "AND": // &
                                if (input1 == "1") // Additional possibility not listed in the puzzle text
                                {
                                    if (!variables.ContainsKey(input2))
                                    {
                                        unprocessedCommands.Enqueue(line);
                                    }
                                    else
                                    {
                                        variables[resultantVariable] = 1 & variables[input2];
                                    }
                                }
                                else
                                {
                                    // Both vars
                                    if (!(variables.ContainsKey(input1) && variables.ContainsKey(input2)))
                                    {
                                        unprocessedCommands.Enqueue(line);
                                    }
                                    else
                                    {
                                        variables[resultantVariable] = variables[input1] & variables[input2];
                                    }
                                }
                                break;
                            case "OR": // |
                                // Both vars
                                if (!(variables.ContainsKey(input1) && variables.ContainsKey(input2)))
                                {
                                    unprocessedCommands.Enqueue(line);
                                }
                                else
                                {
                                    variables[resultantVariable] = variables[input1] | variables[input2];
                                }
                                break;

                            case "LSHIFT": // <<
                                // input1 var, input2 int
                                if (!variables.ContainsKey(input1))
                                {
                                    unprocessedCommands.Enqueue(line);
                                }
                                else
                                {
                                    variables[resultantVariable] = variables[input1] << Convert.ToInt32(input2);
                                }
                                break;
                            case "RSHIFT": // >>
                                // input1 var, input2 int
                                if (!variables.ContainsKey(input1))
                                {
                                    unprocessedCommands.Enqueue(line);
                                }
                                else
                                {
                                    variables[resultantVariable] = variables[input1] >> Convert.ToInt32(input2);
                                }
                                break;
                            default:
                                throw new Exception(line);
                        }
                    }
                    else
                    {
                        throw new Exception(line);
                    }
                    if (variables.ContainsKey("a"))
                    {
                        return Convert.ToString(variables["a"]);
                    }
                }
            }
            throw new Exception("No a found");
        }
        static void Main()
        {
            string file = File.ReadAllText(@"../../../input.txt");
            Console.WriteLine(PartOne(file));
            Console.WriteLine(PartTwo(file));
        }
    }
}