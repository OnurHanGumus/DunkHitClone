using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;
using Data.ValueObject;
using Managers;
using UnityEngine.UI;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI mainScoreText, increasedScoreText, comboCounterText, comboCommentText;
    [SerializeField] private UIManager manager;
    [SerializeField] private Slider timeSlider;
    #endregion
    #region Private Variables
    private int _comboCounter = 1;
    private int _comboIndeks = 0;
    private UIData _data;
    private bool _isCounterActive = false;


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
        _data = manager.GetData();

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
            UpdateIncreaseText(_comboCounter);
            UpdateComboCounterText(true);
        }
        else
        {
            _comboCounter = 1;
            _comboIndeks = 0;
            ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Score, 1);
            UpdateIncreaseText(1);
            UpdateComboCounterText(false);

        }
    }

    private void UpdateIncreaseText(int value)
    {
        increasedScoreText.alpha = 1;

        increasedScoreText.transform.DOMoveY(_data.IncreasedTextIncreasedYPos, 0.5f).SetEase(Ease.InOutBack).OnComplete(()=>
{
    increasedScoreText.transform.position = new Vector2(_data.IncreasedTextXPos, _data.IncreasedTextNormalYPos);
    increasedScoreText.alpha = 0;
    });;
        increasedScoreText.text = "+" + value;
    }

    private void UpdateComboCounterText(bool isCombo)
    {
        if (isCombo)
        {
            comboCounterText.alpha = 1;
            ++_comboIndeks;
            comboCounterText.text = "Perfect x" + _comboIndeks;
        }
        else
        {
            comboCounterText.alpha = 0;

            comboCounterText.text = "";
        }

    }

    private void FixedUpdate()
    {
        if (_isCounterActive)
        {
            SliderValue();
        }
        else
        {

        }
    }

    private void SliderValue()
    {
        timeSlider.value -= _data.SliderDecreaseValue;
        if (timeSlider.value <= 0)
        {
            _isCounterActive = false;
            LevelSignals.Instance.onTimeUp?.Invoke();
        }
    }

    public void OnBasket()
    {
        timeSlider.value = _data.SliderMaksTime;
        _isCounterActive = true;

    }
}
