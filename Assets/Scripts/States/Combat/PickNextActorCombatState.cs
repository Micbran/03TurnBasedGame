using System.Collections;
using UnityEngine;

public class PickNextActorCombatState : CombatState
{
    [SerializeField] private AudioClip EnemyTurnSoundEff;
    [SerializeField] private AudioClip PlayerTurnSoundEff;
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
            if (this.StateMachine.CheckWinLoseCondition()) return;
            if (this.StateMachine.CurrentActor is PlayerCharacter)
            {
                AudioHelper.PlayClip2D(this.PlayerTurnSoundEff, 1f);
                StateMachine.ChangeState<PlayerCharacterTurnCombatState>();
            }
            else if (this.StateMachine.CurrentActor is EnemyCharacter)
            {
                AudioHelper.PlayClip2D(this.EnemyTurnSoundEff, 1f);
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
