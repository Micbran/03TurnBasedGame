using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public event Action NewRoundStarted = delegate { };

    private List<Actor> actedEntities = new List<Actor>();
    private InitiativeQueue actingEntities = new InitiativeQueue();

    private void Start()
    {
        Actor[] interimList = FindObjectsOfType<Actor>();
        foreach (Actor a in interimList)
        {
            a.RollInitiative();
            this.actingEntities.Enqueue(a);
        }
    }

    public void StartNewRound()
    {
        foreach (Actor a in actedEntities)
        {
            this.actingEntities.Enqueue(a);
        }
        this.actedEntities.Clear();
    }

    public Actor Peek()
    {
        Debug.Log(this.actingEntities.ToString());
        return this.actingEntities.Peek();
    }

    public void EndTurn()
    {
        Actor deq = this.actingEntities.Dequeue();
        this.actedEntities.Add(deq);
        if (this.actingEntities.Count == 0)
        {
            this.NewRoundStarted?.Invoke();
            this.StartNewRound();
        }
    }
}
