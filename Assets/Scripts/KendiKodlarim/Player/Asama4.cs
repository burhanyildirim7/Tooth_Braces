using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisYapiskan
{
    public class Asama4
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        [Header("YapistiriciIslemleri")]
        private GameObject _sticky;

        public Asama4()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _sticky = playerControl.sticky;
        }


        public void ActiveSticky()
        {
            _sticky.SetActive(true);
        }

        public void MoveSticky()
        {
            if (_sticky.activeSelf)
            {
                _sticky.transform.position = Vector3.Lerp(_sticky.transform.position, _hit.point + Vector3.forward * -.25f + Vector3.right * .25f * (Mathf.Abs(_hit.point.x) / _hit.point.x), Time.deltaTime * 10);
                _sticky.transform.rotation = Quaternion.Euler(Vector3.right * 60 + Vector3.up * -60 * (Mathf.Abs(_hit.point.x) / _hit.point.x));
            }
            else
            {
                ActiveSticky();
            }
        }

        public void OnlyMoveSticky()
        {
            _sticky.transform.position = Vector3.Lerp(_sticky.transform.position, _hit.point - Vector3.forward * .65f, Time.deltaTime * 10);
        }


        public void BiggerSticky()
        {
            if (_hit.transform.GetChild(0).transform.gameObject.activeSelf && _hit.transform.GetChild(0).transform.localScale.y * 1000 <= 1)
            {
                _hit.transform.GetChild(0).transform.localScale += Vector3.one * .00006f;
            }
            else
            {
                _hit.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
        }


        public void DeactiveSticky()
        {
            _sticky.SetActive(false);
        }
    }
}

