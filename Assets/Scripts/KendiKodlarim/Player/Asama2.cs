using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisFircasi
{
    public class Asama2 
    {
        public Vector3 _touchPosition { get; set; }
        public RaycastHit _hit;


        private GameObject _disFircasi;

        public Asama2()
        {
            _disFircasi = GameObject.FindObjectOfType<PlayerControl>().disFircasi;
            Debug.Log(_disFircasi.name);
        }

        public void ActiveToothBrush()
        {

        }

        public void MoveToothBrush()
        {
            if(_disFircasi.activeSelf)
            {
                _disFircasi.transform.position = Vector3.Lerp(_disFircasi.transform.position, _hit.point, Time.deltaTime * 10);
            }
            else
            {
                _disFircasi.transform.position = _hit.point;
                _disFircasi.SetActive(true);
            }
        }

        public void DeactiveToothBrush()
        {
            _disFircasi.SetActive(false);
        }
    }

}
