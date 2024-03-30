using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldInstruction
{
    private static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    private static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static Dictionary<float, WaitForSeconds> WaitForSecondsDic = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (WaitForSecondsDic.TryGetValue(seconds, out var waitForSeconds))
        {
            WaitForSecondsDic.Add(seconds, waitForSeconds = new WaitForSeconds(seconds));
        }
        return waitForSeconds;
    }
}
