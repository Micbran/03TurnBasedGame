using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDisplayTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject ToolTipCanvasReference;
    [SerializeField] private Text ToolTipText;
    private string TooltipDisplayString = "";

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TooltipDisplayString != "")
        {
            ToolTipText.text = TooltipDisplayString;
            ToolTipCanvasReference.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipCanvasReference.SetActive(false);
    }

    public void UpdateTooltip(string newTooltip)
    {
        this.TooltipDisplayString = newTooltip;
    }
}
