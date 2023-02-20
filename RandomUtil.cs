using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace AlonUnityUtils
{
    public class RandomUtil
    {
        static public T Element<T>(T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        static public T ElementOrDefault<T>(T[] array)
        {
            if (array.Length == 0)
                return default(T);

            return array[Random.Range(0, array.Length)];
        }
    }
}
