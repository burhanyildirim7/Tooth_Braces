using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsamaControl : MonoBehaviour
{
    [Header("Asamalar")]
    [SerializeField] private GameObject[] asamalar;

    [Header("DisSayisi")]
    private int disSayisi;

    [Header("DisTeliHaraketi")]
    public bool isMoveDentalBraces;

    [Header("DisHaraketi")]
    public bool isMoveTooth;

    void Start()
    {
        disSayisi = GameObject.FindGameObjectsWithTag("Tooth").Length;
        isMoveDentalBraces = false;
        isMoveTooth = false;
    }

    public void Stage4Invoke()
    {
        for (int i = 0; i < asamalar.Length; i++)
        {
            asamalar[i].SetActive(false);
        }
        asamalar[1].SetActive(true);
    }


    public void Stage7Invoke()
    {
        disSayisi--;

        if(disSayisi <= 0)
        {
            ActiveDentalBraces();
        }
    }

    public void ActiveDentalBraces()
    {
        for (int i = 0; i < asamalar.Length; i++)
        {
            asamalar[i].SetActive(false);
        }
        asamalar[2].SetActive(true);

        StartCoroutine(MoveDentalBraces());
    }


    IEnumerator MoveDentalBraces()
    {
        yield return new WaitForSeconds(.75f);
        isMoveDentalBraces = true;

        yield return new WaitForSeconds(.75f);
        isMoveTooth = true;
    }


}
