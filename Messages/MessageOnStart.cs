using UnityEngine;

namespace UnSec
{
    public class MessageOnStart : StateMachineBehaviour
    {
        [SerializeField] private string _message;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var receiver = animator.GetComponent<MessageReceiver>();
            if (receiver != null)
                receiver.Receive(_message);
        }

    }
}