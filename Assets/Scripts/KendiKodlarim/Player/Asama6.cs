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
        public GameObject _solUstYay;
        public GameObject _sagUstYay;
        public GameObject _solAltYay;
        public GameObject _sagAltYay;
        private GameObject ins_disYayi;

        private bool isAddingDisYayi;
        private bool isFirstDisYayi;

        public Asama6()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();

            _solUstYay = playerControl.solUstYay;
            _sagUstYay = playerControl.sagUstYay;
            _solAltYay = playerControl.solAltYay;
            _sagAltYay = playerControl.sagAltYay;

            isAddingDisYayi = false;
            isFirstDisYayi = true;
        }


        public void CreateDisYayi()
        {
            if (!isAddingDisYayi)
            {

                if (_hit.point.y > 0)
                {
                    if (_hit.point.x <= 0)
                    {
                        ins_disYayi = Instantiate(_solUstYay);
                    }
                    else if (_hit.point.x >= 0)
                    {
                        ins_disYayi = Instantiate(_sagUstYay);
                    }
                }
                else if (_hit.point.y < 0)
                {
                    if (_hit.point.x <= 0)
                    {
                        ins_disYayi = Instantiate(_solAltYay);
                    }
                    else if (_hit.point.x >= 0)
                    {
                        ins_disYayi = Instantiate(_sagAltYay);
                    }
                }
                isAddingDisYayi = true;
                isFirstDisYayi = true;
            }
        }

        public void MoveDisYayi()
        {
            if (isAddingDisYayi)
            {
                ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.position = _hit.point;
            }

        }


        public void DeactiveDisYayi()
        {
            if (_hit.transform.gameObject.CompareTag("Tooth"))
            {
                CreateDisYayi();
                if (isFirstDisYayi)
                {

                    GameObject obje = ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
                    obje.transform.parent = _hit.transform.GetChild(1).transform;
                    obje.transform.localPosition = -Vector3.forward * .5f + Vector3.up * .5f;

                    isAddingDisYayi = true;
                    isFirstDisYayi = false;
                }
                else
                {
                    GameObject obje = ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
                    obje.transform.parent = _hit.transform.GetChild(1).transform;
                    obje.transform.localPosition = Vector3.forward * 1.3f + Vector3.up * .3f + Vector3.right * -.4f;

                    isAddingDisYayi = false;
                    ins_disYayi = null;
                }
            }
            else
            {
                Destroy(ins_disYayi);
                isAddingDisYayi = false;
            }
        }


    }
}

