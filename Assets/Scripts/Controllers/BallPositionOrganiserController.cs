using Data.UnityObject;
using Data.ValueObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionOrganiserController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables


    #endregion

    #region Serialized Variables



    #endregion

    #region Private Variables
    private bool _isOnRight = true;
    private ScreenData _data;

    #endregion

    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _data = GetData();
        _isOnRight = transform.position.x > 0;
    }
    private ScreenData GetData() => Resources.Load<CD_Screen>("Data/CD_Screen").Data;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(_data.ScreenPos *  (_isOnRight ? -1 : 1), other.transform.position.y);
        }
    }
}
