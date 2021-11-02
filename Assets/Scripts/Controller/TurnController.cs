using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private List<Actor> actedEntities = new List<Actor>();
    private InitiativeQueue actingEntities = new InitiativeQueue();

    private void Start()
    {
        Actor[] interimList = FindObjectsOfType<Actor>();
        foreach (Actor a in interimList)
        {
            this.actingEntities.Enqueue(a);
        }
    }

    public void StartNewRound()
    {
        foreach (Actor a in actedEntities)
        {
            this.actingEntities.Enqueue(a);
        }
    }
}
