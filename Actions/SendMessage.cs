using System.Collections;
using UnityEngine;

namespace UnSec
{
    [SequenceActionInfo("Send message", "Send Unity message to all GameObject scripts")]
    public class SendMessage : SequenceAction
    {
        [SerializeField] private GameObject _object;
        [SerializeField] private string _message;


        public override IEnumerator Run()
        {
            _object.SendMessage(_message);

            yield return null;
        }

        public override string ListDescription => $"{_message} | {NameOf(_object)}";
    }
}
