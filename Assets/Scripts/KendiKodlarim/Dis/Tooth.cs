using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    [Header("RenkAyarlari")]
    private Material mat;
    [SerializeField] private float baslangicRenk;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;


        mat.SetFloat("_FlakeColorVariationAmount", baslangicRenk);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ToothBrush"))
        {
            baslangicRenk -= .035f;
            if (baslangicRenk <= .3f)
            {
                baslangicRenk = 0;
            }
            mat.SetFloat("_FlakeColorVariationAmount", baslangicRenk);
        }
    }
}
