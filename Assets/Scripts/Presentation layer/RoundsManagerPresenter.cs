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

    [SerializeField] private float fadeDuration = 0.1f;

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
            roundText.DOFade(0, fadeDuration).OnComplete(() =>
            {
                roundText.text = currentRoundLabel + " " + roundNumber.ToString();
                roundText.DOFade(1, fadeDuration);
            });
        }));
    }

    private void DrawNextTetromino(Tetromino nextTetromino)
    {
        Color color = nextTetromino.blocks[0].color.Value;
        Sprite sprite = nextTetromino.blocks[0].sprite.Value;

        bool[,] shape = nextTetromino.shape;

        Vector2Int shapeSize = new Vector2Int(shape.GetLength(1), shape.GetLength(0));

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
                if (shape[j, i])
                {
                    Image blockImage = Instantiate(drawBlockPrefab, drawNextTetrominoContainer);
                    drawnBlocksObjects.Add(blockImage.gameObject);

                    blockImage.color = color;
                    blockImage.sprite = sprite;
                    blockImage.rectTransform.sizeDelta = blockSize;

                    blockImage.transform.localPosition = GetBlockPosition(blockSize, new Vector2Int(i, j), shapeSize);
                }
            }
        }
    }

    private Vector2 GetBlockPosition(Vector2 blockSize, Vector2Int pos, Vector2 drawnTetrominoSize)
    {
        Vector2 freeSpace = drawNextTetrominoContainer.rect.size - drawnTetrominoSize * blockSize;

        return blockSize * pos + freeSpace / 2;
    }

    private void ClearDrawnBlocksObjects()
    {
        drawnBlocksObjects.ForEach(db => Destroy(db));
        drawnBlocksObjects.Clear();
    }
}
