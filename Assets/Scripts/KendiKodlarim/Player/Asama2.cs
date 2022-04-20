using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisSulama
{
    public class Asama2
    {
        public Vector3 _touchPosition { get; set; }
        public RaycastHit _hit;

        //  [Header("RelatedWater")]
        public GameObject _waterEffect { get; set; }
        public GameObject _waterObj;
        public GameObject _waterCollider;

        private PlayerControl _playerControl;

        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        public Asama2()
        {
            _playerControl = GameObject.FindObjectOfType<PlayerControl>();

            _waterEffect = _playerControl.waterEffect;
            _waterObj = _playerControl.waterObj;
            _waterCollider = _playerControl.waterCollier;

            _startingPosition = _waterObj.transform.position;
            _startingRotation = _waterObj.transform.rotation;

        }

        public void ActiveWaterObject()
        {
            _waterObj.transform.rotation = Quaternion.LookRotation(_hit.point - _waterObj.transform.position);
            _waterCollider.transform.position = _hit.point;

            _waterObj.transform.position = _hit.transform.position - Vector3.forward * 3 + Vector3.right * 1;


            _waterCollider.SetActive(true);
        }

        public void ActiveWaterEffect()
        {
            _waterEffect.SetActive(true);

        }


        public void MoveWater()
        {
            if (_waterCollider.activeSelf && _waterCollider.activeSelf && _waterEffect.activeSelf)
            {
                _waterCollider.transform.position = Vector3.Lerp(_waterCollider.transform.position, _hit.point, Time.deltaTime * 10);
                _waterObj.transform.rotation = Quaternion.LookRotation(_hit.point - _waterObj.transform.position);
                _waterObj.transform.position = Vector3.Lerp(_waterObj.transform.position, _hit.point - Vector3.forward * 1.65f + Vector3.right * (Mathf.Abs(_hit.point.x) / _hit.point.x) + Vector3.up * (Mathf.Abs(_hit.point.y) / _hit.point.y), Time.deltaTime * 15);
            }
            else
            {
                ActiveWaterObject();
                ActiveWaterEffect();
            }
        }

        public void OnlyMoveWater()
        {
            if (_waterObj.activeSelf && !_waterEffect.activeSelf)
            {
                _waterObj.transform.position = Vector3.Lerp(_waterObj.transform.position, _hit.point - Vector3.forward * 1.65f + Vector3.right * (Mathf.Abs(_hit.point.x) / _hit.point.x) + Vector3.up * (Mathf.Abs(_hit.point.y) / _hit.point.y), Time.deltaTime * 15);
            }
            else
            {
                ActiveWaterObject();
                DeactiveWaterEffect();
            }
        }

        public void DeactiveWaterEffect()
        {
            _waterEffect.SetActive(false);
        }

        public void DeactiveWater()
        {
            StartingPositionAndRotation();
            _waterCollider.SetActive(false);
            _waterEffect.SetActive(false);
        }



        void StartingPositionAndRotation()
        {
            while (Vector3.Distance(_waterObj.transform.position, _startingPosition) >= .1f)
            {
                _waterObj.transform.position = Vector3.Lerp(_waterObj.transform.position, _startingPosition, Time.deltaTime * 15);
                _waterObj.transform.rotation = Quaternion.Slerp(_waterObj.transform.rotation, _startingRotation, Time.deltaTime * 500);
            }
        }
    }
}
