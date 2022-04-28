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
        public GameObject _disYayiOrnek;
        private GameObject ins_disYayi;
        private GameObject _disYayi;

        [Header("KopmaAyarlari")]
        private DisYayim _disYayim;
        private GameObject _ilkDis;
        private GameObject _ikinciDis;

        [Header("Controllerler")]
        private AsamaControl _asamaControl;

        [Header("Efektler")]
        private ParticleSystem _disKirilmaEfekt;
        private ParticleSystem _disOnarmaEfekt;

        [Header("PositionAndRotationSettings")]
        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        private string ilkIsim;
        private string ikinciIsim;


        public bool isAddingDisYayi;
        private bool isFirstDisYayi;

        public Asama7()
        {
            PlayerControl playerControl = GameObject.FindObjectOfType<PlayerControl>();
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();
            _disKirilmaEfekt = playerControl.disKirilmaEfekt;
            _disOnarmaEfekt = playerControl.disOnarmaEfekt;

            _disYayiOrnek = playerControl.disYayiOrnek;
            _disYayi = playerControl.disYayi;

            isAddingDisYayi = false;
            isFirstDisYayi = true;

            _startingPosition = _disYayi.transform.position;
            _startingRotation = _disYayi.transform.rotation;
        }


        public void CreateDisYayi()
        {
            if (!isAddingDisYayi)
            {

                ins_disYayi = Instantiate(_disYayiOrnek, Vector3.forward * -3 + Vector3.up * .85f - Vector3.right * .95f, Quaternion.identity);

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
            else
            {
                _disYayi.transform.position = _hit.point;
                _disYayi.transform.rotation = Quaternion.Euler(Vector3.right * -10 + Vector3.up * -4 * (Mathf.Pow(Mathf.Abs(_hit.point.x), 3) / _hit.point.x) + Vector3.forward * 70);
            }
        }


        public void DeactiveDisYayi()
        {
            if (_hit.transform.gameObject.tag == "Tooth" && !_hit.transform.GetChild(2).transform.gameObject.activeSelf)
            {
                Tooth tooth = _hit.transform.GetComponent<Tooth>();

                if (isFirstDisYayi)
                {
                    CreateDisYayi();
                    GameObject obje = ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
                    obje.transform.parent = _hit.transform.GetChild(1).transform;
                    obje.transform.localPosition = Vector3.zero;
                    _ilkDis = _hit.transform.gameObject;  //ilk disi bulur

                    tooth.AnimasyonuDurdur();
                    ilkIsim = _hit.transform.parent.transform.gameObject.name;

                    isAddingDisYayi = true;
                    isFirstDisYayi = false;
                    tooth.willFix = true;
                    tooth.willWearDisYayi = false;
                    _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                }
                else if (ilkIsim != _hit.transform.parent.transform.gameObject.name)
                {
                    GameObject obje = ins_disYayi.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
                    obje.transform.parent = _hit.transform.GetChild(1).transform;
                    obje.transform.localPosition = Vector3.zero;
                    _ikinciDis = _hit.transform.gameObject;  //ikinci disi bulur

                    tooth.AnimasyonuDurdur();

                    isAddingDisYayi = false;
                    ins_disYayi = null;

                    _hit.transform.GetComponent<Tooth>().willFix = true;

                    _asamaControl.ReduceTelSayisi(2);
                    _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
                    if (_disYayim.kopar)
                    {
                        _ilkDis.GetComponent<Tooth>().DisKirilsin(Vector3.forward * -12f + Vector3.up * 5f);
                        _ikinciDis.GetComponent<Tooth>().DisKirilsin(Vector3.forward * -12f + Vector3.up * 5f);
                        _disYayim.KuvvetUygula();
                        _disKirilmaEfekt.Play();

                        isFirstDisYayi = true;
                        isAddingDisYayi = false;
                        _ilkDis = null; //Bosa cikarmak icin kullanilir
                        _ikinciDis = null;
                        ilkIsim = null;
                    }
                    else
                    {
                        //   _asamaControl.ApplyTheMove();
                        _disOnarmaEfekt.Play();

                        isFirstDisYayi = true;
                        isAddingDisYayi = false;
                        _ilkDis = null; //Bosa cikarmak icin kullanilir
                        _ikinciDis = null;
                        ilkIsim = null;
                    }

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                    _ilkDis = null; //Bosa cikarmak icin kullanilir
                    _ikinciDis = null;
                }
                else //Ayni dise dis teli takmaya calisirken girilir
                {
                    Destroy(ins_disYayi);
                    isFirstDisYayi = true;
                    isAddingDisYayi = false;
                    tooth.willWearDisYayi = true;
                    tooth.willFix = false;

                    _ilkDis = null; //Bosa cikarmak icin kullanilir
                    _ikinciDis = null;
                    ilkIsim = null;
                }
            }
            else //Dis teli takilamayacak olan objeye takmaya calisirken olur
            {
                Destroy(ins_disYayi);
                isAddingDisYayi = false;
            }

            StartingPositionAndRotation();
        }

        void StartingPositionAndRotation()
        {
            while (Vector3.Distance(_disYayi.transform.position, _startingPosition) >= .1f)
            {
                _disYayi.transform.position = Vector3.Lerp(_disYayi.transform.position, _startingPosition, Time.deltaTime * 15);
                _disYayi.transform.rotation = Quaternion.Slerp(_disYayi.transform.rotation, _startingRotation, Time.deltaTime * 500);
            }
        }
    }
}
