using System;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public float ForceX = 10, ForceY = 100;
        public float ComboTresholdSpeedValue = 3;
    }
}