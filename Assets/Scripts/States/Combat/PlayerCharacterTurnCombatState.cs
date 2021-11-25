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
        if (this.currentActionBase?.ActionType == ActionType.Movement)
        {
            this.currentRange = this.currentActionBase.ActionRange - this.StateMachine.CurrentActor.MovementHandler.CurrentMovement;
        }
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
            if (action.ActionType == ActionType.Movement)
            {
                this.StateMachine.CurrentActor.MovementHandler.StartNewMoveAction(action.ActionRange);
            }
        }
        else
        {
            this.StateMachine.UI.DisplayErrorMessage("You don't have enough AP for that!");
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
        RaycastHit moveHit;
        if (Physics.Raycast(moveRay, out moveHit, 10000f, this.StateMachine.CollisionLayer.value))
        {
            this.StateMachine.CurrentActor.MovementHandler.SetNewDestination(moveHit.point);
            if (!this.actionConfirmed)
            {
                this.actionConfirmed = true;
                this.StateMachine.CurrentActor.TakeAction(this.currentActionBase.ActionPointCost);
                this.StateMachine.TriggerActorUIUpdate();
            }
        }
    }

    private void HandleAttackClick(Ray attackRay)
    {
        RaycastHit hit;
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
        this.StateMachine.CurrentActor.UpdateTargetingCircle(0f);
        this.StateMachine.CurrentActor.ActorEndTurn();
        this.StateMachine.ChangeState<PickNextActorCombatState>();
    }
}
