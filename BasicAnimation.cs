using System.Collections.Generic;
using UnityEngine;

namespace AlonUnityUtils
{
    public class BasicAnimation
    {
        private Transform transform;
        private float animationTime;
        private float pow;

        private Vector3 startPosition;
        private Vector3 targetPosition;

        private float timePassed;

        private bool finished;

        public BasicAnimation(Transform transform, float animationTime, Vector3 slideFromOffset, float pow)
        {
            this.transform = transform;
            this.animationTime = animationTime;
            this.pow = pow;

            targetPosition = transform.position;

            transform.position += slideFromOffset;

            startPosition = transform.position;
        }

        public bool IsFinished()
        {
            return finished;
        }

        public static void UpdateBasicAnimations(IEnumerable<BasicAnimation> basicAnimations)
        {
            foreach (var anim in basicAnimations) {
                // Timekeep
                anim.timePassed += Time.deltaTime;
                var t = Mathf.Clamp01(anim.timePassed / anim.animationTime);
                anim.finished = t >= 1f;
                t = Mathf.Pow(t, anim.pow);

                // Slide animation
                var lerpPos = Vector3.Lerp(anim.startPosition, anim.targetPosition, t);
                anim.transform.position = lerpPos;
            }
        }
    }
}
