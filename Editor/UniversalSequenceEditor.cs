#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UnSec
{
    [CustomEditor(typeof(UniversalSequence))]
    public class UniversalSequenceEditor : Editor
    {
        private SerializedProperty _showActionsInGameObjectProperty;
        private SerializedProperty _actionListProperty;

        private ReorderableList _actionList;
        private Type[] _actionTypes;

        private UnityEngine.Object _selectedAction;
        private Editor _selectedActionEditor;
        private UniversalSequence _sequence;

        HideFlags _hideMode => _showActionsInGameObjectProperty.boolValue ? HideFlags.None : HideFlags.HideInInspector;

        protected virtual void OnEnable()
        {
            _sequence = serializedObject.targetObject as UniversalSequence;

            var asse = AppDomain.CurrentDomain.GetAssemblies();
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();

            _actionTypes = types.Where(t => !t.IsAbstract && typeof(SequenceAction).IsAssignableFrom(t)).OrderBy(t => t.Name).ToArray();

            _showActionsInGameObjectProperty = serializedObject.FindProperty("_showActionsInGameObject");
            _actionListProperty = serializedObject.FindProperty("_actionList");

            _actionList = new ReorderableList(serializedObject, _actionListProperty, true, true, true, true);
            _actionList.drawHeaderCallback = DrawActionsHeader;
            _actionList.drawElementCallback = DrawActionListElement;
            _actionList.onSelectCallback = SelectActionCallback;
            _actionList.onAddDropdownCallback = AddActionDropdownCallback;
            _actionList.onRemoveCallback = RemoveActionCallback;

            foreach (var action in _sequence.GetComponents<SequenceAction>())
                action.hideFlags = _hideMode;
        }

        private void DrawActionsHeader(Rect rect)
        {
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width - 100, rect.height), "Actions");
            GUI.enabled = Application.isPlaying;
            GUI.Button(new Rect(rect.x + rect.width - 100, rect.y, 100, rect.height), "Launch");
            GUI.enabled = true;
        }

        private void DrawActionListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var level = _actionListProperty.GetArrayElementAtIndex(index);
            var content = "Empty";
            if (level.objectReferenceValue != null)
            {
                var action = level.objectReferenceValue as SequenceAction;
                content = $"{action.GetType().Name} | {action.ListDescription}";
                if (action == _sequence.CurrentAction)
                    content = $"> {content}";
            }
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, rect.height), content);
        }

        private void SelectActionCallback(ReorderableList list)
        {
            _selectedAction = _actionListProperty.GetArrayElementAtIndex(list.index).objectReferenceValue;
            _selectedActionEditor = Editor.CreateEditor(_selectedAction);
        }

        private void AddActionDropdownCallback(Rect buttonRect, ReorderableList list)
        {
            var menu = new GenericMenu();
            foreach (var type in _actionTypes)
            {
                var tt = type;
                var attr = type.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(SequenceActionInfoAttribute));
                if (attr == null)
                    throw new Exception($"type {tt} must have SequenceActionInfo attribute");
                var attrib = (SequenceActionInfoAttribute)attr.Constructor.Invoke(attr.ConstructorArguments.Select(a => a.Value).ToArray());
                menu.AddItem(new GUIContent(attrib.ActionName, attrib.HelpText), false, () =>
                {
                    var action = _sequence.gameObject.AddComponent(tt);
                    action.hideFlags = _hideMode;

                    _actionListProperty.arraySize++;
                    var element = _actionListProperty.GetArrayElementAtIndex(_actionListProperty.arraySize - 1);
                    element.objectReferenceValue = action;

                    serializedObject.ApplyModifiedProperties();
                });
            }
            menu.ShowAsContext();
        }

        private void RemoveActionCallback(ReorderableList list)
        {
            if (_selectedAction != null)
            {
                DestroyImmediate(_selectedAction);
                _selectedActionEditor = null;
                _actionListProperty.DeleteArrayElementAtIndex(list.index);
            }
            _actionListProperty.DeleteArrayElementAtIndex(list.index);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var show = EditorGUILayout.Toggle("Show Actions In GameObject", _showActionsInGameObjectProperty.boolValue);
            if (show != _showActionsInGameObjectProperty.boolValue)
            {
                _showActionsInGameObjectProperty.boolValue = show;
                foreach (var action in _sequence.GetComponents<SequenceAction>())
                    action.hideFlags = _hideMode;
            }
            _actionList.DoLayoutList();

            _selectedActionEditor?.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif