using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI mainScoreText, increasedScoreText, comboCounterText, comboCommentText;
    #endregion
    #region Private Variables
    private int _comboCounter = 1;

    #endregion
    #region Properties

    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {


    }
    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Score))
        {
            mainScoreText.text = score.ToString();
        }
    }

    public void OnRestartLevel()
    {
        mainScoreText.text = 0.ToString();
    }

    public void OnComboBasket(bool isCombo)
    {
        if (isCombo)
        {
            if (_comboCounter < 8)
            {
                _comboCounter *= 2;
            }

            ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Score, _comboCounter);
        }
        else
        {
            _comboCounter = 1;
            ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Score, 1);

        }
    }
}
