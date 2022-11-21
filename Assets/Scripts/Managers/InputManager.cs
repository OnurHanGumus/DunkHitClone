using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;
        [SerializeField] private bool isTimeUp = false;
        [SerializeField] private bool isReadyForTouch;



        #endregion

        #region Serialized Variables



        #endregion

        #region Private Variables



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
            if (!isReadyForTouch)
            {
                return;
            }
            if (IsPointerOverUIElement())
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (isTimeUp)
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
            isTimeUp = true;
        }

        private void OnBasket()
        {
            isTimeUp = false;

        }
        private void OnReset()
        {
            isTimeUp = false;
            isReadyForTouch = false;
        }
    }
}