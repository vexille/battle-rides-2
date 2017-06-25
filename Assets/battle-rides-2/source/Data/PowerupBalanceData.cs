using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Luderia.BattleRides2.Data {
    public enum PowerupType {
        None = 0,
        Missile,
        MachineGun,
        Shock,
        Landmine,
        Nitrous,
        Repair
    }

    [System.Serializable]
    public class PowerupWeight {
        public PowerupType Type;
        public int Weight = 1;
    }

    [CreateAssetMenu(menuName = "BattleRides/Powerup Balance Data", fileName = "PowerupBalanceData")]
    public class PowerupBalanceData : ScriptableObject {
        public List<PowerupWeight> PowerupWeights;

        public static int MaxPowerupSlots = 4;

        [Header("Powerup spawn config")]
        public GameObject PowerupDropPrefab;
        public float PowerupSpawnInterval = 2f;
        public int MaxActivePowerupPickups = 2;

        [Header("Balance data references")]
        public MissileBalanceData MissileData;
        public BulletBalanceData BulletData;
        public MineBalanceData MineData;
        public ShockBalanceData ShockData;
        public NitroBalanceData NitroData;
        public float RepairAmount = 25f;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PowerupBalanceData))]
    [CanEditMultipleObjects]
    public class PowerupBalanceDataEditor : Editor {
        private ReorderableList _list;

        private void OnEnable() {
            _list = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("PowerupWeights"),
                    true, true, true, true);

            _list.drawHeaderCallback = (Rect rect) => {
                GUI.Label(new Rect(rect.x + 10, rect.y, rect.width * 0.6f - 10, rect.height), "Powerup");
                GUI.Label(new Rect(rect.width * 0.6f, rect.y, rect.width * 0.4f, rect.height), "Weights");
            };

            _list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);

                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, rect.width * 0.5f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Type"), GUIContent.none);

                EditorGUI.PropertyField(
                    new Rect(rect.width * 0.6f + 10, rect.y, rect.width * 0.4f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Weight"), GUIContent.none);
            };
        }

        public override void OnInspectorGUI() {
            GUI.enabled = false;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
            GUI.enabled = true;
            EditorGUILayout.Space();

            serializedObject.Update();
            GUILayout.Label("Powerup Weights", EditorStyles.boldLabel);
            _list.DoLayoutList();
            EditorGUILayout.Space();
            DrawPropertiesExcluding(serializedObject, "m_Script", "PowerupWeights");

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
