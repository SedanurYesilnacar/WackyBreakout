using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupBlock : Block
{
    [SerializeField]
    private Sprite _freezerTile;
    [SerializeField]
    private Sprite _speedUpTile;

    private PickupEffect _effect;

    private float _freezerEffectDuration;
    FreezerEffectActivated freezerEffectEvent;

    private float _speedUpDuration;
    private float _speedUpFactor;
    SpeedUpEffectActivated speedUpEffectEvent;

    SpriteRenderer spriteRenderer;


    public PickupEffect PickupBlockType
    {
        set
        {
            _effect = value;

            spriteRenderer = GetComponent<SpriteRenderer>();
            if(_effect == PickupEffect.Freezer)
            {
                spriteRenderer.sprite = _freezerTile;
                _freezerEffectDuration = ConfigurationUtils.FreezerEffectDuration;
                freezerEffectEvent = new FreezerEffectActivated();
                EventManager.AddFreezerEffectInvoker(this);
            }
            else if(_effect == PickupEffect.Speedup)
            {
                spriteRenderer.sprite = _speedUpTile;
                _speedUpFactor = ConfigurationUtils.SpeedUpFactor;
                _speedUpDuration = ConfigurationUtils.SpeedUpDuration;
                speedUpEffectEvent = new SpeedUpEffectActivated();
                EventManager.AddSpeedUpEffectInvoker(this);
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        _pointValue = ConfigurationUtils.PickupBlockPoint;
    }

    public void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectEvent.AddListener(listener);
    }

    public void AddSpeedUpEffectListener(UnityAction<float,float> listener)
    {
        speedUpEffectEvent.AddListener(listener);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            base.OnCollisionEnter2D(collision);
            if (_effect == PickupEffect.Freezer)
            {
                freezerEffectEvent.Invoke(_freezerEffectDuration);
            } else
            {
                speedUpEffectEvent.Invoke(_speedUpFactor, _speedUpDuration);
            }
            Destroy(gameObject);
        }
    }
}
