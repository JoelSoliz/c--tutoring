using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// moments:
// actual direction, stop time, queue of people waiting
public class Escalator
{
    public static void findLastTimeInEscalator()
    {
        string numberOfPeople = Console.ReadLine();
        int N = int.Parse(numberOfPeople);

        int[] times = new int[N];
        int[] directions = new int[N];

        for (int i = 0; i < N; i++)
        {
            string[] data = Console.ReadLine().Split();
            times[i] = int.Parse(data[0]);
            directions[i] = int.Parse(data[1]);
        }

        int currentDirection = -1;  // -1 = stopped, 0 or 1 = moving
        int stopTime = 0;
        Queue<int> peopleWaiting = new Queue<int>();

        for (int i = 0; i < N; i++)
        {
            int arrivalTime = times[i];
            int wantedDirection = directions[i];

            while (peopleWaiting.Count > 0 && arrivalTime >= stopTime)
            {
                int waitingPerson = peopleWaiting.Dequeue();
                int waitingPersonDirection = directions[waitingPerson];

                currentDirection = waitingPersonDirection;
                stopTime = stopTime + 10;
            }

            if (arrivalTime >= stopTime)
            {
                currentDirection = wantedDirection;
                stopTime = arrivalTime + 10;
            }
            else
            {
                if (currentDirection == wantedDirection)
                {
                    int exitTime = arrivalTime + 10;
                    if (exitTime > stopTime)
                    {
                        stopTime = exitTime;
                    }
                }
                else
                {
                    peopleWaiting.Enqueue(i);
                }
            }
        }

        // Process remaining people
        while (peopleWaiting.Count > 0)
        {
            int waitingPersonIndex = peopleWaiting.Dequeue();
            int waitingPersonDirection = directions[waitingPersonIndex];

            currentDirection = waitingPersonDirection;
            stopTime = stopTime + 10;
        }

        Console.WriteLine(stopTime);
    }
}