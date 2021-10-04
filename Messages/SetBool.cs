using UnityEngine;

namespace UnSec
{
    public class SetBool : StateMachineBehaviour
    {
        [SerializeField] private string _parameter;
        [SerializeField] private bool _value;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(_parameter, _value);
        }

    }
}