using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterTurnCombatState : CombatState
{
    private List<ActionButton> currentActionSet = new List<ActionButton>();
    private ActionStats currentActionBase = null;
    private float currentRange = 0f;
    private bool actionConfirmed = false;

    public override void OnEnter()
    {
        StateMachine.Input.PressedConfirm += this.OnPressedConfirm;
        StateMachine.Input.ClickedLocation += this.OnClickedLocation;
        StateMachine.UIActionsUpdated += this.OnUIActionsUpdated;

        this.StateMachine.CurrentActor.StartNewTurn();
        this.StateMachine.TriggerNewTurn();

    }

    public override void OnExit()
    {
        this.UnsubscribeFromActions();
        StateMachine.Input.PressedConfirm -= this.OnPressedConfirm;
        StateMachine.Input.ClickedLocation -= this.OnClickedLocation;
        StateMachine.UIActionsUpdated -= this.OnUIActionsUpdated;
    }

    public override void Tick()
    {
        this.StateMachine.CurrentActor.UpdateTargetingCircle(this.currentRange);
    }

    private void UnsubscribeFromActions()
    {
        foreach (ActionButton button in this.currentActionSet)
        {
            button.OnActionButtonPressed -= OnActionButtonPressed;
        }
    }

    private void OnPressedConfirm()
    {
        this.TransitionToNextTurn();
    }

    private void OnUIActionsUpdated(List<ActionButton> actionButtons)
    {
        this.currentActionSet = actionButtons;
        foreach (ActionButton button in actionButtons)
        {
            button.OnActionButtonPressed += OnActionButtonPressed;
        }
    }

    private void OnActionButtonPressed(ActionStats action)
    {
        if (this.StateMachine.CurrentActor.CheckAction(action.ActionPointCost))
        {
            this.currentActionBase = action;
            this.currentRange = this.currentActionBase.ActionRange;
            this.actionConfirmed = false;
            this.StateMachine.CurrentActor.UpdateTargetingCircle(this.currentRange);
        }
        else
        {
            // display to user that they don't have enough AP
        }

    }

    private void ActionFinishedOrCanceled()
    {
        this.currentActionBase = null;
        this.currentRange = 0;
        this.actionConfirmed = false;
    }

    private void OnClickedLocation(Ray clickRay)
    {
        if (currentActionBase == null) return;
        switch (this.currentActionBase.ActionType)
        {
            case ActionType.Movement:
                this.HandleMovementClick(clickRay);
                break;
            case ActionType.TargetSingleEnemy:
                this.HandleAttackClick(clickRay);
                break;
        }
    }

    private void HandleMovementClick(Ray moveRay)
    {
        if (!this.actionConfirmed)
        {
            this.actionConfirmed = true;
            this.StateMachine.CurrentActor.TakeAction(this.currentActionBase.ActionPointCost);
        }

    }

    private void HandleAttackClick(Ray attackRay)
    {
        RaycastHit hit;
        Debug.DrawLine(attackRay.origin, attackRay.direction * 1000, Color.red, 10f);
        if(Physics.Raycast(attackRay, out hit, 10000f, this.StateMachine.ActorLayer.value))
        {
            EnemyCharacter hitCharacter = hit.collider.gameObject.GetComponent<EnemyCharacter>();

            if (hitCharacter == null) return; // hit nobody, we exit

            if (IsWithinRange(hitCharacter))
            {
                this.actionConfirmed = true;
                this.StateMachine.CurrentActor.TakeAction(this.currentActionBase.ActionPointCost);
                this.StateMachine.Log.AddNewResult(this.StateMachine.CurrentActor.AttackActor(hitCharacter));
                hitCharacter.CheckIfDead();
                this.StateMachine.TriggerActorUIUpdate();
                this.ActionFinishedOrCanceled();
            }
        }
    }

    private bool IsWithinRange(Actor otherActor)
    {
        return Vector3.Distance(this.StateMachine.CurrentActor.transform.position, otherActor.transform.position) <= this.currentRange + GlobalConstants.DistanceObjectSizeCompensation;
    }

    public void TransitionToNextTurn()
    {
        this.StateMachine.Turn.EndTurn();
        this.ActionFinishedOrCanceled();
        this.StateMachine.CurrentActor.ActorEndTurn();
        this.StateMachine.ChangeState<PickNextActorCombatState>();
    }
}
