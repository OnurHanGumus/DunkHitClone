using System;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public float ForceX = 10, ForceY = 100;
        public float ComboTresholdSpeedValue = 3;
        public float GravityScale = 2.5f;
    }
}