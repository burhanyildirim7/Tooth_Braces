using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisKerpeten
{
    public class Asama8
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;
        public RaycastHit hitCollider;

        [Header("Controllerler")]
        private AsamaControl _asamaControl;

        [Header("DisKerpeten")]
        private GameObject _disKerpeten;

        [Header("PositionAndRotationSettings")]
        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        int layerMask;

        public Asama8()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();

            _disKerpeten = playerControl.disKerpeten;

            _startingPosition = _disKerpeten.transform.position;
            _startingRotation = _disKerpeten.transform.rotation;

            layerMask = 1 << 9;
            layerMask = ~layerMask;
        }

        public void MoveDisKerpeten()
        {
            _disKerpeten.transform.position = _hit.point + Vector3.forward * -.0f;
            _disKerpeten.transform.rotation = Quaternion.Euler(Vector3.right * -10 + Vector3.up * -4 * (Mathf.Pow(Mathf.Abs(_hit.point.x), 3) / _hit.point.x) + Vector3.forward * 70);

            if (Physics.Raycast(_disKerpeten.transform.position, _disKerpeten.transform.TransformDirection(Vector3.forward), out hitCollider, 40, layerMask))
            {
                //Debug.DrawRay(_disKerpeten.transform.position, _disKerpeten.transform.TransformDirection(Vector3.forward) * 50, Color.red);
                if (hitCollider.transform.gameObject.CompareTag("Tooth"))
                {
                    DestroyDisTeli();
                }
            }
        }


        public void DestroyDisTeli()
        {
            if (hitCollider.transform.GetChild(2).transform.gameObject.activeSelf)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                hitCollider.transform.GetChild(2).transform.gameObject.SetActive(false);
                _asamaControl.DeleteTell();
                hitCollider.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
            }
        }


        public void Deactive()
        {
            StartingPositionAndRotation();
        }

        void StartingPositionAndRotation()
        {
            while (Vector3.Distance(_disKerpeten.transform.position, _startingPosition) >= .1f)
            {
                _disKerpeten.transform.position = Vector3.Lerp(_disKerpeten.transform.position, _startingPosition, Time.deltaTime * 15);
                _disKerpeten.transform.rotation = Quaternion.Slerp(_disKerpeten.transform.rotation, _startingRotation, Time.deltaTime * 500);
            }
        }
    }

}
