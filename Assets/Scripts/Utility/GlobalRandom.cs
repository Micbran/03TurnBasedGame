using System;
using UnityEngine;

public static class GlobalRandom
{
    private static System.Random random = new System.Random();

    public static int RandomInt(int max, int min = 1)
    {
        return random.Next(min, max);
    }

    public static int RandomOddInt(int max)
    {
        int randOdd = random.Next(1, max);
        if (randOdd % 2 == 0)
        {
            randOdd -= 1;
        }
        return randOdd;
    }

    public static int RandomOddInt(int min, int max)
    {
        int randOdd = random.Next(min, max);
        if (randOdd % 2 == 0)
        {
            randOdd -= 1;
        }
        if (randOdd < min)
        {
            randOdd += 2;
        }
        return randOdd;
    }

    public static int AttackRoll()
    {
        return random.Next(1, 20);
    }
}
