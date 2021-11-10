using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActorAttack
{
    private List<Dice> diceExpression;

    public ActorAttack(string diceExpressionString)
    {
        this.diceExpression = new List<Dice>();
        if (diceExpressionString.Length == 0) return;

        string[] dice = diceExpressionString.Split('+').Select(d => d?.Trim()).ToArray();
        foreach (string die in dice)
        {
            this.diceExpression.Add(new Dice(die));
        }
    }

    public int RollAttack()
    {
        int damageSum = 0;
        foreach (Dice dice in diceExpression)
        {
            damageSum += dice.RollDice();
        }
        return damageSum;
    }

    public override string ToString()
    {
        string sum = "";
        foreach (Dice dice in diceExpression)
        {
            sum += dice.ToString() + " + ";
        }
        if (sum.Length < 3) return sum;
        return sum.Substring(0, sum.Length - 3);
    }
}
