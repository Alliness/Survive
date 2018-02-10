using System;

namespace Game.Scripts.Utils
{
    public class RandomUtils
    {
        /**
         * Return random enum value from enum type
         */
        public static T RandomEnum<T>()
        {
            Random rnd = new Random();
            Array enumVals =Enum.GetValues(typeof(T));
            int i = rnd.Next(1, enumVals.Length-1);
            return (T) enumVals.GetValue(i);
        }
    }
}