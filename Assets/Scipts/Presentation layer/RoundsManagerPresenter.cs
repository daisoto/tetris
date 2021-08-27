using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RoundsManagerPresenter : ConstructableBehaviour<RoundsManager>
{
    [SerializeField] private RectTransform rectTransform = null;

    [SerializeField] private Text roundNumber = null;

    [SerializeField] private GameObject roundEndObject = null;

    protected override void Subscribe()
    {
        //disposablesContainer.Add(model.nextTetromino.Subscribe(nextTetromino =>
        //{
        //    if (nextTetromino != null)
        //    {
        //        DrawNextTetromino(nextTetromino);
        //    }

        //    roundEndObject.SetActive(nextTetromino == null);
        //}));

        //disposablesContainer.Add(model.roundNumber.Subscribe(roundNumber =>
        //{
        //    this.roundNumber.text = roundNumber.ToString();
        //}));
    }

    private void DrawNextTetromino(Tetromino nextTetromino)
    {
        Color color = nextTetromino.blocks[0].color.Value;
        Sprite sprite = nextTetromino.blocks[0].sprite.Value;

        bool[,] shape = nextTetromino.shape;
        Vector2Int shapeSize = new Vector2Int(shape.GetLength(0), shape.GetLength(1));

        Vector2 blockSize = rectTransform.sizeDelta / (shapeSize + Vector2Int.one);


    }
}
