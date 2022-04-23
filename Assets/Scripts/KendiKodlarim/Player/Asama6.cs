using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisYayi
{
    public class Asama6 : MonoBehaviour
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        [Header("DisYayiIcinGerkelidir")]
        private GameObject _disYayi;
        private GameObject ins_disYayi;

        private bool isAddinDisYayi;

        public Asama6()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();

            _disYayi = playerControl.disYayi;

            isAddinDisYayi = false;
        }


        public void CreateDisYayi()
        {
            ins_disYayi = Instantiate(_disYayi);
        }

        public void MoveDisYayi()
        {

        }


        public void DeactiveDisYayi()
        {
            
        }
        

    }
}

