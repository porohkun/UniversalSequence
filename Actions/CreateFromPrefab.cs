using System.Collections;
using UnityEngine;

namespace UnSec
{
    [SequenceActionInfo("Create from prefab", "Create From Prefab")]
    public class CreateFromPrefab : SequenceAction
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Component _prefab;
        [SerializeField] private Transform _positionSource;

        public override IEnumerator Run()
        {
            if (_prefab != null)
            {
                var result = Instantiate(_prefab, _target);
                if (_positionSource != null)
                {
                    var posRT = _positionSource as RectTransform;
                    var resRT = result.transform as RectTransform;
                    if (posRT != null && resRT != null)
                        resRT.anchoredPosition = posRT.anchoredPosition;
                    else
                        result.transform.position = _positionSource.position;
                }
            }
            yield break;
        }

        public override string ListDescription => $"{_prefab} | {NameOf(_target)}";
    }
}
