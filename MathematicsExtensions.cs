using System.Linq;
using System.Runtime.CompilerServices;
using AlonUnityUtils;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace AlonUnityUtils
{
    public static class MathematicsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 To3D(this float2 v)
        {
            return math.float3(v, 0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 To2D(this Transform t)
        {
            return To2D(t.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 To2D(this Vector3 v)
        {
            return math.float2(v.x, v.y);
        }
    }
}
