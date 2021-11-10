using System.Collections;
using UnityEngine;

public class PickNextActorCombatState : CombatState
{
    bool finishPicking = false;

    public override void OnEnter()
    {
        this.finishPicking = false;
    }

    public override void Tick()
    {
        if (!this.finishPicking)
        {
            this.finishPicking = true;
            this.StateMachine.CurrentActor = this.StateMachine.Turn.Peek();
            Debug.Log($"I choose you, {StateMachine.CurrentActor.Name}!");
            if (this.StateMachine.CurrentActor is PlayerCharacter)
            {
                StateMachine.ChangeState<PlayerCharacterTurnCombatState>();
            }
            else if (this.StateMachine.CurrentActor is EnemyCharacter)
            {
                StateMachine.ChangeState<EnemyCharacterTurnCombatState>();
            }
            else
            {
                Debug.Log("WTF???");
            }
        }
    }

    public override void OnExit()
    {
        this.finishPicking = false;
    }
}
