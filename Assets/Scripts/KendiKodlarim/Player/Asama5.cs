using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisBraket
{
    public class Asama5
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        [Header("AsamaIslemleri")]
        private AsamaControl asamaControl;

        private GameObject _braket;

        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        public Asama5()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            asamaControl = GameObject.FindObjectOfType<AsamaControl>();

            _braket = playerControl.braket;

            _startingPosition = _braket.transform.position;
            _startingRotation = _braket.transform.rotation;
        }


        public void MoveBraket()
        {
            _braket.transform.position = _hit.point - Vector3.forward * .35f - Vector3.up * .1f;
            _braket.transform.rotation = Quaternion.Slerp(_braket.transform.rotation, Quaternion.Euler(Vector3.up * -(Mathf.Abs(Mathf.Pow(_hit.point.x * 4, 1)) * Mathf.Pow(_hit.point.x * 3, 1))) * Quaternion.Euler(Vector3.forward * -12 * (Mathf.Abs(_hit.point.x) / _hit.point.x) * (Mathf.Abs(_hit.point.y) / _hit.point.y)), Time.deltaTime * 50);
        }


        public void CreateBraket()
        {
            if (!_hit.transform.GetChild(1).transform.gameObject.activeSelf && _hit.transform.GetChild(0).transform.gameObject.activeSelf)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                _hit.transform.GetChild(1).transform.gameObject.SetActive(true);
                _hit.transform.GetChild(0).transform.gameObject.SetActive(false);

                asamaControl.Stage7Invoke();
            }
        }

        public void DeactiveBraket()
        {
            StartingPositionAndRotation();
        }

        void StartingPositionAndRotation()
        {
            _braket.transform.position = _startingPosition;
            _braket.transform.rotation = _startingRotation;
        }
    }

}
