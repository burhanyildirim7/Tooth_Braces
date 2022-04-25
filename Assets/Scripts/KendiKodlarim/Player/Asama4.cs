using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisYapiskan
{
    public class Asama4
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;
        private RaycastHit hitCollider;

        [Header("YapistiriciIslemleri")]
        private GameObject _sticky;

        [Header("PositionAndRotationSettings")]
        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        int layerMask;

        public Asama4()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _sticky = playerControl.sticky;

            _startingPosition = _sticky.transform.position;
            _startingRotation = _sticky.transform.rotation;

            layerMask = 1 << 9;
            layerMask = ~layerMask;
        }


        public void ActiveSticky()
        {
            _sticky.SetActive(true);
        }

        public void MoveSticky()
        {
            if (_sticky.activeSelf)
            {
                OnlyMoveSticky();
                if (Physics.Raycast(_sticky.transform.position, _sticky.transform.TransformDirection(Vector3.up), out hitCollider, 40, layerMask))
                {
                    if (hitCollider.transform.gameObject.CompareTag("Tooth"))
                    {
                        BiggerSticky();
                    }
                }
            }
            else
            {
                ActiveSticky();
            }
        }

        public void OnlyMoveSticky()
        {
            _sticky.transform.position = Vector3.Lerp(_sticky.transform.position, _hit.point + Vector3.forward * 0f, Time.deltaTime * 10);
            _sticky.transform.rotation = Quaternion.Euler(Vector3.right * -30 + Vector3.up * -7 * (Mathf.Pow(Mathf.Abs(_hit.point.x), 3) / _hit.point.x) + Vector3.forward * 70 + Vector3.up * 100);
        }


        public void BiggerSticky()
        {
            if (hitCollider.transform.GetChild(0).transform.gameObject.activeSelf && hitCollider.transform.GetChild(0).transform.localScale.y * 1000 <= 1)
            {
                hitCollider.transform.GetChild(0).transform.localScale += Vector3.one * .00008f;
            }
            else
            {
                hitCollider.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
        }


        public void DeactiveSticky()
        {
            StartingPositionAndRotation();
        }


        void StartingPositionAndRotation()
        {
            while (Vector3.Distance(_sticky.transform.position, _startingPosition) >= .1f)
            {
                _sticky.transform.position = Vector3.Lerp(_sticky.transform.position, _startingPosition, Time.deltaTime * 15);
                _sticky.transform.rotation = Quaternion.Slerp(_sticky.transform.rotation, _startingRotation, Time.deltaTime * 500);
            }
        }
    }
}

