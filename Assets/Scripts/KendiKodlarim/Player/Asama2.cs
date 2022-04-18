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
        public ParticleSystem _waterEffect { get; set; }
        public GameObject _waterObj;
        public GameObject _waterCollider;

        private PlayerControl _playerControl;

        public Asama2()
        {
            _playerControl = GameObject.FindObjectOfType<PlayerControl>();

            _waterEffect = _playerControl.waterEffect;
            _waterObj = _playerControl.waterObj;
            _waterCollider = _playerControl.waterCollier;

        }

        public void ActiveWater()
        {
            _waterObj.transform.rotation = Quaternion.LookRotation(_hit.point - _waterObj.transform.position);
            _waterCollider.transform.position = _hit.point;

            _waterObj.transform.position = _hit.transform.position - Vector3.forward * 3 + Vector3.right * 1;


            _waterCollider.SetActive(true);
            _waterObj.SetActive(true);
        }

        public void MoveWater()
        {
            if (_waterCollider.activeSelf && _waterCollider.activeSelf)
            {
                _waterCollider.transform.position = Vector3.Lerp(_waterCollider.transform.position, _hit.point, Time.deltaTime * 10);


                _waterObj.transform.rotation = Quaternion.LookRotation(_hit.point - _waterObj.transform.position);


                _waterObj.transform.position = Vector3.Lerp(_waterObj.transform.position, _hit.transform.position - Vector3.forward * 1.65f + Vector3.right * (Mathf.Abs(_hit.point.x) / _hit.point.x) + Vector3.up * (Mathf.Abs(_hit.point.y) / _hit.point.y), Time.deltaTime * 15);

            }
            else
            {
                ActiveWater();
            }
        }

        public void DeactiveWater()
        {
            _waterCollider.SetActive(false);
            _waterObj.SetActive(false);
        }

    }
}
