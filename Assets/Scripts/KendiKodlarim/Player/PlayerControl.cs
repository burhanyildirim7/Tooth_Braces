using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBehaviour;


public class PlayerControl : MonoBehaviour
{
    [Header("MirasIslemleri")]
    private PlayerTouch playerTouch;

    [Header("Asama1")]
    public ParticleSystem disMacunu;

    [Header("Asama2")]
    public ParticleSystem disFircasi;


    void Start()
    {
        playerTouch = new PlayerTouch();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isContinue)
        {
            playerTouch.Touch();
        }
    }
}
