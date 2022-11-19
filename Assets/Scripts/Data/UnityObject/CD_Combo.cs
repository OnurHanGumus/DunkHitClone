using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Combo", menuName = "Picker3D/CD_Combo", order = 0)]
    public class CD_Combo : ScriptableObject
    {
        public ComboData Data;
    }
}