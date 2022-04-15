using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CeneBehaviour
{
    public class CeneHaraket
    {

        [Header("HedefRot")]
        private float _hedefRot;

        Transform _transform;

        public CeneHaraket(Transform transform, float hedefRot)
        {
            _transform = transform;
            _hedefRot = hedefRot;
        }


        public void OpenMouth()
        {
            if (Mathf.Abs(_transform.rotation.eulerAngles.x - _hedefRot) >= 1f)
            {
                _transform.DORotate(Vector3.right * _hedefRot, 1, RotateMode.Fast);
            }
        }

        public void CloseMouth()
        {
            if (Mathf.Abs(_transform.rotation.eulerAngles.x - 0) >= 1f)
            {
                _transform.DORotate(Vector3.right * 0, 1, RotateMode.Fast);
            }
        }
    }

}
