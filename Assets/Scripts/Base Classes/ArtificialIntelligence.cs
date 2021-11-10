using UnityEngine;

public abstract class ArtificialIntelligence : MonoBehaviour
{
    protected bool turnFinished;
    public bool TurnFinished => this.turnFinished;

    public abstract void Act();
}
