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


    void Start()
    {
        ceneHaraket = new CeneHaraket(transform, hedefRot);
    }

    void Update()
    {
        if (GameController.instance.openMouth)
        {
            ceneHaraket.OpenMouth();
        }
        else if (GameController.instance.closeMouth)
        {
            ceneHaraket.CloseMouth();
        }
    }
}
