using UnityEngine;

[CreateAssetMenu(fileName = "ActionStats.asset", menuName = "ActionStats")]
public class ActionStats : ScriptableObject
{
    [SerializeField] private Sprite iconImage;
    [SerializeField] private int apCost;
    [SerializeField] private int range;
    [SerializeField] private string description;
    [SerializeField] private ActionType actionType;

    public Sprite IconImage => this.iconImage;
    public int ActionPointCost => this.apCost;
    public int ActionRange => this.range;
    public string Description => this.description;
    public ActionType ActionType => this.actionType;
}
