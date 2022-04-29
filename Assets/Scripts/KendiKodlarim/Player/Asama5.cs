using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisBraket
{
    public class Asama5 : MonoBehaviour
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        [Header("AsamaIslemleri")]
        private AsamaControl asamaControl;

        private GameObject _braket;

        private Vector3 _startingPosition;
        private Quaternion _startingRotation;
        private Vector3 _startingLocalScale;

        [Header("TabaktaBulunanObjeIcinGereklidir")]
        private GameObject ornekTabaktakiObje;
        private Outline _outline;

        public Asama5()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            asamaControl = GameObject.FindObjectOfType<AsamaControl>();

            _braket = playerControl.braket;

            _startingPosition = _braket.transform.position;
            _startingRotation = _braket.transform.rotation;
            _startingLocalScale = _braket.transform.localScale;
        }


        public void MoveBraket()
        {
            if (ornekTabaktakiObje == null)
            {
                ornekTabaktakiObje = Instantiate(_braket, _startingPosition, _startingRotation);
                _outline = ornekTabaktakiObje.GetComponent<Outline>();
            }
            else if (Vector3.Distance(ornekTabaktakiObje.transform.localScale, _startingLocalScale * 1.4f) >= .1f)
            {
                ornekTabaktakiObje.transform.localScale = Vector3.Lerp(ornekTabaktakiObje.transform.localScale, _startingLocalScale * 1.5f, Time.deltaTime * 15);
            }
            else if (_outline.outlineWidth < 1)
            {
                _outline.outlineWidth = 10;
                _outline.UpdateMaterialProperties();
            }


            _braket.transform.position = _hit.point - Vector3.forward * .35f - Vector3.up * .1f;
            _braket.transform.rotation = Quaternion.Slerp(_braket.transform.rotation, Quaternion.Euler(Vector3.up * -(Mathf.Abs(Mathf.Pow(_hit.point.x * 4, 1)) * Mathf.Pow(_hit.point.x * 3, 1))) * Quaternion.Euler(Vector3.forward * -12 * (Mathf.Abs(_hit.point.x) / _hit.point.x) * (Mathf.Abs(_hit.point.y) / _hit.point.y)), Time.deltaTime * 50);
        }


        public void CreateBraket()
        {
            if (!_hit.transform.GetChild(1).transform.gameObject.activeSelf && _hit.transform.GetChild(0).transform.gameObject.activeSelf)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                _hit.transform.GetChild(1).transform.gameObject.SetActive(true);
                _hit.transform.GetChild(0).transform.gameObject.SetActive(false);

                asamaControl.Stage7Invoke();
            }
        }

        public void DeactiveBraket()
        {
            Destroy(ornekTabaktakiObje);
            ornekTabaktakiObje = null;


            StartingPositionAndRotation();
        }

        void StartingPositionAndRotation()
        {
            _braket.transform.position = _startingPosition;
            _braket.transform.rotation = _startingRotation;
        }
    }

}
