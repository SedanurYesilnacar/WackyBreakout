using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectUtils
{
    static SpeedUpEffectMonitor speedUpEffectMonitor;

    public static bool SpeedUpActive
    {
        get { return speedUpEffectMonitor.SpeedUpActive; }
    }

    public static float TimeLeft
    {
        get { return speedUpEffectMonitor.TimeLeft; }
    }

    public static float SpeedUpFactor
    {
        get { return speedUpEffectMonitor.SpeedUpFactor; }
    }

    static EffectUtils()
    {
        speedUpEffectMonitor = Camera.main.GetComponent<SpeedUpEffectMonitor>();
    }
}
