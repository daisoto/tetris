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
        disposablesContainer.Add(model.nextTetromino.Subscribe(nextTetromino =>
        {
            if (nextTetromino != null)
            {
                DrawNextTetromino(nextTetromino.shape);
            }

            roundEndObject.SetActive(nextTetromino == null);
        }));

        disposablesContainer.Add(model.roundNumber.Subscribe(roundNumber =>
        {
            this.roundNumber.text = roundNumber.ToString();
        }));
    }

    private void DrawNextTetromino(bool[,] shape)
    { 
    }
}
