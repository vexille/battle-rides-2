using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Luderia.BattleRides2.Data {
    [System.Serializable]
    public class CarData {
        public GameObject Prefab;
        public CarBalance Data;
    }

    [CreateAssetMenu(menuName = "BattleRides/Car Data List", fileName = "CarDataList")]
    public class CarDataList : ScriptableObject {
        public List<CarData> CarList;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CarDataList))]
    public class CarDataListEditor : Editor {
        private ReorderableList list;

        private void OnEnable() {
            list = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("CarList"),
                    true, true, true, true);

            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Car Data List");
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;

                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, rect.width * 0.5f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Prefab"), GUIContent.none);

                EditorGUI.PropertyField(
                    new Rect(rect.x + rect.width * 0.5f, rect.y, rect.width * 0.5f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Data"), GUIContent.none);
            };
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
