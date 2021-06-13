using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    private float paddleMoveUnitsPerSecond = 10f;
    private float ballImpulseForce = 200f;
    private float ballLifetime = 10f;
    private float minBallSpawnTime = 5f;
    private float maxBallSpawnTime = 10f;
    private int standardBlockPoint = 10;
    private int bonusBlockPoint = 20;
    private int pickupBlockPoint = 25;
    private float standardBlockProbability = 7;
    private float bonusBlockProbability = 2;
    private float pickupBlockProbability = 1;
    private int numberOfBallsPerGame = 10;
    private float freezerEffectDuration = 2f;
    private float speedUpFactor = 2f;
    private float speedUpDuration = 2f;

    #endregion

    #region Properties

    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    public float BallImpulseForce
    {
        get { return ballImpulseForce; }
    }

    public float BallLifetime
    {
        get { return ballLifetime; }
    }

    public float MinBallSpawnTime
    {
        get { return minBallSpawnTime; }
    }

    public float MaxBallSpawnTime
    {
        get { return maxBallSpawnTime; }
    }

    public int StandardBlockPoint
    {
        get { return standardBlockPoint; }
    }

    public int BonusBlockPoint
    {
        get { return bonusBlockPoint; }
    }

    public int PickupBlockPoint
    {
        get { return pickupBlockPoint; }
    }

    public float StandardBlockProbability
    {
        get { return standardBlockProbability; }
    }

    public float BonusBlockProbability
    {
        get { return bonusBlockProbability; }
    }

    public float PickupBlockProbability
    {
        get { return pickupBlockProbability; }
    }

    public int NumberOfBallsPerGame
    {
        get { return numberOfBallsPerGame; }
    }

    public float FreezerEffectDuration
    {
        get { return freezerEffectDuration; }
    }

    public float SpeedUpFactor
    {
        get { return speedUpFactor; }
    }

    public float SpeedUpDuration
    {
        get { return speedUpDuration; }
    }

    #endregion

    #region Methods

    public ConfigurationData()
    {
        StreamReader input = null;

        try
        {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));

            string names = input.ReadLine();
            string values = input.ReadLine();

            if(values != null)
            {
                SetConfigurationData(values);
            }

        } catch(Exception e)
        {
            Debug.Log(e.Message);
        } finally
        {
            if(input != null)
            {
                input.Close();
            }
        }
    }

    private void SetConfigurationData(string csvValues)
    {
        string[] values = csvValues.Split(';');

        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        ballImpulseForce = float.Parse(values[1]);
        ballLifetime = float.Parse(values[2]);
        minBallSpawnTime = float.Parse(values[3]);
        maxBallSpawnTime = float.Parse(values[4]);
        standardBlockPoint = int.Parse(values[5]);
        bonusBlockPoint = int.Parse(values[6]);
        pickupBlockPoint = int.Parse(values[7]);
        standardBlockProbability = float.Parse(values[8]);
        bonusBlockProbability = float.Parse(values[9]);
        pickupBlockProbability = float.Parse(values[10]);
        numberOfBallsPerGame = int.Parse(values[11]);
        freezerEffectDuration = float.Parse(values[12]);
        speedUpFactor = float.Parse(values[13]);
        speedUpDuration = float.Parse(values[14]);
    }

    #endregion
}
