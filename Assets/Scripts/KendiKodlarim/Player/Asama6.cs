using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisTelim
{
    public class Asama6
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;


        public Asama6()
        {


        }

        public void CreateDisTeli()
        {
            if (!_hit.transform.GetChild(2).transform.gameObject.activeSelf)
            {
                _hit.transform.GetChild(2).transform.gameObject.SetActive(true);
            }
        }
    }
}

