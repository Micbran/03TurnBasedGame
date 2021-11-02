using System.Collections;
using UnityEngine;

public class PickNextActorCombatState : CombatState
{
    // probably has access to turn manager
    // determines next transition Player or Enemy

    bool finishPicking = false;

    public override void OnEnter()
    {
        this.finishPicking = false;
        Debug.Log("Entering PickNextActorCombatState.");
    }

    public override void Tick()
    {
        if (!this.finishPicking)
        {
            this.finishPicking = true;
            StartCoroutine(PickingRoutine());
        }
    }

    IEnumerator PickingRoutine()
    {
        Debug.Log("Picking next turn. . . . .");
        yield return new WaitForSeconds(1f);

        Debug.Log("Turn picked!");
        this.PickNextTurn();
    }

    private void PickNextTurn()
    {
        int randChoice = Random.Range(0, 2); // TODO scrap this later for something proper
        if (randChoice == 0)
        {
            StateMachine.ChangeState<PlayerCharacterTurnCombatState>();
        }
        else
        {
            StateMachine.ChangeState<EnemyCharacterTurnCombatState>();
        }
    }

    public override void OnExit()
    {
        this.finishPicking = false;
        Debug.Log("Exiting PickNextActorCombatState.");
    }
}
