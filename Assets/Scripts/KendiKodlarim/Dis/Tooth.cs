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

    [Header("RotasyonAyarlariVePozisyon")]
    [SerializeField] private Vector3 hedefRot;
    [SerializeField] private Vector3 hedefPos;

    private bool isMove;

    [Header("DuzeltimeAyarlari")]
    public bool willFix;




    private WaitForSeconds beklemeSuresi = new WaitForSeconds(.25f);

    void Start()
    {
        mat = transform.parent.transform.GetChild(0).transform.gameObject.GetComponent<SkinnedMeshRenderer>().material;

        mat.SetFloat("_FlakeColorVariationAmount", baslangicRenk);

        toothController = GameObject.FindObjectOfType<ToothController>();
        asamaControl = GameObject.FindObjectOfType<AsamaControl>();

        isMove = false;
        willFix = false;

        StartCoroutine(StageController1());
        StartCoroutine(MoveControl());
    }


    private IEnumerator StageController1()
    {
        yield return beklemeSuresi;
        while (true)
        {
            if (mat.GetFloat("_FlakeColorVariationAmount") <= .65f)
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
        while (true)
        {
            if (mat.GetFloat("_FlakeColorVariationAmount") <= 0)
            {
                toothController.Stage2FinishedTooth();
                break;
            }
            yield return beklemeSuresi;
        }
    }



    IEnumerator MoveControl() //Dis haraketi icin gereklidir
    {
        while (!isMove)
        {
            if (asamaControl.isMoveTooth)
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
        while (willFix && Vector3.Distance(parentTransform.localPosition, hedefPos) <= .01f)
        {
            parentTransform.localPosition = Vector3.Lerp(parentTransform.localPosition, hedefPos, Time.deltaTime * 7);
            parentTransform.localRotation = Quaternion.Slerp(parentTransform.localRotation, Quaternion.Euler(hedefRot), Time.deltaTime * 1);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToothBrush"))
        {
            if (baslangicRenk <= .75f && baslangicRenk > .5f)
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

    public void DisKirilsin(Vector3 yon)
    {
        Rigidbody fizik;
        fizik = transform.parent.transform.gameObject.AddComponent<Rigidbody>();

        fizik.useGravity = true;
        fizik.velocity = yon;
    }
}
