using System;

namespace Data.ValueObject
{
    [Serializable]
    public class PotaData
    {
        public float MinYPos = -2, MaxYPos = 2;
        public float StandartXPos = 5.25f, InitializeXPos = 8.0f;
        public float PotaTiming = 0.5f;
        public int LeftEuler = 180, RightEuler = 0;
    }
}