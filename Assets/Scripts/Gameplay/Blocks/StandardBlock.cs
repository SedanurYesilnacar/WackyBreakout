using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block
{
    [SerializeField]
    private Sprite[] _spriteArr;

    SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _pointValue = ConfigurationUtils.StandardBlockPoint;

        int randomSprite = Random.Range(0, _spriteArr.Length);
        spriteRenderer.sprite = _spriteArr[randomSprite];
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Destroy(gameObject);
    }
}
