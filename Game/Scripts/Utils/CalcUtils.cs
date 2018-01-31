namespace Game.Scripts.Utils
{
    public class CalcUtils
    {
        public static float getPrecents(float currentValue, float maxValue)
        {
            return currentValue / maxValue * 100;
        }


        public static float percentMultiplier(float targetValue, float percents)
        {
            return targetValue + targetValue / 100 * percents;
        }
    }
}