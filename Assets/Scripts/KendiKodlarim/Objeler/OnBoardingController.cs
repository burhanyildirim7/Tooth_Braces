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

    }

    public void PlayOnBoarding(int sayi)
    {
        if(PlayerPrefs.GetInt("Asama" + sayi.ToString()) == 0)
        {
            panel.SetActive(true);
            anim.Play("Asama" + sayi.ToString());
            PlayerPrefs.SetInt("Asama" + sayi.ToString(), 1);
        }
    }

    public void DeactiveOnBoarding()
    {
        anim.Stop();
        panel.SetActive(false);
    }

}
