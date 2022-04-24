using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisFircasi
{
    public class Asama1 : MonoBehaviour
    {
        public RaycastHit _hit;


        private GameObject _disFircasi;
        private ParticleSystem _bubbleEffect;
        private float _numberStartingEffect;
        Animation _disFircasiAnim;

        private Transform _pointToothBrush;


        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        private bool isEffectActive;


        public Asama1()
        {
            _disFircasi = GameObject.FindObjectOfType<PlayerControl>().disFircasi;
            _disFircasiAnim = _disFircasi.transform.GetChild(0).transform.gameObject.GetComponent<Animation>();
            _bubbleEffect = GameObject.FindObjectOfType<PlayerControl>().bubbleEffect;

            _pointToothBrush = _disFircasi.transform.GetChild(0).transform.GetChild(0).transform;


            _startingPosition = _disFircasi.transform.position;
            _startingRotation = _disFircasi.transform.rotation;


            isEffectActive = false;
        }

        public void ActiveToothBrush()
        {
            _disFircasi.transform.position = _hit.point;
            _disFircasiAnim.enabled = true;
        }

        public void ActiveToothBrushEffect()
        {
            var emission = _bubbleEffect.emission;
            emission.rateOverTime = 20;
            isEffectActive = true;
            _numberStartingEffect = 20;
        }

        private void IncreaseToothBrushEffect()
        {
            var emission = _bubbleEffect.emission;
            if (_numberStartingEffect <= 32)
            {
                _numberStartingEffect += 2 * Time.deltaTime;
                emission.rateOverTime = _numberStartingEffect;
            }

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
                IncreaseToothBrushEffect();

                _disFircasi.transform.position = Vector3.Lerp(_disFircasi.transform.position, _hit.point + Vector3.forward * .25f, Time.deltaTime * 100);
                //_disFircasi.transform.rotation = Quaternion.Euler(Vector3.up * -(Mathf.Abs(Mathf.Pow(_hit.point.x * 6, 1)) * Mathf.Pow(_hit.point.x * 4, 1))) * Quaternion.Euler(Vector3.forward * -12 * (Mathf.Abs(_hit.point.x) / _hit.point.x) * (Mathf.Abs(_hit.point.y) / _hit.point.y));
                _disFircasi.transform.rotation = Quaternion.Slerp(_disFircasi.transform.rotation, Quaternion.Euler(Vector3.up * -(Mathf.Abs(Mathf.Pow(_hit.point.x * 6, 1)) * Mathf.Pow(_hit.point.x * 4, 1))) * Quaternion.Euler(Vector3.forward * -12 * (Mathf.Abs(_hit.point.x) / _hit.point.x) * (Mathf.Abs(_hit.point.y) / _hit.point.y)), Time.deltaTime * 10);
                _bubbleEffect.transform.position = _hit.point + _disFircasi.transform.right * -.3f;

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

            _disFircasiAnim.enabled = false;
        }

        void StartingPositionAndRotation()
        {
            _disFircasi.transform.position = _startingPosition;
            _disFircasi.transform.rotation = _startingRotation;
        }
    }
}
