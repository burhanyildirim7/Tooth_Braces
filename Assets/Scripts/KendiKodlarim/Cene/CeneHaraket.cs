using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CeneBehaviour
{
    public class CeneHaraket
    {

        [Header("HedefRot")]
        private float _hedefRot;

        Transform _transform;

        public CeneHaraket(Transform transform,float hedefRot)
        {
            _transform = transform;
            _hedefRot = hedefRot;
        }


        public void OpenMouth()
        {
            while (Mathf.Abs(_transform.rotation.eulerAngles.x - _hedefRot) >= 1f)
            {
                _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(Vector3.right * _hedefRot), Time.deltaTime * 20);
                yield return null;
            }
        }

        public void CloseMouth()
        {
            while (Mathf.Abs(_transform.rotation.eulerAngles.x - 0) >= 1f)
            {
                _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(Vector3.right * 0), Time.deltaTime * 20);
                yield return null;
            }

        }

        


    }

}
