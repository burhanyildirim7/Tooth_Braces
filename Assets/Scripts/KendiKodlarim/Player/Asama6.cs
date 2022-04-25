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

        [Header("KopmaAyarlari")]
        private DisYayim _disYayim;
        private GameObject _ilkDis;
        private GameObject _ikinciDis;

        [Header("Controllerler")]
        private AsamaControl _asamaControl;

        private bool isAddingDisYayi;
        private bool isFirstDisYayi;

        public Asama6()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();


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
                _disYayim = ins_disYayi.GetComponent<DisYayim>();

                ins_disYayi.transform.parent = GameObject.FindWithTag("Player").transform;
                _hit.transform.GetComponent<Tooth>().willFix = true;
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
                    _ilkDis = _hit.transform.gameObject;  //ilk disi bulur

                    isAddingDisYayi = true;
                    isFirstDisYayi = false;
                    _hit.transform.GetComponent<Tooth>().willFix = true;
                }
                else
                {
                    GameObject obje = ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
                    obje.transform.parent = _hit.transform.GetChild(1).transform;
                    obje.transform.localPosition = Vector3.forward * 1.3f + Vector3.up * .3f + Vector3.right * -.4f;
                    _ikinciDis = _hit.transform.gameObject;  //ikinci disi bulur

                    isAddingDisYayi = false;
                    ins_disYayi = null;

                    _hit.transform.GetComponent<Tooth>().willFix = true;

                    _asamaControl.neededDisYayi--;
                    if (_disYayim.kopar)
                    {
                        _ilkDis.GetComponent<Tooth>().DisKirilsin((_ikinciDis.transform.position - _ilkDis.transform.position).normalized + Vector3.forward * -4f + Vector3.up * 4f);
                        _ikinciDis.GetComponent<Tooth>().DisKirilsin((_ilkDis.transform.position - _ikinciDis.transform.position).normalized + Vector3.forward * -4f + Vector3.up * 4f);
                        _disYayim.KuvvetUygula();
                    }
                    if (_asamaControl.neededDisYayi <= 0)
                    {
                        _asamaControl.ApplyTheMove();
                    }

                    _ilkDis = null; //Bosa cikarmak icin kullanilir
                    _ikinciDis = null;
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

