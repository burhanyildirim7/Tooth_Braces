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
    private AsamaControl asamaControl;

    [Header("RotasyonAyarlari")]
    [SerializeField] private Vector3 hedefRot;

    private bool isMove;



    private WaitForSeconds beklemeSuresi = new WaitForSeconds(.25f);

    void Start()
    {
        mat = transform.parent.transform.GetChild(0).transform.gameObject.GetComponent<SkinnedMeshRenderer>().material;

        mat.SetFloat("_FlakeColorVariationAmount", baslangicRenk);

        toothController = GameObject.FindObjectOfType<ToothController>();
        asamaControl = GameObject.FindObjectOfType<AsamaControl>();

        isMove = false;

        StartCoroutine(StageController1());
        StartCoroutine(MoveControl());
    }


    private IEnumerator StageController1()
    {
        yield return beklemeSuresi;
        while (true)
        {
            if(mat.GetFloat("_FlakeColorVariationAmount") <= .65f)
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
                StartCoroutine(StageController4());
                break;
            }
            yield return beklemeSuresi;
        }
    }

    private IEnumerator StageController4()
    {
        while (true)
        {
            if (transform.GetChild(0).localScale.y * 1000 >= .1f)
            {
                toothController.Stage4FinishedTooth();
                StartCoroutine(StageController5());
                break;
            }
            yield return beklemeSuresi;
        }
    }

    private IEnumerator StageController5()
    {
        while (true)
        {
            if (transform.GetChild(1).transform.gameObject.activeSelf)
            {
                toothController.Stage5FinishedTooth();
                break;
            }
            yield return beklemeSuresi;
        }
    }


    IEnumerator MoveControl() //Dis haraketi icin gereklidir
    {
        while(!isMove)
        {
            if(asamaControl.isMoveTooth)
            {
                isMove = true;
                StartCoroutine(SmoothlyMove());
                break;
            }    
            yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator SmoothlyMove()
    {
        Transform parentTransform = transform.parent;
        while(true)
        {
            parentTransform.localRotation = Quaternion.Slerp(parentTransform.localRotation, Quaternion.Euler(hedefRot), Time.deltaTime * 3);
            yield return null;
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
                baslangicRenk -= .025f;
            }
            mat.SetFloat("_FlakeColorVariationAmount", baslangicRenk);
        }
        else if (other.CompareTag("Water") && baslangicRenk <= .6f)
        {
            mat.SetFloat("_FlakeColorVariationAmount", 0);
        }
    }

}
