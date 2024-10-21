using System.Collections;
using UnityEngine;

namespace ProjectName.Core
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(IEnumerator coroutine);
        public void StopAllCoroutines();
    }
}