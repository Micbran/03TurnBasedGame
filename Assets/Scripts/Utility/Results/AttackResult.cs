
using UnityEngine;

public class AttackResult : Result
{
    public string resultAttack; // TODO
    public int attackRollTotal;
    public int defense;
    public int damageRoll;
    public int damageBonus;
    public int damageReduction;
    public int finalDamage;
    public string resultSource;
    public string resultTarget;

    public override string ToResultString()
    {
        string attackResult = attackRollTotal < defense ? "misses" : $"deals {finalDamage} ({resultAttack} + {damageBonus}) damage (reduced by {Mathf.Clamp(damageRoll + damageBonus - finalDamage, 0, damageReduction)})"; 
        return $"{this.resultSource} attacks {this.resultTarget} ({this.attackRollTotal} vs. {this.defense}) and {attackResult}.";
    }
}
