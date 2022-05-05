using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisSuCekme
{
    public class Asama3 : MonoBehaviour
    {

        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        private GameObject _waterSender;
        private GameObject _water;
        private float _amountWater;

        private bool suAlinmayaBaslandiMi;

        private Vector3 _startingPosition;
        private Quaternion _startingRotation;
        private Vector3 _startingLocalScale;

        [Header("TabaktaBulunanObjeIcinGereklidir")]
        private GameObject ornekTabaktakiObje;

        public Asama3()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();

            _water = playerControl.water;
            _waterSender = playerControl.waterSender;

            suAlinmayaBaslandiMi = false;


            _startingPosition = _waterSender.transform.position;
            _startingRotation = _waterSender.transform.rotation;
            _startingLocalScale = _waterSender.transform.localScale;
        }

        private void ActiveMoveSender()
        {

        }

        public void MoveWaterSender()
        {
            
            if (_waterSender.activeSelf)
            {
                _waterSender.transform.rotation = Quaternion.Euler(Vector3.up * -50 + Vector3.forward * 130);
                _waterSender.transform.position = Vector3.right * 1 + Vector3.up * -.5f + Vector3.forward * -2;
                ReduceWaterAmount();

                if (!suAlinmayaBaslandiMi)
                {
                    suAlinmayaBaslandiMi = true;
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                }
            }
            else
            {
                ActiveMoveSender();
            }
        }


        public void OnlyMoveWaterSender()
        {
            if (ornekTabaktakiObje == null)
            {
                _waterSender.transform.localScale = _startingLocalScale * 1.2f;
                ornekTabaktakiObje = Instantiate(_waterSender, _startingPosition, _startingRotation);
            }
            else if (Vector3.Distance(ornekTabaktakiObje.transform.localScale, _startingLocalScale * 1.4f) >= .1f)
            {
                ornekTabaktakiObje.transform.localScale = Vector3.Lerp(ornekTabaktakiObje.transform.localScale, _startingLocalScale * 1.5f, Time.deltaTime * 15);
            }

            if (_waterSender.activeSelf)
            {
                _waterSender.transform.rotation = Quaternion.LookRotation(_hit.point - _waterSender.transform.position) * Quaternion.Euler(Vector3.right * 150 - Vector3.up * 45 - Vector3.forward * 100);
                _waterSender.transform.position = Vector3.Lerp(_waterSender.transform.position, _hit.point + Vector3.up * 2 + Vector3.forward * 2 + Vector3.right * (Mathf.Abs(_hit.point.x) / _hit.point.x) + Vector3.up * (Mathf.Abs(_hit.point.y) / _hit.point.y), Time.deltaTime * 50);
            }
            else
            {
                ActiveMoveSender();
            }
        }

        public void ReduceWaterAmount()
        {
            _amountWater -= Time.deltaTime * 1.5f;

            if (_amountWater <= .2f && _water.activeSelf)
            {
                _amountWater = 0;
                _water.SetActive(false);
                GameObject.FindObjectOfType<AsamaControl>().Stage4Invoke();

                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            }
            else
            {
                UpdateWaterHeightAndWaight();
            }
        }

        public void IncreaseWaterAmount()
        {
            _amountWater += Time.deltaTime * .5f;

            if (_amountWater >= 1)
            {
                _amountWater = 1;
            }
            UpdateWaterHeightAndWaight();
        }


        //Yukselen su icin gereklidir
        public void UpdateWaterHeightAndWaight()
        {
            if (!_water.activeSelf && _amountWater >= .01f)
            {
                _water.SetActive(true);
            }
            _water.transform.localScale = Vector3.forward * 1 + Vector3.right * 110 + Vector3.up * 110;
            _water.transform.position = Vector3.up * (-.45f + .4f * (_amountWater)) - Vector3.forward * .61f;
        }


        public void DeactiveWaterSenderer()
        {
            Destroy(ornekTabaktakiObje);
            ornekTabaktakiObje = null;


            StartingPositionAndRotation();
            suAlinmayaBaslandiMi = false;
        }



        void StartingPositionAndRotation()
        {
            while (Vector3.Distance(_waterSender.transform.position, _startingPosition) >= .1f)
            {

                _waterSender.transform.localScale = _startingLocalScale;
                _waterSender.transform.position = Vector3.Lerp(_waterSender.transform.position, _startingPosition, Time.deltaTime * 15);
                _waterSender.transform.rotation = Quaternion.Slerp(_waterSender.transform.rotation, _startingRotation, Time.deltaTime * 500);
            }

    
        }
    }
}

