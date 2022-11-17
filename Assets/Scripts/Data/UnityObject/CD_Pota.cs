using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Pota", menuName = "Picker3D/CD_Pota", order = 0)]
    public class CD_Pota : ScriptableObject
    {
        public PotaData Data;
    }
}