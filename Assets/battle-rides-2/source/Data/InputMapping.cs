using Luderia.BattleRides2.InputHandling;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Luderia.BattleRides2.Data {

    [System.Serializable]
    public class KeyMapping {
        public KeyCode Key;
        public InputAction Action;
    }

    [CreateAssetMenu(fileName = "InputMapping", menuName = "BattleRides/Input Mapping")]
    public class InputMapping : ScriptableObject {
        public List<KeyMapping> P1KeyboardMapping;
        public List<KeyMapping> P2KeyboardMapping;


    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(InputMapping))]
    public class InputMappingEditor : Editor {
        private ReorderableList _p1keyList;
        private ReorderableList _p2keyList;

        private void OnEnable() {
            // Player 1
            _p1keyList = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("P1KeyboardMapping"),
                    true, true, true, true);

            _p1keyList.drawHeaderCallback = rect => DrawKeyListHeader(rect, "Player 1");
            _p1keyList.drawElementCallback = 
                (rect, index, isActive, isFocused) => DrawKeyListElement(_p1keyList, rect, index, isActive, isFocused);

            // Player 2
            _p2keyList = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("P2KeyboardMapping"),
                    true, true, true, true);

            _p2keyList.drawHeaderCallback = rect => DrawKeyListHeader(rect, "Player 2");
            _p2keyList.drawElementCallback =
                (rect, index, isActive, isFocused) => DrawKeyListElement(_p2keyList, rect, index, isActive, isFocused);
        }

        private void DrawKeyListHeader(Rect rect, string player) {
            EditorGUI.LabelField(rect, "Keyboard Mappings: " + player);
        }

        private void DrawKeyListElement(ReorderableList list, Rect rect, int index, bool isActive, bool isFocused) {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width * 0.5f, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Action"), GUIContent.none);

            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width * 0.5f, rect.y, rect.width * 0.5f, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Key"), GUIContent.none);
        }

        public override void OnInspectorGUI() {
            GUI.enabled = false;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
            GUI.enabled = true;
            EditorGUILayout.Space();

            serializedObject.Update();
            _p1keyList.DoLayoutList();
            EditorGUILayout.Space();
            _p2keyList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
