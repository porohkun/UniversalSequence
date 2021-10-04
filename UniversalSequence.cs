using System;
using System.Collections;
using UnityEngine;

namespace UnSec
{
    public class UniversalSequence : MonoBehaviour
    {
        [SerializeField] private bool _showActionsInGameObject;
        [SerializeField] private SequenceAction[] _actionList;

        public SequenceAction CurrentAction { get; private set; }

        Coroutine _runCoroutine;
        Action _runCallback;

        public void Run(Action callback = null)
        {
            _runCallback?.Invoke();
            StopAllCoroutines();
            _runCoroutine = StartCoroutine(RunRoutine(callback));
        }

        private IEnumerator RunRoutine(Action callback)
        {
            _runCallback = callback;

            foreach (var action in _actionList)
                if (action != null)
                {
                    CurrentAction = action;
                    yield return StartCoroutine(action.Run());
                }
            CurrentAction = null;
            _runCallback = null;
            _runCoroutine = null;
            callback?.Invoke();
        }
    }
}
