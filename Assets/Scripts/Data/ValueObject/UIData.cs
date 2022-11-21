using System;

namespace Data.ValueObject
{
    [Serializable]
    public class UIData
    {
        public int IncreasedTextIncreasedYPos = 1734, IncreasedTextNormalYPos = 1619, IncreasedTextXPos = 540;
        public float SliderDecreaseValue = 0.02f;
        public int SliderMaksTime = 5;
        public float IncreasedTextRiseDelay = 0.5f;
        public float IncreasedTextShowTime = 0.5f;

        public float FadeMaksValue = 0.2f, FadeDelay = 0.5f;
    }
}