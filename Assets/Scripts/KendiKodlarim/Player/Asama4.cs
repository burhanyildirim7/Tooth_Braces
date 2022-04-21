using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisYapiskan
{
    public class Asama4
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;


        public Asama4()
        {

        }


        public void BiggerSticky()
        {
            if(_hit.transform.GetChild(0).transform.gameObject.activeSelf && _hit.transform.GetChild(0).transform.localScale.y * 1000 <= 1)
            {
                _hit.transform.GetChild(0).transform.localScale += Vector3.one * .00006f;
            }
            else
            {
                _hit.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
             
        }

    }
}

