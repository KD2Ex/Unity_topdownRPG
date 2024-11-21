using System;
using System.Collections;
using UnityEngine;

namespace NewInteractionSystem
{
    public class BlockInput : MonoBehaviour
    {
        [SerializeField] private InputReader input;

        private void OnEnable()
        {
            input.DisablePlayerInput();
            StartCoroutine(WaitOneFrame());
        }

        private void OnDisable()
        {
            input.EnablePlayerInput();
        }

        private IEnumerator WaitOneFrame()
        {
            yield return null;
            input.DisablePlayerInput();
        }
    }
}