using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerParticleController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private List<GameObject> particles;
    #endregion
    #region Private Variables
    [ShowInInspector] private int _indeks = 0;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    public void OnComboIncreased(bool isCombo)
    {

        if (isCombo)
        {
            if (_indeks == particles.Count)
            {
                return;
            }
            Debug.Log("arttý");

            ComboReset();
            particles[_indeks++].SetActive(true);
        }
        else
        {
            _indeks = 0;
            ComboReset();
        }
        
    }

    private void ComboReset()
    {
        foreach (var i in particles)
        {
            i.SetActive(false);
        }
    }

    public void OnRestartLevel()
    {
        _indeks = 0;
        ComboReset();
    }
}
