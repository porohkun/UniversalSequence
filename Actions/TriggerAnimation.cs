using System.Collections;
using UnityEngine;

namespace UnSec
{
    [SequenceActionInfo("Trigger animation", "Trigger Animation")]
    public class TriggerAnimation : SequenceAction
    {
        [SerializeField] private Animator _target;
        [SerializeField] private string _trigger;
        [SerializeField] private float _callbackDelay;

        public override IEnumerator Run()
        {
            if (_target != null)
                _target.SetTrigger(_trigger);
            if (_callbackDelay > 0)
                yield return new WaitForSeconds(_callbackDelay);
        }

        public override string ListDescription => $"{_trigger} | {NameOf(_target)}";
    }
}
