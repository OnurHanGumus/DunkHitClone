using UnityEngine;
using Signals;
using Enums;
using Managers;
using Data.ValueObject;
using System;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private bool isPlayerUp = false;
        [SerializeField] private Rigidbody2D rig;
        [SerializeField] private PlayerManager manager;
        #endregion
        #region Private Variables
        private PlayerData _data;
        private bool _isPlayerOnGround = false;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("TopPota"))
            {
                isPlayerUp = true;
            }
            else if (other.CompareTag("BottomPota"))
            {
                if (isPlayerUp.Equals(true))
                {
                    LevelSignals.Instance.onBasket?.Invoke();
                    if (Mathf.Abs(rig.velocity.y) > _data.ComboTresholdSpeedValue)
                    {
                        ScoreSignals.Instance.onComboBasket?.Invoke(true);
                    }
                    else
                    {
                        ScoreSignals.Instance.onComboBasket?.Invoke(false);

                    }
                }
            }
            else if (other.CompareTag("Ground"))
            {
                _isPlayerOnGround = true;
                if (manager.IsTimeUp)
                {
                    CoreGameSignals.Instance.onLevelFailed?.Invoke();
                }
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("TopPota"))
            {
                isPlayerUp = false;
            }
            else if (other.CompareTag("Ground"))
            {
                _isPlayerOnGround = false;
            }
        }
        public bool IsOnGround()
        {
            return _isPlayerOnGround;
        }
    }
}