using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardingController : MonoBehaviour
{
    [Header("ElAnim")]
    [SerializeField] private Animation anim;

    [Header("Panel")]
    [SerializeField] private GameObject panel;

    void Start()
    {
        if (PlayerPrefs.GetInt("level") == 0)
        {
            Asama1();
        }
    }


    public void Asama1()
    {
        panel.SetActive(true);
        anim.Play("Asama1");
    }
}
