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
    private UIData _uiData;
    private ComboData _comboData;
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
        _uiData = manager.GetUIData();
        _comboData = GetComboData();
    }
    public ComboData GetComboData() => Resources.Load<CD_Combo>("Data/CD_Combo").Data;

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
    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Score))
        {
            mainScoreText.text = score.ToString();
        }
    }



    public void OnComboBasket(bool isCombo)
    {
        if (isCombo)
        {
            if (_comboCounter < _comboData.MaksComboValue)
            {
                _comboCounter *= _comboData.ComboValueMultiplier;
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

        increasedScoreText.transform.DOMoveY(_uiData.IncreasedTextIncreasedYPos, _uiData.IncreasedTextRiseDelay).SetEase(Ease.InOutBack).OnComplete(()=>
        {
            StartCoroutine(IncreaseTextEffect());
        });
        increasedScoreText.text = "+" + value;
    }

    private IEnumerator IncreaseTextEffect()
    {
        yield return new WaitForSeconds(_uiData.IncreasedTextShowTime);
        increasedScoreText.alpha = 0;
        increasedScoreText.transform.position = new Vector2(_uiData.IncreasedTextXPos, _uiData.IncreasedTextNormalYPos);

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

    private void SliderValue()
    {
        timeSlider.value -= _uiData.SliderDecreaseValue;
        if (timeSlider.value <= 0)
        {
            _isCounterActive = false;
            LevelSignals.Instance.onTimeUp?.Invoke();
        }
    }

    public void OnBasket()
    {
        timeSlider.value = _uiData.SliderMaksTime;
        _isCounterActive = true;

    }

    public void OnRestartLevel()
    {
        mainScoreText.text = 0.ToString();
        timeSlider.value = _uiData.SliderMaksTime;
    }

}
