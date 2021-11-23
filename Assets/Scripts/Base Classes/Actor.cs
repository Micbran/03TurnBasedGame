using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public event Action<string> ActorDied = delegate { };

    [SerializeField] ActorStats baseStats;
    [SerializeField] protected string actorName;
    [SerializeField] protected GameObject highlight;
    [SerializeField] RectTransform targetingIndicator;

    protected int health;
    public int Health => this.health;
    protected int maxHealth;
    public int MaxHealth => this.maxHealth;
    protected int defense;
    public int Defense => this.defense;
    protected int attack;
    public int Attack => this.attack;
    protected int damageBonus;
    public int Damage => this.damageBonus;
    protected int damageReduction;
    public int DamageReduction => this.damageReduction;
    protected int initiativeBonus;
    protected int movement;
    public int Movement => this.movement;

    public int Initiative => this.initiativeScore;
    protected int initiativeScore;

    public int ActionPoints => this.actionPoints;
    protected int actionPoints;

    public List<ActionStats> Actions => this.actions;
    protected List<ActionStats> actions = new List<ActionStats>();

    public string Name => this.actorName;

    private float distanceMovedForAction = 0f;
    public float DistanceMovedForAction => this.distanceMovedForAction;

    protected void Awake()
    {
        this.health = baseStats.Health;
        this.maxHealth = this.health;
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

    public void UpdateTargetingCircle(float newRange)
    {
        Vector3 scaleFactor = UIUtilities.ToMetersWorldspace(2000, newRange);
        this.targetingIndicator.localScale = scaleFactor;
    }

    public void DisableTargetingCircle()
    {
        this.targetingIndicator.localScale = Vector3.zero;
    }

    public bool CheckAction(int actionCost)
    {
        return actionCost <= this.actionPoints;
    }

    public void TakeAction(int actionCost)
    {
        this.actionPoints -= actionCost;
    }

    public AttackResult AttackActor(Actor otherActor)
    {
        int rollResult = GlobalRandom.AttackRoll();
        int damageRoll = GlobalRandom.RollDie(new Dice("1d8"));
        int finalDamage = 0;
        if (rollResult + this.Attack >= otherActor.Defense)
        {
            finalDamage = otherActor.TakeDamage(this.Damage + damageRoll);
        }

        return new AttackResult()
        {
            resultSource = this.Name,
            resultTarget = otherActor.Name,
            defense = otherActor.Defense,
            resultAttack = "1d8",
            damageBonus = this.Damage,
            damageReduction = otherActor.DamageReduction,
            damageRoll = damageRoll,
            finalDamage = finalDamage,
            attackRollTotal = rollResult + this.Attack
        };
    }

    public int TakeDamage(int damage)
    {
        int damageToTake = Mathf.Clamp(damage - this.DamageReduction, 0, 9999);
        this.health -= damageToTake;
        // play feedback
        return damageToTake;
    }

    public void CheckIfDead()
    {
        if (this.Health <= 0)
        {
            Debug.Log($"{this.actorName} has died!");
            this.KillActor();
        }
    }

    protected virtual void KillActor()
    {
        this.ActorDied?.Invoke(this.Name);
        // play feedback
        Destroy(this.gameObject);
    }

    public override string ToString()
    {
        return $"{this.Name} - {this.Initiative}";
    }
}
