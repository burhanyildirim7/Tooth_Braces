using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisYayi
{
    public class Asama7 : MonoBehaviour
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

        [Header("Efektler")]
        private ParticleSystem _disKirilmaEfekt;
        private ParticleSystem _disOnarmaEfekt;


        private bool isAddingDisYayi;
        private bool isFirstDisYayi;

        public Asama7()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();
            _disKirilmaEfekt = playerControl.disKirilmaEfekt;
            _disOnarmaEfekt = playerControl.disOnarmaEfekt;

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
                        ins_disYayi = Instantiate(_solUstYay, Vector3.forward * -3+ Vector3.up * 1.5f, Quaternion.identity);
                    }
                    else if (_hit.point.x >= 0)
                    {
                        ins_disYayi = Instantiate(_sagUstYay, Vector3.forward * -3 + Vector3.up * 1.5f, Quaternion.identity);
                    }
                }
                else if (_hit.point.y < 0)
                {
                    if (_hit.point.x <= 0)
                    {
                        ins_disYayi = Instantiate(_solAltYay, Vector3.forward * -3 + Vector3.up * 1.5f, Quaternion.identity);
                    }
                    else if (_hit.point.x >= 0)
                    {
                        ins_disYayi = Instantiate(_sagAltYay, Vector3.forward * -3 + Vector3.up * 1.5f, Quaternion.identity);
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
            if (_hit.transform.gameObject.CompareTag("Tooth") && !_hit.transform.GetChild(2).transform.gameObject.activeSelf)
            {
                Tooth tooth = _hit.transform.GetComponent<Tooth>();
                if (!tooth.willFix & tooth.willWearDisYayi)
                {
                    CreateDisYayi();
                }

                if (isFirstDisYayi && tooth.willWearDisYayi)
                {
                    GameObject obje = ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
                    obje.transform.parent = _hit.transform.GetChild(1).transform;
                    obje.transform.localPosition = -Vector3.forward * .5f + Vector3.up * .5f;
                    _ilkDis = _hit.transform.gameObject;  //ilk disi bulur

                    isAddingDisYayi = true;
                    isFirstDisYayi = false;
                    tooth.willFix = true;
                    tooth.willWearDisYayi = false;
                    _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                }
                else if (tooth.willWearDisYayi)
                {
                    GameObject obje = ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
                    obje.transform.parent = _hit.transform.GetChild(1).transform;
                    obje.transform.localPosition = Vector3.forward * 1.3f + Vector3.up * .3f + Vector3.right * -.4f;
                    _ikinciDis = _hit.transform.gameObject;  //ikinci disi bulur

                    isAddingDisYayi = false;
                    ins_disYayi = null;

                    _hit.transform.GetComponent<Tooth>().willFix = true;

                    _asamaControl.neededDisYayi--;
                    _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
                    if (_disYayim.kopar)
                    {
                        _ilkDis.GetComponent<Tooth>().DisKirilsin((_ikinciDis.transform.position - _ilkDis.transform.position).normalized + Vector3.forward * -10f + Vector3.up * 4f);
                        _ikinciDis.GetComponent<Tooth>().DisKirilsin((_ilkDis.transform.position - _ikinciDis.transform.position).normalized + Vector3.forward * -10f + Vector3.up * 4f);
                        _disYayim.KuvvetUygula();
                        _disKirilmaEfekt.Play();
                    }
                    else
                    {
                        _asamaControl.ApplyTheMove();
                        _disOnarmaEfekt.Play();
                    }

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                    _ilkDis = null; //Bosa cikarmak icin kullanilir
                    _ikinciDis = null;
                }
                else //Ayni dise dis teli takmaya calisirken girilir
                {
                    Destroy(ins_disYayi);
                    isAddingDisYayi = false;
                    tooth.willWearDisYayi = true;
                    tooth.willFix = false;

                    _ilkDis = null; //Bosa cikarmak icin kullanilir
                    _ikinciDis = null;
                }
            }
            else //Dis teli takilamayacak olan objeye takmaya calisirken olur
            {
                Destroy(ins_disYayi);
                isAddingDisYayi = false;
            }
        }
    }
}
