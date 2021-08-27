using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class RoundsManagerPresenter : ConstructableBehaviour<RoundsManager>
{
    [SerializeField] private Image drawBlockPrefab = null;

    [Space]

    [SerializeField] private string currentRoundLabel = "Round:";

    [Space]

    [SerializeField] private RectTransform drawNextTetrominoContainer = null;

    [SerializeField] private Text roundText = null;

    [SerializeField] private GameObject roundEndObject = null;

    private List<GameObject> drawnBlocksObjects = new List<GameObject>();

    protected override void Subscribe()
    {
        disposablesContainer.Add(model.nextTetromino.Subscribe(nextTetromino =>
        {
            ClearDrawnBlocksObjects();

            if (nextTetromino != null)
            {
                DrawNextTetromino(nextTetromino);
            }

            roundEndObject.SetActive(nextTetromino == null);
        }));

        disposablesContainer.Add(model.roundNumber.Subscribe(roundNumber =>
        {
            roundText.DOFade(0, 0.1f).OnComplete(() =>
            {
                roundText.text = currentRoundLabel + " " + roundNumber.ToString();
                roundText.DOFade(1, 0.1f);
            });
        }));
    }

    private void DrawNextTetromino(Tetromino nextTetromino)
    {
        Color color = nextTetromino.blocks[0].color.Value;
        Sprite sprite = nextTetromino.blocks[0].sprite.Value;

        bool[,] shape = nextTetromino.shape;
        Vector2Int shapeSize = new Vector2Int(shape.GetLength(0), shape.GetLength(1));

        Vector2 blockSize = drawNextTetrominoContainer.rect.size / (shapeSize + Vector2Int.one);

        if (blockSize.x < blockSize.y)
        {
            blockSize.y = blockSize.x;
        }
        else
        {
            blockSize.x = blockSize.y;
        }

        for (int i = 0; i < shapeSize.x; i++)
        {
            for (int j = 0; j < shapeSize.y; j++)
            {
                if (shape[i, j])
                {
                    Image blockImage = Instantiate(drawBlockPrefab, drawNextTetrominoContainer);
                    drawnBlocksObjects.Add(blockImage.gameObject);

                    blockImage.color = color;
                    blockImage.sprite = sprite;
                    blockImage.rectTransform.sizeDelta = blockSize;

                    blockImage.transform.localPosition = GetBlockPosition(blockSize, new Vector2Int(j, i), new Vector2(shapeSize.y, shapeSize.x));
                }
            }
        }
    }

    private Vector2 GetBlockPosition(Vector2 blockSize, Vector2Int gridPos, Vector2 drawnTetrominoSize)
    {
        Vector2 freeSpace = drawNextTetrominoContainer.rect.size - drawnTetrominoSize * blockSize;

        Vector2 newPosition = blockSize * gridPos + freeSpace / 2;

        return newPosition;
    }

    private void ClearDrawnBlocksObjects()
    {
        drawnBlocksObjects.ForEach(db => Destroy(db));
        drawnBlocksObjects.Clear();
    }
}
