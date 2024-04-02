using RSG;
using System.Collections;
using UnityEngine;

namespace ProtoGame.Extensions
{
    public static class MonoExtensions
    {
        public static IPromise WaitForEndOfFrames(this MonoBehaviour monoBehaviour, int framesCount) =>
            WaitForFrames(monoBehaviour, framesCount);

        private static IPromise WaitForFrames(MonoBehaviour monoBehaviour, int framesCount)
        {
            if (framesCount <= 0) return Promise.Resolved();
            Promise waitPromise = new();
            monoBehaviour.StartCoroutine(WaitForEndOfFrame(waitPromise));

            return waitPromise.Then(() => WaitForFrames(monoBehaviour, framesCount - 1));
        }

        private static IEnumerator WaitForEndOfFrame(Promise waitPromise)
        {
            yield return new WaitForEndOfFrame();
            waitPromise.Resolve();
        }



    }
}
