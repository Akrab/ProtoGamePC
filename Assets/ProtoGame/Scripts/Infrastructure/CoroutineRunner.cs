using ProtoGame.Extensions;
using RSG;
using System.Collections;
using UnityEngine;

namespace ProtoGame.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        IPromise WaitEndOfFrames(int framesCount);
    }
    public class CoroutineRunner : MonoBehaviour , ICoroutineRunner
    {
        public IPromise WaitEndOfFrames(int framesCount)
        {
            return this.WaitForEndOfFrames(framesCount);
        }
    }
}
