using UnityEngine;

namespace UnSec
{
    public class GrabLength : StateMachineBehaviour
    {
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var receiver = animator.GetComponent<MessageReceiver>();
            if (receiver != null)
                receiver.Receive(stateInfo.length);
        }

    }
}