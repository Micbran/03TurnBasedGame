using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonManager : MonoBehaviour
{
    [SerializeField] private Button endTurn = null;
    [SerializeField] private Button centerOnCharacter = null;

    public void UpdateButtons(Actor currentActor)
    {
        bool isPlayer = currentActor is PlayerCharacter;

        this.endTurn.interactable = isPlayer;
    }
}
