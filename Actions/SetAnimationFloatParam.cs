using System.Collections;
using UnityEngine;

namespace UnSec
{
    [SequenceActionInfo("Set animation float param", "Animation Float Param")]
    public class SetAnimationFloatParam : SequenceAction
    {
        [SerializeField] private Animator _target;
        [SerializeField] private string _parameter;
        [SerializeField] private float _value;

        public override IEnumerator Run()
        {
            _target.SetFloat(_parameter, _value);
            yield break;
        }

        public override string ListDescription => $"{_parameter} | {_value} | {NameOf(_target)}";
    }
}
