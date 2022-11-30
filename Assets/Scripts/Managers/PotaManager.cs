using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace Managers
{
    public class PotaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        #endregion

        #region Private Variables
        private bool _isOnRight = true;
        private PotaData _data;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
        }
        public PotaData GetData() => Resources.Load<CD_Pota>("Data/CD_Pota").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
            InitialMove();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onBasket += OnBasket;

        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onBasket -= OnBasket;

        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void InitialMove()
        {
            _isOnRight = transform.position.x > 0;
            transform.DOMoveX(_data.StandartXPos * (_isOnRight ? 1 : -1), _data.PotaTiming).SetEase(Ease.InOutBack);
            //transform.DORotate(new Vector3(0, 0, 0), _data.PotaTiming);
        }

        private void OnBasket()
        {
            transform.DOMoveX(_data.InitializeXPos * (_isOnRight ? 1 : -1), _data.PotaTiming).SetEase(Ease.InOutBack).OnComplete(() => gameObject.SetActive(false));
            transform.DOLocalRotate(new Vector3(0, (_isOnRight ? _data.RightEuler : _data.LeftEuler), -90), _data.PotaTiming);
        }
    }
}