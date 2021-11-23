using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public event Action<ActionStats> OnActionButtonPressed = delegate { };
    private HoverDisplayTooltip tooltipManager = null;
    private Button buttonBase = null;
    private ActionStats representedAction = null;
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        this.buttonBase = this.GetComponent<Button>();
        this.tooltipManager = this.GetComponent<HoverDisplayTooltip>();
    }

    public void UpdateAbility(ActionStats action, bool isPlayer)
    {
        this.representedAction = action;
        this.UpdateActionIcon(action.IconImage);
        this.tooltipManager.UpdateTooltip(action.Description);
        this.buttonBase.interactable = isPlayer;
    }

    public void UpdateActionIcon(Sprite newIcon)
    {
        this.iconImage.sprite = newIcon;
    }

    public void ResetButton()
    {
        this.UpdateActionIcon(null);
        this.tooltipManager.UpdateTooltip("");
    }

    public void OnButtonPressed()
    {
        if (this.representedAction != null)
        {
            this.OnActionButtonPressed?.Invoke(this.representedAction);
        }
    }
}
