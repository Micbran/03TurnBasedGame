
public class AttackResult : Result
{
    public string resultAttack; // TODO
    public int attackRollTotal;
    public int defense;
    public int damageRoll;
    public int damageBonus;
    public string resultSource;
    public string resultTarget;

    public override string ToResultString()
    {
        string attackResult = attackRollTotal < defense ? "misses" : $"deals {damageRoll + damageBonus} ({resultAttack} + {damageBonus}) damage";
        return $"{this.resultSource} attacks {this.resultTarget} ({this.attackRollTotal} vs. {this.defense}) and {attackResult}.";
    }
}
