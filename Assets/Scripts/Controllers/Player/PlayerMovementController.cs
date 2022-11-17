using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        #endregion
        #region Private Variables
        private Rigidbody2D _rig;
        private PlayerManager _manager;
        private PlayerData _data;

        private bool _isClicked = false;
        private bool _isNotStarted = true;
        private bool _isPlayerDead = false;
        private bool _isRopeReached = false;


        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody2D>();
            _manager = GetComponent<PlayerManager>();
            _data = _manager.GetData();
        }


        private void FixedUpdate()
        {
            if (_isNotStarted || _isPlayerDead)
            {
                return;
            }

            if (_isClicked)
            {
                _rig.velocity = Vector3.zero;
                _rig.AddForce(new Vector2(_data.ForceX, _data.ForceY));
                _isClicked = false;
            }
        }



        public void OnClicked()
        {
            _isClicked = true;
        }


        public void OnPlay()
        {
            _isNotStarted = false;
        }
        public void OnPlayerDie()
        {
            _isPlayerDead = true;
            _rig.velocity = Vector3.zero;
        }
        public void OnReset()
        {

            _isPlayerDead = false;
        }
    }
}