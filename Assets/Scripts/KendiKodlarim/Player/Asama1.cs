using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisFircasi
{
    public class Asama1 : MonoBehaviour
    {
        public Vector3 _touchPosition { get; set; }
        public RaycastHit _hit;


        private GameObject _disFircasi;
        private ParticleSystem _bubbleEffect;
        private int _numberStartingEffect;

        private Transform _pointToothBrush;


        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        private bool isEffectActive;


        public Asama1()
        {
            _disFircasi = GameObject.FindObjectOfType<PlayerControl>().disFircasi;
            _bubbleEffect = GameObject.FindObjectOfType<PlayerControl>().bubbleEffect;

            _pointToothBrush = _disFircasi.transform.GetChild(0).transform.GetChild(0).transform;


            _startingPosition = _disFircasi.transform.position;
            _startingRotation = _disFircasi.transform.rotation;


            isEffectActive = false;
        }

        public void ActiveToothBrush()
        {
            _disFircasi.transform.position = _hit.point;
            _disFircasi.SetActive(true);
        }

        public void ActiveToothBrushEffect()
        {
            var emission = _bubbleEffect.emission;
            emission.rateOverTime = 25;
            isEffectActive = true;
        }

        public void DeactiveToothBrushEffect()
        {
            var emission = _bubbleEffect.emission;
            emission.rateOverTime = 0;
            isEffectActive = false;
        }





        //Haraket ettirme islemleri icin
        public void MoveToothBrush()
        {
            if (_disFircasi.activeSelf && _bubbleEffect.transform.gameObject.activeSelf && isEffectActive)
            {
                _disFircasi.transform.position = Vector3.Lerp(_disFircasi.transform.position, _hit.point + Vector3.forward * .25f, Time.deltaTime * 25);
                _disFircasi.transform.rotation = Quaternion.Euler(Vector3.up * -(Mathf.Abs(Mathf.Pow(_hit.point.x * 6, 1)) * Mathf.Pow(_hit.point.x * 4, 1))) * Quaternion.Euler(Vector3.forward * -20 * (Mathf.Abs(_hit.point.x) / _hit.point.x) * (Mathf.Abs(_hit.point.y) / _hit.point.y));
                _bubbleEffect.transform.position = _hit.point;
               
            }
            else
            {
                ActiveToothBrush();
                ActiveToothBrushEffect();
            }
        }

        public void OnlyMoveToothBrush()
        {
            if (_disFircasi.activeSelf && !isEffectActive)
            {
                _disFircasi.transform.position = Vector3.Lerp(_disFircasi.transform.position, _hit.point, Time.deltaTime * 25);
                _disFircasi.transform.rotation = Quaternion.Euler(Vector3.up * -20 + Vector3.right * -20);
            }
            else
            {
                DeactiveToothBrushEffect();
                ActiveToothBrush();
            }
        }



        public void DeactiveToothBrush()
        {
            isEffectActive = false;
            StartingPositionAndRotation();

            var emission = _bubbleEffect.emission;
            emission.rateOverTime = 0;
        }

        void StartingPositionAndRotation()
        {
            while (Vector3.Distance(_disFircasi.transform.position, _startingPosition) >= .1f)
            {
                _disFircasi.transform.position = Vector3.Lerp(_disFircasi.transform.position, _startingPosition, Time.deltaTime * 15);
                _disFircasi.transform.rotation = Quaternion.Slerp(_disFircasi.transform.rotation, _startingRotation, Time.deltaTime * 500);
            }
        }
    }
}
