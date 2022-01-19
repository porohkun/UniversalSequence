using System.Collections;
using UnityEngine;

namespace UnSec
{
    [SequenceActionInfo("Delay", "Delay")]
    public class Delay : SequenceAction
    {
        [SerializeField] private float _seconds;

        public override IEnumerator Run()
        {
            if (_seconds > 0)
                yield return new WaitForSeconds(_seconds);
        }

        public override string ListDescription => _seconds.ToString();
    }
}
