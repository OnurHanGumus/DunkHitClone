using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Screen", menuName = "Picker3D/CD_Screen", order = 0)]
    public class CD_Screen : ScriptableObject
    {
        public ScreenData Data;
    }
}