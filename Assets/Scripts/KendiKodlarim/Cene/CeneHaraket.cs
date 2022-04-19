using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CeneBehaviour
{
    public class CeneHaraket
    {

        [Header("HedefRot")]
        private Vector3 _hedefRot;

        Transform _transform;

        public CeneHaraket(Transform transform, Vector3 hedefRot)
        {
            _transform = transform;
            _hedefRot = hedefRot;
        }


        public void OpenMouth()
        {
            if (Quaternion.Angle(_transform.rotation, Quaternion.Euler(_hedefRot)) >= 1)
            {
                _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(_hedefRot), Time.deltaTime * 3);
            }
        }

        public void CloseMouth()
        {
            if (Quaternion.Angle(_transform.rotation, Quaternion.Euler(_hedefRot)) >= 1)
            {
                _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(_hedefRot), Time.deltaTime * 3);
            }
        }
    }

}
