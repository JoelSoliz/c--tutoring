#region JoaoJoao
//int JoaoJoao(int[] difficultyLevels)
//{
//    //bool level1 = false;
//    //bool level2 = false;
//    //bool level3 = false;
//    HashSet<int> validatedDifficulties = new HashSet<int>() { 1,2,3,4 };
//    //bool level4 = false;
//    for (int i = 0; i < difficultyLevels.Length; i++)
//    {
//        if (validatedDifficulties.Contains(difficultyLevels[i]))
//        {
//            validatedDifficulties.Remove(difficultyLevels[i]);
//        }
//        //    if (difficultyLevels[i] == 1)
//        //    {
//        //        level1 = true;
//        //    }

//        //    if (difficultyLevels[i] == 2)
//        //    {
//        //        level2 = true;
//        //    }

//        //    if (difficultyLevels[i] == 3)
//        //    {
//        //        level3 = true;
//        //    }

//        //    if (difficultyLevels[i] == 4)
//        //    {
//        //        level4 = true;
//        //    }
//    }

//    //HashSet<int> validatedDifficulties = new HashSet<int>(difficultyLevels);

//    //return 4 - (level1 ? 1 : 0) - (level2 ? 1 : 0) - (level3 ? 1 : 0) - (level4 ? 1 : 0);
//    //return 4 - validatedDifficulties.Count;
//    return validatedDifficulties.Count;
//}


//int[] difficultyLevels1 = new int[] { 1, 3, 4, 1, 3, 4, 1, 3, 4, 1 };
//Console.WriteLine(JoaoJoao(difficultyLevels1));

//int[] difficultyLevels2 = new int[] { 4, 1, 1, 4, 3, 1, 2, 1, 2, 2 };
//Console.WriteLine(JoaoJoao(difficultyLevels2));
#endregion

#region AttentionMeeting
//int AttentionToTheMeeting(int n, int k)
//{
//    int speachInterval = 1;
//    int intervals = n - 1;
//    int remainingTime = k - intervals * speachInterval;
//    return remainingTime / n;
//}


//int N = 7; // N Directors
//int K = 120; // Max Meeting Duration
//Console.WriteLine(AttentionToTheMeeting(N, K));

//N = 1; // N Directors
//K = 10; // Max Meeting Duration
//Console.WriteLine(AttentionToTheMeeting(N, K));

//N = 100; // N Directors
//K = 1000; // Max Meeting Duration
//Console.WriteLine(AttentionToTheMeeting(N, K));
#endregion

#region AppendPanic
int AppendAndPanic(string input)
{
    //HashSet<char> letters = new HashSet<char>(input);
    //// |input| = |original| + |unicos|
    //// |original| = |input| - |unicos|
    //return input.Length - letters.Count;
    HashSet<char> letters = new HashSet<char>();
    for(int i = input.Length - 1; i >= input.Length / 2 - 1; i--)
    {
        if (!letters.Contains(input[i]))
        {
            letters.Add(input[i]);
        }
        else
        {
            return i + 1;
        }
    }

    return 0;
}


string S = "ICPCCIP"; // input
Console.WriteLine(AppendAndPanic(S));
string S2 = "ABEDCCCABCDE";
Console.WriteLine(AppendAndPanic(S2));
string S3 = "ZZ";
Console.WriteLine(AppendAndPanic(S3));
#endregion