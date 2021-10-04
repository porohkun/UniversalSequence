using System;
using System.Collections;
using UnityEngine;

namespace UnSec
{
    public class MessageBeforeEnd : StateMachineBehaviour
    {
        [SerializeField] private string _message;
        [SerializeField] private float _advance;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var receiver = animator.GetComponent<MessageReceiver>();
            if (receiver != null)
            {
                var animationLength = stateInfo.length;
                receiver.StartCoroutine(WaitRoutine(animationLength - _advance / (stateInfo.speed * stateInfo.speedMultiplier), () => receiver.Receive(_message)));
            }
        }

        public static IEnumerator WaitRoutine(float delay, Action callback)
        {
            if (delay > 0)
                yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    }
}