using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils
{
    #region Fields

    private static ConfigurationData configurationData;

    #endregion

    #region Properties

    public static float PaddleMoveUnitsPerSecond
    {
        get
        {
            return configurationData.PaddleMoveUnitsPerSecond;
        }
    }

    public static float BallImpulseForce
    {
        get
        {
            return configurationData.BallImpulseForce;
        }
    }

    public static float BallLifetime
    {
        get
        {
            return configurationData.BallLifetime;
        }
    }

    public static float MinBallSpawnTime
    {
        get
        {
            return configurationData.MinBallSpawnTime;
        }
    }

    public static float MaxBallSpawnTime
    {
        get
        {
            return configurationData.MaxBallSpawnTime;
        }
    }

    public static int StandardBlockPoint
    {
        get
        {
            return configurationData.StandardBlockPoint;
        }
    }

    public static int BonusBlockPoint
    {
        get
        {
            return configurationData.BonusBlockPoint;
        }
    }

    public static int PickupBlockPoint
    {
        get
        {
            return configurationData.PickupBlockPoint;
        }
    }

    public static float StandardBlockProbability
    {
        get
        {
            return configurationData.StandardBlockProbability;
        }
    }

    public static float BonusBlockProbability
    {
        get
        {
            return configurationData.BonusBlockProbability;
        }
    }

    public static float PickupBlockProbability
    {
        get
        {
            return configurationData.PickupBlockProbability;
        }
    }

    public static int NumberOfBallsPerGame
    {
        get
        {
            return configurationData.NumberOfBallsPerGame;
        }
    }

    public static float FreezerEffectDuration
    {
        get
        {
            return configurationData.FreezerEffectDuration;
        }
    }

    public static float SpeedUpFactor
    {
        get
        {
            return configurationData.SpeedUpFactor;
        }
    }

    public static float SpeedUpDuration
    {
        get
        {
            return configurationData.SpeedUpDuration;
        }
    }

    #endregion

    // Initializes the configuration utils
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
