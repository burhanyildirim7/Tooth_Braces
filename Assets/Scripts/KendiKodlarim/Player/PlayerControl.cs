using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBehaviour;


public class PlayerControl : MonoBehaviour
{
    [Header("MirasIslemleri")]
    public PlayerTouch playerTouch;


    [Header("Asama1")]
    public GameObject disFircasi;
    public ParticleSystem bubbleEffect;

    [Header("Asama2")]
    public GameObject waterEffect;
    public GameObject waterObj;
    public GameObject waterCollier;

    [Header("Asama3")]
    public GameObject water;
    public GameObject waterSender;

    [Header("Asama4")]
    public GameObject sticky;

    [Header("Asama5")]
    public GameObject braket;

    [Header("Asama6")]
    public GameObject disTeli;
    public ParticleSystem telOlusmaEfekt;

    [Header("Asama7")]
    public GameObject disYayiOrnek;
    public GameObject disYayi;

    [Header("Asama8")]
    public GameObject disKerpeten;

    [Header("Efektler")]
    public ParticleSystem disKirilmaEfekt;
    public ParticleSystem disOnarmaEfekt;



    void Start()
    {
        playerTouch = new PlayerTouch();
        Application.targetFrameRate = 60;

        StartCoroutine(Bekle());
    }

    IEnumerator Bekle()
    {
        yield return new WaitForSeconds(.1f);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isContinue)
        {
            playerTouch.Touch();
        }
    }

    public void ChangeCase(int numberOfCase)
    {
        playerTouch.caseNumber = numberOfCase;
    }
}
