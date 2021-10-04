using System;
using System.Collections;
using UnityEngine;

namespace UnSec
{
    public class MessageAfterStart : StateMachineBehaviour
    {
        [SerializeField] private string _message;
        [SerializeField] private float _delay;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var receiver = animator.GetComponent<MessageReceiver>();
            if (receiver != null)
            {
                receiver.StartCoroutine(WaitRoutine(_delay, () => receiver.Receive(_message)));
            }
        }

        public static IEnumerator WaitRoutine(float delay, Action callback)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    }
}