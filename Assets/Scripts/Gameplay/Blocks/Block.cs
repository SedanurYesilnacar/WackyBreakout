using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    protected static int blockLeft;
    protected int _pointValue;

    protected PointsAddedEvent pointsAddedEvent;
    BlockDestroyedEvent blockDestroyedEvent;

    private void Awake()
    {
        LevelBuilder levelBuilder = Camera.main.GetComponent<LevelBuilder>();
        blockLeft = levelBuilder.BlockPiece;        
    }

    protected virtual void Start()
    {
        blockDestroyedEvent = new BlockDestroyedEvent();
        pointsAddedEvent = new PointsAddedEvent();

        EventManager.AddBlockDestroyedInvoker(this);
        EventManager.AddAddPointsInvoker(this);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        blockLeft--;
        pointsAddedEvent.Invoke(_pointValue);
        AudioManager.Play(AudioClipName.BreakBlock);
        if (blockLeft <= 0)
        {
            blockDestroyedEvent.Invoke();
        }
    }

    public void AddAddPointsListener(UnityAction<int> listener)
    {
        pointsAddedEvent.AddListener(listener);
    }

    public void AddBlockDestroyedListener(UnityAction listener)
    {
        blockDestroyedEvent.AddListener(listener);
    }
}
