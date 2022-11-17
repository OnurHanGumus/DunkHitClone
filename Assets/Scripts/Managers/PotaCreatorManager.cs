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

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {

        }
        public PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

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
            temp.transform.position = new Vector2(8.5f, Random.Range(-2f, 2f));
            temp.SetActive(true);
        }
    }
}