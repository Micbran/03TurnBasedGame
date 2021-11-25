using System;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalRandom
{
    private static System.Random random = new System.Random();

    public static int RandomInt(int max, int min = 1)
    {
        return random.Next(min, max + 1);
    }

    public static int RollDie(Dice die)
    {
        int sum = 0;
        for (int i = 0; i < die.Number; i++)
        {
            sum += RandomInt(die.Sides);
        }
        return sum;
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
        return random.Next(1, 21);
    }

    public static T RandomFromList<T>(List<T> list) 
    {
        return list[random.Next(0, list.Count)];
    }
}
