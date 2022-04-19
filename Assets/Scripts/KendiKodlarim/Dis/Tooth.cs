using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tooth : MonoBehaviour
{
    [Header("RenkAyarlari")]
    private Material mat;
    [SerializeField] private float baslangicRenk;

    [Header("Controllers")]
    private ToothController toothController;



    private WaitForSeconds beklemeSuresi = new WaitForSeconds(.25f);

    void Start()
    {
        mat = GetComponent<SkinnedMeshRenderer>().material;

        mat.SetFloat("_FlakeColorVariationAmount", baslangicRenk);

        toothController = GameObject.FindObjectOfType<ToothController>();


        StartCoroutine(StageController1());
    }


    private IEnumerator StageController1()
    {
        yield return beklemeSuresi;
        while (true)
        {
            if(mat.GetFloat("_FlakeColorVariationAmount") <= .51f)
            {
                toothController.Stage1FinishedTooth();
                StartCoroutine(StageController2());
                break;
            }
            yield return beklemeSuresi;
        }
    }

    private IEnumerator StageController2()
    {
        while(true)
        {
            if (mat.GetFloat("_FlakeColorVariationAmount") <= 0)
            {
                toothController.Stage2FinishedTooth();
                break;
            }
            yield return beklemeSuresi;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToothBrush"))
        {
            if (baslangicRenk <= .7f && baslangicRenk > .5f)
            {
                baslangicRenk = .5f;
            }
            else if (baslangicRenk > .7f)
            {
                baslangicRenk -= .035f;
            }
            mat.SetFloat("_FlakeColorVariationAmount", baslangicRenk);
        }
        else if (other.CompareTag("Water"))
        {
            mat.SetFloat("_FlakeColorVariationAmount", 0);
        }
    }

}
