using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block
{
    [SerializeField]
    private Sprite tileBroken;

    private int hitNumber = 0;

    SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        _pointValue = ConfigurationUtils.BonusBlockPoint;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if(hitNumber == 0)
            {
                hitNumber++;
                spriteRenderer.sprite = tileBroken;
            } else
            {
                base.OnCollisionEnter2D(collision);
                Destroy(gameObject);
            }
        }
    }
}
