using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        ScreenUtils.Initialize();
        ConfigurationUtils.Initialize();
    }
}
