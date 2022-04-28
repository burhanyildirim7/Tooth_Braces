using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisTelim
{
    public class Asama6 : MonoBehaviour
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        [Header("Controllerler")]
        private AsamaControl _asamaControl;

        [Header("DisTeli")]
        private GameObject _disTeli;
        

        [Header("Efektler")]
        private ParticleSystem _telOlusmaEfekt;

        [Header("PositionAndRotationSettings")]
        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        public Asama6()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();

            _telOlusmaEfekt = playerControl.telOlusmaEfekt;

            _disTeli = playerControl.disTeli;

            _startingPosition = _disTeli.transform.position;
            _startingRotation = _disTeli.transform.rotation;
        }

        public void MoveDisTeli()
        {
            _disTeli.transform.position = _hit.point;
            _disTeli.transform.rotation = Quaternion.Euler(Vector3.right * -5 + Vector3.up * -10 * (Mathf.Pow(Mathf.Abs(_hit.point.x), 3) / _hit.point.x) + Vector3.forward * 90);
        }

        public void CreateDisTeli()
        {
            Tooth tooth = _hit.transform.gameObject.GetComponent<Tooth>();
            if (!_hit.transform.GetChild(2).transform.gameObject.activeSelf && PlayerPrefs.GetInt("level") != 0)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                _hit.transform.GetChild(2).transform.gameObject.SetActive(true);
                _asamaControl.AddTel();
                _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
                Instantiate(_telOlusmaEfekt, _hit.point + Vector3.forward * -.25f + Vector3.up * .05f, Quaternion.identity);
            }
            else if (!_hit.transform.GetChild(2).transform.gameObject.activeSelf && PlayerPrefs.GetInt("level") == 0 && !tooth.willWearDisYayi)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                _hit.transform.GetChild(2).transform.gameObject.SetActive(true);
                _asamaControl.AddTel();
                _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
                Instantiate(_telOlusmaEfekt, _hit.point + Vector3.forward * -.25f + Vector3.up * .05f, Quaternion.identity);
                tooth.AnimasyonuDurdur();
            }
        }

        public void Deactive()
        {
            StartingPositionAndRotation();
        }

        void StartingPositionAndRotation()
        {
            while (Vector3.Distance(_disTeli.transform.position, _startingPosition) >= .1f)
            {
                _disTeli.transform.position = Vector3.Lerp(_disTeli.transform.position, _startingPosition, Time.deltaTime * 15);
                _disTeli.transform.rotation = Quaternion.Slerp(_disTeli.transform.rotation, _startingRotation, Time.deltaTime * 500);
            }
        }
    }
}

