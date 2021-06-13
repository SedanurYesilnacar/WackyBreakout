using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _paddlePrefab;

    [SerializeField]
    private GameObject _standardBlockPrefab;
    [SerializeField]
    private GameObject _bonusBlockPrefab;
    [SerializeField]
    private GameObject _pickupBlockPrefab;

    private float _screenTop;
    private float _screenBottom;
    private float _screenRight;
    private float _screenLeft;

    private float _blockColliderWidth;
    private float _blockColliderHeight;

    private float _xSpaceBetweenBlocks;
    private float _ySpaceBetweenBlocks = 0.15f;
    private float _blockSideSpace = 1f;

    private int _blockRows = 3;
    private int _blockPieceInt;

    private float _standardBlockProbability;
    private float _bonusBlockProbability;
    private float _pickupBlockProbability;

    public int BlockPiece
    {
        get { return _blockPieceInt * _blockRows; }
    }

    private void Start()
    {
        Instantiate(_paddlePrefab);

        _screenTop = ScreenUtils.ScreenTop;
        _screenBottom = ScreenUtils.ScreenBottom;
        _screenRight = ScreenUtils.ScreenRight;
        _screenLeft = ScreenUtils.ScreenLeft;

        GameObject tempBlock = Instantiate<GameObject>(_standardBlockPrefab);

        _blockColliderWidth = tempBlock.GetComponent<BoxCollider2D>().size.x;
        _blockColliderHeight = tempBlock.GetComponent<BoxCollider2D>().size.y;

        Destroy(tempBlock);

        _standardBlockProbability = ConfigurationUtils.StandardBlockProbability;
        _bonusBlockProbability = ConfigurationUtils.BonusBlockProbability;
        _pickupBlockProbability = ConfigurationUtils.PickupBlockProbability;

        CreateBlocks();
    }

    private void CreateBlocks()
    {
        float blockPositionY = _screenTop - (_screenTop - _screenBottom) * 0.2f;

        float blockPieceFloat = ((_screenRight - _screenLeft) - 2 * _blockSideSpace) / _blockColliderWidth;
        _blockPieceInt = Mathf.FloorToInt(blockPieceFloat);

        if(blockPieceFloat % _blockPieceInt == 0)
        {
            _blockPieceInt--;
        }
        _xSpaceBetweenBlocks = (blockPieceFloat % _blockPieceInt) / (_blockPieceInt - 1);

        float firstBlockPositionX = _screenLeft + _blockSideSpace + _blockColliderWidth / 2;
        float blockPositionX = firstBlockPositionX;

        for(int j = 0; j < _blockRows; j++)
        {
            for(int i = 0; i < _blockPieceInt; i++)
            {
                GameObject blockPrefabType = GetBlockType();
                GameObject block = Instantiate(blockPrefabType, new Vector2(blockPositionX,blockPositionY), Quaternion.identity);
                // Set pickup block sprite based on the random value.
                if(blockPrefabType == _pickupBlockPrefab)
                {
                    PickupBlock pickupBlockScript = block.GetComponent<PickupBlock>();
                    int blockType = Random.Range(0, System.Enum.GetValues(typeof(PickupEffect)).Length);
                    pickupBlockScript.PickupBlockType = (PickupEffect)blockType;
                }
                blockPositionX += _blockColliderWidth + _xSpaceBetweenBlocks;
            }
            blockPositionX = firstBlockPositionX;
            blockPositionY -= _blockColliderHeight + _ySpaceBetweenBlocks;
        }
    }

    private GameObject GetBlockType()
    {

        float randomBlockType = Random.value * 10;

        if (randomBlockType < _standardBlockProbability)
        {
            return _standardBlockPrefab;
        }
        else if (randomBlockType < _standardBlockProbability + _bonusBlockProbability)
        {
            return _bonusBlockPrefab;
        }
        else
        {
            return _pickupBlockPrefab;
        }
    }
}
