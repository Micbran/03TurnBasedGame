using UnityEngine;
using UnityEngine.UI;

public class ActorDisplayManager : MonoBehaviour
{
    [SerializeField] private Text nameField;
    [SerializeField] private Text healthField;
    [SerializeField] private Text defenseField;
    [SerializeField] private Text attackField;
    [SerializeField] private Text damageField;
    [SerializeField] private Text damageReductionField;
    [SerializeField] private Text movementField;

    public void UpdateActorDisplay(Actor newActor)
    {
        this.nameField.text = newActor.Name;
        this.healthField.text = $"{newActor.Health}/{newActor.MaxHealth}";
        this.defenseField.text = $"{newActor.Defense}";
        this.attackField.text = $"{newActor.Attack}";
        this.damageField.text = $"{newActor.Damage}";
        this.damageReductionField.text = $"{newActor.DamageReduction}";
        this.movementField.text = $"{newActor.Movement}";
    }
}
