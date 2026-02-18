namespace Week1Assignments;

public class Interception
{
    static void Main(string[] args)
    {
        string values = Console.ReadLine();
        if (values.Contains('9'))
        {
            Console.WriteLine("F");
            return;
        }
        else
        {
            Console.WriteLine("S");
            return;
        }
    }
}