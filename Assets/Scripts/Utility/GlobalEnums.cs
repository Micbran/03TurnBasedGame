using UnityEngine;

// Overly complex, but I decided to enumerate all possible "targeting" types that might be used
// In reality, the game only uses Movement and TargetSingleEnemy
public enum ActionType
{
    Movement = 0,
    //MovementInstant,

    //TargetSingleIndiscriminate,
    TargetSingleEnemy,
    //TargetSingleAlly,

    //TargetAreaCircleIndiscriminate,
    //TargetAreaCircleEnemy,
    //TargetAreaCircleAlly,

    //TargetAreaConeIndiscriminate,
    //TargetAreaConeEnemy,
    //TargetAreaConeAlly,

    //TargetSingleCircleIndiscriminate,
    //TargetSingleCircleEnemy,
    //TargetSingleCircleAlly,

    //PointBlankNova,
}