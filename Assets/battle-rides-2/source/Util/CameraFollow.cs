using UnityEngine;
using System.Collections;
using LuftSchloss.Core;

namespace Luderia.BattleRides2 {
    public class CameraFollow : LuftMonobehaviour {    
        public float FollowVelocity;

        private Transform _target;
        private Vector3 _distanceOffset;

        public void SetTarget(Transform target) {
            _target = target;
            _distanceOffset = _target.position - transform.position;
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (_target == null) return;

            var fakePosition = transform.position + _distanceOffset;
            var targetDirection = (_target.transform.position - fakePosition);

            float interpVelocity = targetDirection.magnitude * FollowVelocity;

            var targetPos = fakePosition + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos - _distanceOffset, 0.25f);
        }
    }
}