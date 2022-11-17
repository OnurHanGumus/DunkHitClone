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

namespace Managers
{
    public class PotaCreatorManager : MonoBehaviour
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


        private void OnBasket()
        {
            GameObject temp = PoolSignals.Instance.onGetObject?.Invoke(PoolEnums.Pota);
            temp.transform.position = new Vector2(_data.InitializeXPos * (_isOnRight ? -1 : 1), Random.Range(_data.MinYPos, _data.MaxYPos + 1));
            temp.transform.eulerAngles = new Vector3(0, (_isOnRight ? _data.LeftEuler : _data.RightEuler), 0);
            temp.SetActive(true);
            _isOnRight = !_isOnRight;
        }
    }
}