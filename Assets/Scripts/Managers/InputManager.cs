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

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;


        #endregion

        #region Private Variables

        private bool _isTouching;

        private float _currentVelocity; //ref type
        private Vector2? _mousePosition; //ref type
        private Vector3 _moveVector; //ref type
        private bool _isTimeUp = false;
        

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
            isReadyForTouch = true;
        }

        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }

        private void OnPlay()
        {
            isReadyForTouch = true;
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
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }
    }
}