using System.Collections;
using UnityEngine;

public class SetupCombatGameState : CombatState
{
    // probably want references to all actor entites, but those can be easily obtained with a find all

    bool finishSetup = false;

    public override void OnEnter()
    {
        this.finishSetup = false;
    }

    public override void Tick()
    {
        if (!this.finishSetup)
        {
            this.finishSetup = true;
            StartCoroutine(SetupRoutine());
        }
    }

    IEnumerator SetupRoutine()
    {
        yield return new WaitForSeconds(1f);

        this.SetupGame();
    }

    private void SetupGame()
    {
        StateMachine.ChangeState<PickNextActorCombatState>();
    }

    public override void OnExit()
    {
        this.finishSetup = false;
    }
}
