using System.Collections;
using UnityEngine;

public class SetupCombatGameState : CombatState
{
    // probably want references to all actor entites, but those can be easily obtained with a find all

    bool finishSetup = false;

    public override void OnEnter()
    {
        this.finishSetup = false;
        Debug.Log("Entering SetupCombatGameState.");
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
        Debug.Log("Setting up game. . . . .");
        yield return new WaitForSeconds(1f);

        Debug.Log("Game setup!");
        this.SetupGame();
    }

    private void SetupGame()
    {
        StateMachine.ChangeState<PickNextActorCombatState>();
    }

    public override void OnExit()
    {
        this.finishSetup = false;
        Debug.Log("Exiting SetupCombatGameState");
    }
}
