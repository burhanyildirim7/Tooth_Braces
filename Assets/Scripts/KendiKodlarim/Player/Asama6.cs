using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisTelim
{
    public class Asama6
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        [Header("Controllerler")]
        private AsamaControl _asamaControl;


        public Asama6()
        {
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();

        }

        public void CreateDisTeli()
        {
            if (!_hit.transform.GetChild(2).transform.gameObject.activeSelf)
            {
                _hit.transform.GetChild(2).transform.gameObject.SetActive(true);
                _asamaControl.LastStage();
                _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
            }
        }
    }
}

