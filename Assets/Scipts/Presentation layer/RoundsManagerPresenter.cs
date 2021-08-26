using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RoundsManagerPresenter : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private Text nextRoundText;

    [SerializeField] private Text roundEndLabel;
}
