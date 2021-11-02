using UnityEngine;
using System;
using System.Collections;

public class EnemyCharacterTurnCombatState : CombatState
{
    public event Action EnemyTurnBegan = delegate { };
    public event Action EnemyTurnEnded = delegate { };

    [SerializeField] float thinkingDuration = 0.5f;

    public override void OnEnter()
    {
        Debug.Log("Entering EnemyCharacterTurnCombatState.");
        this.EnemyTurnBegan?.Invoke();
        StartCoroutine(this.EnemyThinkingRoutine(this.thinkingDuration));
    }

    public override void OnExit()
    {
        Debug.Log("Exiting EnemyCharacterTurnCombatState.");
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking. . . . .");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action!");
        this.EnemyEndTurn();
    }

    private void EnemyEndTurn()
    {
        EnemyTurnEnded?.Invoke();
        this.StateMachine.ChangeState<PickNextActorCombatState>();
    }
}
