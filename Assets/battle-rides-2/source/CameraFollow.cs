using UnityEngine;
using System.Collections;

namespace Luderia.BattleRides2 {
    public class CameraFollow : MonoBehaviour {
        public Transform Target;
        public float FollowVelocity;

        private Vector3 _distanceOffset;
        private void Start() {
            _distanceOffset = Target.position - transform.position;
        }

        private void Update() {
            var fakePosition = transform.position + _distanceOffset;
            var targetDirection = (Target.transform.position - fakePosition);

            float interpVelocity = targetDirection.magnitude * FollowVelocity;

            var targetPos = fakePosition + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos - _distanceOffset, 0.25f);
        }
    }
}