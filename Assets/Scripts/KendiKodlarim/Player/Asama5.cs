using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisBraket
{
    public class Asama5 
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;


        public Asama5()
        {

        }

        public void CreateBraket()
        {
            if (!_hit.transform.GetChild(1).transform.gameObject.activeSelf)
            {
                _hit.transform.GetChild(1).transform.gameObject.SetActive(true);
                _hit.transform.GetChild(0).transform.gameObject.SetActive(false);
            }
        }
    }

}
