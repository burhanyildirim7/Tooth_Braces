using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisSuCekme;
using PlayerBehaviour;

namespace DisSulama
{
    public class Asama2
    {
        public Vector3 _touchPosition { get; set; }
        public RaycastHit _hit;

        private RaycastHit hitCollider;

        public GameObject _waterEffect { get; set; }
        public GameObject _waterObj;
        public GameObject _waterCollider;

        private PlayerControl _playerControl;

        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        private Asama3 _asama3;

        int layerMask;

        public Asama2()
        {
            _playerControl = GameObject.FindObjectOfType<PlayerControl>();
            //_asama3 = _playerControl.playerTouch.asama3;

            _waterEffect = _playerControl.waterEffect;
            _waterObj = _playerControl.waterObj;
            _waterCollider = _playerControl.waterCollier;

            _startingPosition = _waterObj.transform.position;
            _startingRotation = _waterObj.transform.rotation;

            layerMask = 1 << 4 | 1 << 9;
            layerMask = ~layerMask;
        }

        public void ActiveWaterObject()
        {
            _waterObj.transform.rotation = Quaternion.LookRotation(_hit.point - _waterObj.transform.position);
            _waterCollider.transform.position = _hit.point;

            _waterObj.transform.position = _hit.transform.position - Vector3.forward * 3 + Vector3.right * 1;


            _waterCollider.SetActive(true);
        }

      

        public void WaterCollider()
        {
            if (Physics.Raycast(_waterObj.transform.position, _waterObj.transform.TransformDirection(Vector3.forward), out hitCollider, 40, layerMask))
            {
                if(hitCollider.transform.gameObject.CompareTag("Tooth"))
                {
                    _waterCollider.transform.position = Vector3.Lerp(_waterCollider.transform.position, hitCollider.point, Time.deltaTime * 10);
                    //_asama3.IncreaseWaterAmount();

                    if (!_waterEffect.activeSelf)
                    {
                        ActiveWaterEffect();
                    }
                }
                else
                {
                    DeactiveWaterEffect();
                }

            }
        }

        public void MoveWater()
        {
            if (_waterCollider.activeSelf)
            {
                WaterCollider();
                _waterObj.transform.rotation = Quaternion.Euler(Vector3.right + Vector3.up * -6.1f  * (Mathf.Pow(Mathf.Abs(_hit.point.x), 3) / _hit.point.x) + Vector3.forward * 70);
                
                _waterObj.transform.position = Vector3.Lerp(_waterObj.transform.position, _hit.point, Time.deltaTime * 15);
            }
            else
            {
                ActiveWaterObject();
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
            }
        }

        public void ActiveWaterEffect()
        {
            _waterEffect.SetActive(true);

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
