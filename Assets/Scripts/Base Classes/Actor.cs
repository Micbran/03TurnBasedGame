using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField] ActorStats baseStats;
    [SerializeField] protected string actorName;

    protected int health;
    protected int defense;
    protected int attack;
    protected int damageBonus;
    protected int damageReduction;
    protected int initiativeBonus;
    protected int movement;

    public int Initiative => this.initiativeScore;
    protected int initiativeScore;

    public string Name => this.actorName;

    protected void Awake()
    {
        this.health = baseStats.Health;
        this.defense = baseStats.Defense;
        this.attack = baseStats.Attack;
        this.damageBonus = baseStats.DamageBonus;
        this.damageReduction = baseStats.DamageReduction;
        this.initiativeBonus = baseStats.InitiativeBonus;
        this.movement = baseStats.Movement;
    }

    public int RollInitiative()
    {
        this.initiativeScore = GlobalRandom.AttackRoll() + this.initiativeBonus;
        return this.initiativeScore;
    }

    public override string ToString()
    {
        return $"{this.Name} - {this.Initiative}";
    }
}
