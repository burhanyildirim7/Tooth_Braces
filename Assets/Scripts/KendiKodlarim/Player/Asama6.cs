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

        [Header("Efektler")]
        private ParticleSystem _telOlusmaEfekt;

        public Asama6()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();

            _telOlusmaEfekt = playerControl.telOlusmaEfekt;

        }

        public void CreateDisTeli()
        {
            if (!_hit.transform.GetChild(2).transform.gameObject.activeSelf)
            {
                _hit.transform.GetChild(2).transform.gameObject.SetActive(true);
                _asamaControl.LastStage();
                _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
                Instantiate(_telOlusmaEfekt, _hit.point + Vector3.forward * -.25f + Vector3.up * .25f, Quaternion.identity);
            }
        }
    }
}

