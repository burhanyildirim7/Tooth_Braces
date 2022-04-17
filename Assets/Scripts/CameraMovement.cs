using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraMovement : MonoBehaviour
{

    // private GameObject Player;

    Vector3 aradakiFark;


    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        //aradakiFark = transform.position - Player.transform.position;
    }


    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y, Player.transform.position.z + aradakiFark.z), Time.deltaTime * 5f);

        if (GameController.instance.asamaSayisi >= 1 && Vector3.Distance(transform.position, -Vector3.forward * 12) >= 1f)
        {
            transform.DOMove(-Vector3.forward * 12, 1.25f);
        }
    }

}
