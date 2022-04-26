using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DisPense
{
    public class Asama8 
    {
        [Header("DokunmaIslemleri")]
        public RaycastHit _hit;

        [Header("Controllerler")]
        private AsamaControl _asamaControl;

        public Asama8()
        {
            _asamaControl = GameObject.FindObjectOfType<AsamaControl>();
        }

        public void DestroyDisTeli()
        {
            if (_hit.transform.GetChild(2).transform.gameObject.activeSelf)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                _hit.transform.GetChild(2).transform.gameObject.SetActive(false);
                _asamaControl.DeleteTell();
                _hit.transform.GetChild(1).transform.gameObject.GetComponent<Animation>().Play("BraketAnim");
            }
        }
    }

}
