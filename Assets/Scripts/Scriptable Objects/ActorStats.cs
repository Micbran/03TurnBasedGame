using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats.asset", menuName = "ActorStats")]
public class ActorStats : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int defense;
    [SerializeField] private int attack;
    [SerializeField] private int damageBonus;
    [SerializeField] private int damageReduction;
    [SerializeField] private int initiativeBonus;
    [SerializeField] private int movement;

    public int Health => this.health;
    public int Defense => this.defense;
    public int Attack => this.attack;
    public int DamageBonus => this.damageBonus;
    public int DamageReduction => this.damageReduction;
    public int InitiativeBonus => this.initiativeBonus;
    public int Movement => this.movement;
}
