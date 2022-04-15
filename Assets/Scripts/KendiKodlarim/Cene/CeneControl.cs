using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CeneBehaviour;

public class CeneControl : MonoBehaviour
{
    [Header("RotasyonIslemleri")]
    [SerializeField] private float hedefRot;


    [Header("MirasIslemleri")]
    private CeneHaraket ceneHaraket;

    private WaitForSeconds beklemeSuresi0 = new WaitForSeconds(.2f);

    void Start()
    {
        ceneHaraket = new CeneHaraket(transform, hedefRot);
        StartCoroutine(OyunKontrol());
    }

    IEnumerator OyunKontrol()
    {
        while(true)
        {
            if(GameController.instance.openMouth)
            {
                ceneHaraket.OpenMouth();
            }
            else if(GameController.instance.closeMouth)
            {
                ceneHaraket.CloseMouth();
            }

            yield return beklemeSuresi0;
        }
    }
}
