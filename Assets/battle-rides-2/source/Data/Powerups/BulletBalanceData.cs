using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Powerups/Bullet Balance Data", fileName = "BulletBalanceData")]
    public class BulletBalanceData : ScriptableObject {
        public float Speed = 25f;
        public float ShotsInterval = 0.05f;
        public float Damage = 1f;
        public int BulletPairsFired = 20;
        public float DistanceBetweenBullets = 0.5f;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BulletBalanceData))]
    public class BulletBalanceDataEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var data = target as BulletBalanceData;

            EditorGUILayout.Space();
            GUILayout.Label("Calculated Fields", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Total bullet damage:", (data.Damage * data.BulletPairsFired * 2f).ToString("0.##"));
            EditorGUILayout.LabelField("Bullet burst duration:", (data.ShotsInterval * data.BulletPairsFired).ToString("0.##") + "s");
        }
    }
#endif
}
