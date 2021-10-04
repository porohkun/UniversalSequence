using UnityEngine;

namespace UnSec
{
    public class MessageOnEnd : StateMachineBehaviour
    {
        [SerializeField] private string _message;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var receiver = animator.GetComponent<MessageReceiver>();
            if (receiver != null)
                receiver.Receive(_message);
        }

    }
}