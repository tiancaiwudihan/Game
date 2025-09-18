using UnityEngine;
using System.Collections;

namespace ithappy
{
    public class OscillatePosition : MonoBehaviour
    {
        public Vector3 moveAxis = Vector3.up;
        public float moveDistance = 2f;
        public float duration = 2f;
        public bool useRandomDelay = false;
        public float maxRandomDelay = 1f;
        public bool useFixedDelay = false;
        public float fixedDelay = 0f;
        public float delayBetweenCycles = 0f;

        private Vector3 startPosition;
        private float timeElapsed = 0f;
        private float delayElapsed = 0f;
        private bool isReversing = false;
        private float randomDelay = 0f;

        void Start()
        {
            startPosition = transform.position;

            if (useRandomDelay)
            {
                randomDelay = Random.Range(0f, maxRandomDelay);
            }
        }

        void Update()
        {
            if (useFixedDelay && delayElapsed < fixedDelay)
            {
                delayElapsed += Time.deltaTime;
                return;
            }

            if (timeElapsed < randomDelay)
            {
                timeElapsed += Time.deltaTime;
                return;
            }

            float progress = (timeElapsed - randomDelay) / (duration / 2f);
            progress = Mathf.Clamp01(progress);

            progress = EaseInOut(progress);

            float currentDistance = moveDistance * (isReversing ? (1 - progress) : progress);
            Vector3 currentPosition = startPosition + moveAxis.normalized * currentDistance;

            transform.position = currentPosition;

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= duration / 2f + randomDelay)
            {
                timeElapsed = 0f;
                isReversing = !isReversing;

                StartCoroutine(CycleDelay());
            }
        }

        private IEnumerator CycleDelay()
        {
            yield return new WaitForSeconds(delayBetweenCycles);
        }

        private float EaseInOut(float t)
        {
            return t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
        }
    }
}
