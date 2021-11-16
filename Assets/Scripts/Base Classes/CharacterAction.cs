using UnityEngine;

public class CharacterAction
{
    public Sprite IconImage { get;  private set; }
    public int ActionPointCost { get; private set; }
    public int ActionRange { get; private set; }

    public CharacterAction(ActionStats baseActionStats, int apCost = -1, int range = -1)
    {
        this.IconImage = baseActionStats.IconImage;
        this.ActionPointCost = baseActionStats.ActionPointCost;
        this.ActionRange = baseActionStats.ActionRange;
        if (apCost != -1)
        {
            this.ActionPointCost = apCost;
        }
        if (range != -1)
        {
            this.ActionRange = range;
        }
    }
}
