using UnityEngine;
using System;
using System.Collections;

public class EnemyCharacterTurnCombatState : CombatState
{
    public event Action EnemyTurnBegan = delegate { };
    public event Action EnemyTurnEnded = delegate { };

    private ArtificialIntelligence currentAI = null;

    public override void OnEnter()
    {
        this.EnemyTurnBegan?.Invoke();
        if (this.StateMachine.CurrentActor is EnemyCharacter)
        {
            this.currentAI = this.StateMachine.CurrentActor.GetComponent<ArtificialIntelligence>();
            if (this.currentAI == null)
            {
                this.EnemyEndTurn();
            }
            this.StateMachine.CurrentActor.StartNewTurn();
            this.StateMachine.TriggerNewTurn();
            this.currentAI.Act();
        }
    }

    public override void Tick()
    {
        if (this.currentAI.TurnFinished)
        {
            this.EnemyEndTurn();
        }
    }

    public override void OnExit()
    {
        this.StateMachine.Turn.EndTurn();
        this.currentAI = null;
    }

    private void EnemyEndTurn()
    {
        EnemyTurnEnded?.Invoke();
        this.StateMachine.CurrentActor.ActorEndTurn();
        this.StateMachine.ChangeState<PickNextActorCombatState>();
    }
}
