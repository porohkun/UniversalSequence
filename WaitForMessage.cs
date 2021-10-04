using System.Collections;
using UnityEngine;

namespace UnSec
{
    [SequenceActionInfo("Wait For Message", "Wait For Message")]
    public class WaitForMessage : SequenceAction
    {
        [SerializeField] private MessageReceiver _target;
        [SerializeField] private string _message;

        private bool _messageReceived;

        public override IEnumerator Run()
        {
            if (_target != null)
            {
                _messageReceived = false;
                _target.MessageReceived += _target_MessageReceived;
                while (!_messageReceived)
                    yield return null;
            }
        }

        private void _target_MessageReceived(string message)
        {
            if (message == _message)
            {
                _messageReceived = true;
                _target.MessageReceived -= _target_MessageReceived;
            }
        }

        public override string ListDescription => $"{_message} | {NameOf(_target)}";
    }
}
