using System.Collections;
using UnityEngine;

namespace UnSec
{
    [SequenceActionInfo("Enable GameObject", "Enable GameObject")]
    public class EnableGameObject : SequenceAction
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _enable;

        public override IEnumerator Run()
        {
            if (_target != null)
                _target.SetActive(_enable);
            yield break;
        }

        public override string ListDescription => $"{(_enable ? "on" : "off")} | {NameOf(_target)}";
    }
}
