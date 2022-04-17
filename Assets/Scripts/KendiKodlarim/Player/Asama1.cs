using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisFircasi
{
    public class Asama1
    {
        public Vector3 _touchPosition { get; set; }
        public RaycastHit _hit;


        private GameObject _disFircasi;
        private ParticleSystem _bubbleEffect;

        private Transform _pointToothBrush;


        public Asama1()
        {
            _disFircasi = GameObject.FindObjectOfType<PlayerControl>().disFircasi;
            _bubbleEffect = GameObject.FindObjectOfType<PlayerControl>().bubbleEffect;

            _pointToothBrush = _disFircasi.transform.GetChild(0).transform.GetChild(0).transform;
        }

        public void ActiveToothBrush()
        {
            _disFircasi.transform.position = _hit.point;
            _disFircasi.SetActive(true);

            var emission = _bubbleEffect.emission;
            emission.rateOverTime = 20;


        }

        public void MoveToothBrush()
        {
            if (_disFircasi.activeSelf)
            {
                _disFircasi.transform.position = Vector3.Lerp(_disFircasi.transform.position, _hit.point, Time.deltaTime * 25);
                _disFircasi.transform.rotation = Quaternion.Euler(Vector3.up * -(Mathf.Abs(Mathf.Pow(_hit.point.x * 4, 1)) * Mathf.Pow(_hit.point.x * 4, 1)));
                _bubbleEffect.transform.position = _pointToothBrush.transform.position;
            }
            else
            {
                
                ActiveToothBrush();
            }
        }

        public void DeactiveToothBrush()
        {
            _disFircasi.SetActive(false);

            var emission = _bubbleEffect.emission;
            emission.rateOverTime = 0;
        }
    }

}
