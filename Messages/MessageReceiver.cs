using System;
using UnityEngine;

namespace UnSec
{
    public class MessageReceiver : MonoBehaviour
    {
        public event Action<string> MessageReceived;
        public event Action<float> LengthReceived;

        public void Receive(string message)
        {
            MessageReceived?.Invoke(message);
        }

        public void Receive(float length)
        {
            LengthReceived?.Invoke(length);
        }
    }
}
