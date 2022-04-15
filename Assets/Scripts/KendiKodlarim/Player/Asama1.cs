using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DisMacunu
{
    public class Asama1 : MonoBehaviour
    {
        public Vector3 _touchPosition { get; set; }
        public RaycastHit _hit;

        [Header("DisMacunuIslemleri")]
        public ParticleSystem _disMacunuEfekt;

        private PlayerControl playerControl;

        public Asama1()
        {
            playerControl = GameObject.FindObjectOfType<PlayerControl>();

            //_disMacunu = playerControl.disMacunu;

        }

        public void DisMacunuEfektBaslat()
        {
            _disMacunuEfekt = Instantiate(playerControl.disMacunu, _hit.point, Quaternion.identity).GetComponent<ParticleSystem>();
            _disMacunuEfekt.transform.position = _hit.point;
            _disMacunuEfekt.Play();
        }

        public void DisMacunuCikar()
        {
            _disMacunuEfekt.transform.position = _hit.point;
        }

        public void DisMacunuEfektBitir()
        {
            _disMacunuEfekt?.Stop();
        }
    }
}

