using System;
using System.Collections;
using UnityEngine;

namespace UnSec
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SequenceActionInfoAttribute : Attribute
    {
        public string ActionName { get; set; }
        public string HelpText { get; set; }

        public SequenceActionInfoAttribute(string actionName, string helpText)
        {
            this.ActionName = actionName;
            this.HelpText = helpText;
        }

    }

    public abstract class SequenceAction : MonoBehaviour
    {
        public abstract IEnumerator Run();

        public virtual string ListDescription => string.Empty;

        protected string NameOf(UnityEngine.Object obj)
        {
            if (obj == null)
                return "null";
            return obj.name;
        }
    }
}
