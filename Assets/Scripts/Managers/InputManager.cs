using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables



        #endregion

        #region Private Variables

        private bool _isTimeUp = false;
        private bool _isReadyForTouch;



        #endregion

        #endregion


        private void Awake()
        {
            Data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;


        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            //PlayerSignals.Instance.onPlayerDie += OnChangePlayerLivingState;  //Ölüþ animasyonu sýrasýnda playeri hareket ettiremememiz için varlar.
            //PlayerSignals.Instance.onPlayerSpawned += OnChangePlayerLivingState;
            LevelSignals.Instance.onTimeUp += OnTimeUp;
            LevelSignals.Instance.onBasket += OnBasket;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            //PlayerSignals.Instance.onPlayerDie -= OnChangePlayerLivingState;
            //PlayerSignals.Instance.onPlayerSpawned -= OnChangePlayerLivingState;
            LevelSignals.Instance.onTimeUp -= OnTimeUp;
            LevelSignals.Instance.onBasket -= OnBasket;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (!_isReadyForTouch)
            {
                return;
            }
            if (IsPointerOverUIElement())
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (_isTimeUp)
                {
                    return;
                }
                InputSignals.Instance.onClicked?.Invoke();
            }
        }

        private void OnEnableInput()
        {
            _isReadyForTouch = true;
        }

        private void OnDisableInput()
        {
            _isReadyForTouch = false;
        }

        private void OnPlay()
        {
            _isReadyForTouch = true;
        }

        private bool IsPointerOverUIElement()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void OnTimeUp()
        {
            _isTimeUp = true;
        }

        private void OnBasket()
        {
            _isTimeUp = false;

        }
        private void OnReset()
        {
            _isTimeUp = false;
            _isReadyForTouch = false;
        }
    }
}