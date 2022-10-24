using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace AlonUnityUtils
{
    public class InputSystem
    {
        private bool lastIsTouching;
        private bool isTouching;
        private Touch? touch;

        private float timeIdle;

        public InputSystem()
        {
            timeIdle = 0f;
        }

        public void EarlyUpdate()
        {
            lastIsTouching = isTouching;
            isTouching = Input.touches.Any();
            touch = null;
            if (Input.touchCount > 0) {
                touch = Input.touches[0];
            }

            var anyInput = IsPressing();
            if (anyInput) {
                timeIdle = 0;
            } else {
                timeIdle += Time.unscaledDeltaTime;
            }
        }

        public Vector3 PressedPosition()
        {
            if (touch.HasValue) {
                return math.float3(touch.Value.position, 0f);
            }
            return Input.mousePosition;
        }

        public bool IsJustPressedDown()
        {
            if (touch.HasValue) {
                return touch.Value.phase == TouchPhase.Began;
            }
            return Input.GetMouseButtonDown(0);
            // ||
            // (isTouching && !lastIsTouching);
        }

        public bool IsPressing()
        {
            return Input.GetMouseButton(0) || touch.HasValue;
        }

        public bool IsJustPressedUp()
        {
            if (touch.HasValue) {
                return touch.Value.phase == TouchPhase.Ended;
            }
            return Input.GetMouseButtonUp(0);
            // ||
            // (!isTouching && lastIsTouching);
        }

        public float GetTimeIdle()
        {
            return timeIdle;
        }

    }
}
