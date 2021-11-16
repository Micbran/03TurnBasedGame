using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPointDisplayManager : MonoBehaviour
{
    [SerializeField] private List<Image> APImageReferences = new List<Image>();
    [SerializeField] private Sprite EmptyAPDot;
    [SerializeField] private Sprite FullAPDot;

    public void UpdateAPValues(int apLeft)
    {
        apLeft = apLeft > 5 ? 5 : apLeft < 0 ? 0 : apLeft;
        for (int i = 0; i < 5; i++)
        {
            if (i < apLeft)
            {
                APImageReferences[i].sprite = FullAPDot;
            }
            else
            {
                APImageReferences[i].sprite = EmptyAPDot;
            }
        }
    }
}
