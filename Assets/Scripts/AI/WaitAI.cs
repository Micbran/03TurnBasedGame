using System.Collections;
using UnityEngine;

public class WaitAI : ArtificialIntelligence
{
    public override void Act()
    {
        this.turnFinished = false;
        StartCoroutine(WaitingRoutine(1.0f));
    }

    IEnumerator WaitingRoutine(float pauseDuration)
    {
        this.turnFinished = false;
        yield return new WaitForSeconds(pauseDuration);
        this.turnFinished = true;
    }
}
