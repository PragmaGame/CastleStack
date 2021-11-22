using System.Collections;
using UnityEngine;

namespace Core
{
    public class CoroutinesSystem : MonoBehaviour
    {
        public Coroutine StartRoutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void StopRoutine(Coroutine routine)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }
        }
    }
}