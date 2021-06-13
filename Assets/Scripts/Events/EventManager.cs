using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    private static List<PickupBlock> freezerInvokers = new List<PickupBlock>();
    private static List<UnityAction<float>> freezerListeners = new List<UnityAction<float>>();

    private static List<PickupBlock> speedUpInvokers = new List<PickupBlock>();
    private static List<UnityAction<float, float>> speedUpListeners = new List<UnityAction<float, float>>();

    private static List<Block> addPointInvokers = new List<Block>();
    private static List<UnityAction<int>> addPointListeners = new List<UnityAction<int>>();

    private static List<Ball> ballLeftInvokers = new List<Ball>();
    private static List<UnityAction> ballLeftListeners = new List<UnityAction>();

    private static List<Ball> ballDiedInvokers = new List<Ball>();
    private static List<UnityAction> ballDiedListeners = new List<UnityAction>();

    private static List<HUD> lastBallLeftInvokers = new List<HUD>();
    private static List<UnityAction> lastBallLeftListeners = new List<UnityAction>();

    private static List<Block> blockDestroyedInvokers = new List<Block>();
    private static List<UnityAction> blockDestroyedListeners = new List<UnityAction>();

    public static void AddFreezerEffectListener(UnityAction<float> handler)
    {
        freezerListeners.Add(handler);
        foreach(PickupBlock invoker in freezerInvokers)
        {
            invoker.AddFreezerEffectListener(handler);
        }
    }

    public static void AddFreezerEffectInvoker(PickupBlock script)
    {
        freezerInvokers.Add(script);
        foreach(UnityAction<float> listener in freezerListeners)
        {
            script.AddFreezerEffectListener(listener);
        }
    }

    public static void AddSpeedUpEffectListener(UnityAction<float, float> handler)
    {
        speedUpListeners.Add(handler);
        foreach(PickupBlock invoker in speedUpInvokers)
        {
            invoker.AddSpeedUpEffectListener(handler);
        }
    }

    public static void AddSpeedUpEffectInvoker(PickupBlock script)
    {
        speedUpInvokers.Add(script);
        foreach(UnityAction<float,float> listener in speedUpListeners)
        {
            script.AddSpeedUpEffectListener(listener);
        }
    }

    public static void AddAddPointsListener(UnityAction<int> handler)
    {
        addPointListeners.Add(handler);
        foreach(Block invoker in addPointInvokers)
        {
            invoker.AddAddPointsListener(handler);
        }
    }

    public static void AddAddPointsInvoker(Block script)
    {
        addPointInvokers.Add(script);
        foreach(UnityAction<int> listener in addPointListeners)
        {
            script.AddAddPointsListener(listener);
        }
    }

    public static void AddBallLeftListener(UnityAction handler)
    {
        ballLeftListeners.Add(handler);
        foreach(Ball invoker in ballLeftInvokers)
        {
            invoker.AddBallLeftListener(handler);
        }
    }

    public static void AddBallLeftInvoker(Ball script)
    {
        ballLeftInvokers.Add(script);
        foreach(UnityAction listener in ballLeftListeners)
        {
            script.AddBallLeftListener(listener);
        }
    }

    public static void AddBallDiedListener(UnityAction handler)
    {
        ballDiedListeners.Add(handler);
        foreach(Ball invoker in ballDiedInvokers)
        {
            invoker.AddBallDiedListener(handler);
        }
    }

    public static void AddBallDiedInvoker(Ball script)
    {
        ballDiedInvokers.Add(script);
        foreach(UnityAction listener in ballDiedListeners)
        {
            script.AddBallDiedListener(listener);
        }
    }

    public static void AddLastBallLeftListener(UnityAction handler)
    {
        lastBallLeftListeners.Add(handler);
        foreach(HUD invoker in lastBallLeftInvokers)
        {
            invoker.AddLastBallLeftListener(handler);
        }
    }

    public static void AddLastBallLeftInvoker(HUD script)
    {
        lastBallLeftInvokers.Add(script);
        foreach(UnityAction listener in lastBallLeftListeners)
        {
            script.AddLastBallLeftListener(listener);
        }
    }

    public static void AddBlockDestroyedListener(UnityAction handler)
    {
        blockDestroyedListeners.Add(handler);
        foreach(Block invoker in blockDestroyedInvokers)
        {
            invoker.AddBlockDestroyedListener(handler);
        }
    }

    public static void AddBlockDestroyedInvoker(Block script)
    {
        blockDestroyedInvokers.Add(script);
        foreach(UnityAction listener in blockDestroyedListeners)
        {
            script.AddBlockDestroyedListener(listener);
        }
    }
}
