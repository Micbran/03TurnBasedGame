using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveToPoint))]
public abstract class Actor : MonoBehaviour
{
    public event Action<string> ActorDied = delegate { };

    public bool isDead = false;

    protected MoveToPoint movementHandler;
    public MoveToPoint MovementHandler => this.movementHandler;
    [SerializeField] ActorStats baseStats;
    [SerializeField] protected string actorName;
    [SerializeField] protected GameObject highlight;
    [SerializeField] RectTransform targetingIndicator;
    [SerializeField] ActorHealthbar healthbar;

    [Space(10)]
    [SerializeField] private AudioClip HitSoundEff;
    [SerializeField] private AudioClip MissSoundEff;
    [SerializeField] private AudioClip DeathSoundEff;

    [Space(10)]
    [SerializeField] private ParticleSystem OnHitParticles;

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

        this.movementHandler = this.GetComponent<MoveToPoint>();
        this.healthbar.InitializeHealthbar(this.maxHealth);
    }

    public int RollInitiative()
    {
        this.initiativeScore = GlobalRandom.AttackRoll() + this.initiativeBonus;
        return this.initiativeScore;
    }

    public void StartNewTurn()
    {
        if (this.isDead) return;
        this.actionPoints = 3;
        this.highlight.SetActive(true);
    }

    public void ActorEndTurn()
    {
        this.highlight.SetActive(false);
    }

    public void UpdateTargetingCircle(float newRange)
    {
        if (this.isDead) return;
        Vector3 scaleFactor = UIUtilities.ToMetersWorldspace(500, newRange);
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
        else
        {
            AudioHelper.PlayClip2D(this.MissSoundEff, 1.6f);
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
        this.healthbar.UpdateHealthbar(this.health);
        AudioHelper.PlayClip2D(this.HitSoundEff, 1.0f);
        Instantiate(this.OnHitParticles, this.transform.position, Quaternion.identity);
        return damageToTake;
    }

    public void CheckIfDead()
    {
        if (this.Health <= 0)
        {
            this.KillActor();
        }
    }

    protected virtual void KillActor()
    {
        AudioHelper.PlayClip2D(this.DeathSoundEff, 1.0f);
        this.isDead = true;
        Destroy(this.gameObject);
        this.ActorDied?.Invoke(this.Name);
    }

    public override string ToString()
    {
        return $"{this.Name} - {this.Initiative}";
    }
}
