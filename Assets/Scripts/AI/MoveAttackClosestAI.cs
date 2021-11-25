using System.Collections.Generic;
using UnityEngine;

public enum AIAction
{
    Undecided = 0,
    Movement = 1,
    Attack = 2,
    Done = 3,
}

public class MoveAttackClosestAI : ArtificialIntelligence
{
    [SerializeField] private EnemyCharacter self;
    [SerializeField] private float actionTimer = 0.2f;
    private float actionTimerSub = 0f;

    private PlayerCharacter target = null;
    private AIAction currentActionType = AIAction.Undecided;

    private List<ActionStats> movementTypeActions = new List<ActionStats>();
    private List<ActionStats> attackTypeActions = new List<ActionStats>();

    public override void Act()
    {
        movementTypeActions.Clear();
        attackTypeActions.Clear();
        this.actionTimerSub = -1f;

        this.turnFinished = false;
        this.currentActionType = AIAction.Undecided;

        this.PopulateActionLists();

        this.target = FindClosestTarget();

        this.DetermineNextMove();
    }

    public override Result IntelligenceTick()
    {
        this.actionTimerSub -= Time.deltaTime;
        if (this.actionTimerSub > 0) return null;
        if (this.self.MovementHandler.IsMoving) return null;
        Result potentialResult = null;
        switch (this.currentActionType)
        {
            case AIAction.Undecided:
                this.DetermineNextMove();
                break;
            case AIAction.Movement:
                this.HandleMovementType();
                break;
            case AIAction.Attack:
                potentialResult = this.HandleAttackType();
                break;
            case AIAction.Done:
                this.turnFinished = true;
                break;
        }

        if (this.target == null)
        {
            this.currentActionType = AIAction.Done;
        }

        return potentialResult;
    }

    private PlayerCharacter FindClosestTarget()
    {
        List<PlayerCharacter> players = new List<PlayerCharacter>(FindObjectsOfType<PlayerCharacter>());
        if (players.Count == 0) return null;
        PlayerCharacter closestPlayer = null;
        foreach (PlayerCharacter player in players) // this could be done with a single linq query, but I hate writing .Sorts
        {
            if (closestPlayer == null)
            {
                closestPlayer = player;
                continue;
            }

            closestPlayer = this.CloserDistance(closestPlayer, player);
        }
        return closestPlayer;
    }

    // I could like, theoretically implement an IComparer for PlayerCharacter or something, but... no.
    private PlayerCharacter CloserDistance(PlayerCharacter a, PlayerCharacter b)
    {
        return
            Vector3.Distance(this.transform.position, a.transform.position) < Vector3.Distance(this.transform.position, b.transform.position)
            ? a : b;
    }

    private void PopulateActionLists()
    {
        foreach (ActionStats action in this.self.Actions)
        {
            switch (action.ActionType)
            {
                case ActionType.Movement:
                    this.movementTypeActions.Add(action);
                    break;
                case ActionType.TargetSingleEnemy:
                    this.attackTypeActions.Add(action);
                    break;
            }
        }
    }

    private void DetermineNextMove()
    {
        if (this.self.ActionPoints == 0)
        {
            this.currentActionType = AIAction.Done;
            return;
        }

        if (this.target == null || this.target.Health <= 0)
        {
            this.target = this.FindClosestTarget();
            return;
        }

        if (Vector3.Distance(this.transform.position, this.target.transform.position) < 1.5)
        {
            this.currentActionType = AIAction.Attack;
        }
        else
        {
            this.currentActionType = AIAction.Movement;
        }
    }

    private void HandleMovementType()
    {
        ActionStats chosenMovementAction = GlobalRandom.RandomFromList<ActionStats>(this.movementTypeActions);
        this.self.MovementHandler.StartNewMoveAction(chosenMovementAction.ActionRange);
        this.self.MovementHandler.SetNewDestination(this.GetPointOnGroundWithinRangeOfPosition(1.2f, this.transform.position, this.target.transform.position));
        this.self.TakeAction(chosenMovementAction.ActionPointCost);
        this.FinishAction();
    }

    private Result HandleAttackType()
    {
        ActionStats chosenAttackAction = GlobalRandom.RandomFromList<ActionStats>(this.attackTypeActions);
        this.self.TakeAction(chosenAttackAction.ActionPointCost);
        AttackResult attackResult = this.self.AttackActor(this.target);
        this.target.CheckIfDead();
        this.FinishAction();
        return attackResult;
    }

    private Vector3 GetPointOnGroundWithinRangeOfPosition(float distanceAway, Vector3 startingPoint, Vector3 targetPoint)
    {
        Vector3 directionStartTarget = Vector3.Normalize(startingPoint - targetPoint);
        Vector3 finalPosition = targetPoint + directionStartTarget * distanceAway;
        return finalPosition;
    }

    private void FinishAction()
    {
        this.actionTimerSub = this.actionTimer;
        this.currentActionType = AIAction.Undecided;
    }

}
