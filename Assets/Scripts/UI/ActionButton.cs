using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    private HoverDisplayTooltip tooltipManager = null;
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        this.tooltipManager = this.GetComponent<HoverDisplayTooltip>();
    }

    public void UpdateAbility(ActionStats action)
    {
        this.UpdateActionIcon(action.IconImage);
        this.tooltipManager.UpdateTooltip(action.Description);
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
}
