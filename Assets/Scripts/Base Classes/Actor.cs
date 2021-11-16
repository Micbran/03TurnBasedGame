using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField] ActorStats baseStats;
    [SerializeField] protected string actorName;
    [SerializeField] protected GameObject highlight;

    protected int health;
    protected int defense;
    protected int attack;
    protected int damageBonus;
    protected int damageReduction;
    protected int initiativeBonus;
    protected int movement;

    public int Initiative => this.initiativeScore;
    protected int initiativeScore;

    public int ActionPoints => this.actionPoints;
    protected int actionPoints;

    public List<ActionStats> Actions => this.actions;
    protected List<ActionStats> actions = new List<ActionStats>();

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
        this.actionPoints = 0;
        this.actions = this.baseStats.Actions;
    }

    public int RollInitiative()
    {
        this.initiativeScore = GlobalRandom.AttackRoll() + this.initiativeBonus;
        return this.initiativeScore;
    }

    public void StartNewTurn()
    {
        this.actionPoints = 3;
        this.highlight.SetActive(true);
    }

    public void ActorEndTurn()
    {
        this.highlight.SetActive(false);
    }

    public bool CheckAction(int actionCost)
    {
        return actionCost <= this.actionPoints;
    }

    public void TakeAction(int actionCost)
    {
        this.actionPoints -= actionCost;
    }

    public override string ToString()
    {
        return $"{this.Name} - {this.Initiative}";
    }
}
