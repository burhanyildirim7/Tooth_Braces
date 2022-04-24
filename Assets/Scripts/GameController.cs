using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance; // singleton yapisi icin gerekli ornek ayrintilar icin BeniOku 22. satirdan itibaren bak.


    [HideInInspector] public int score, elmas; // ayrintilar icin benioku 9. satirdan itibaren bak

    [HideInInspector] public bool isContinue;  // ayrintilar icin beni oku 19. satirdan itibaren bak

    [Header("Asamalar")]
    public bool closeMouth;
    public bool openMouth;
    public int asamaSayisi;

    private int puanKatsayisi;

    private WaitForSeconds beklemeSuresi = new WaitForSeconds(1);

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);

        isContinue = false;

    }


    public void StartingEvents()
    {
        isContinue = false;
        StopCoroutine(PuanHesaplayici());
        StartCoroutine(PuanHesaplayici());
    }

    IEnumerator PuanHesaplayici()
    {
        Debug.Log("A");
        puanKatsayisi = 120;
        while (puanKatsayisi > 0)
        {
            puanKatsayisi--;
            Debug.Log(puanKatsayisi);
            yield return beklemeSuresi;
        }
    }

    public void FinishingEvents()
    {
        SetScore(100 + puanKatsayisi * 10);
    }


    /// <summary>
    /// Bu fonksiyon geçerli leveldeki scoreu belirtilen miktarda artirir veya azaltir. Artirma icin +5 gibi pozitif eksiltme
    /// icin -5 gibi negatif deger girin.
    /// </summary>
    /// <param name="eklenecekScore">Her collectible da ne kadar score eklenip cikarilacaksa parametre olarak o sayi verilmeli</param>
    public void SetScore(int eklenecekScore)
    {
        score += eklenecekScore;
        // Eðer oyunda collectible yok ise developer kendi score sistemini yazmalý...

    }


    /// <summary>
    /// Bu fonksiyon geçerli leveldeki elmasi belirtilen miktarda artirir veya azaltir. Artirma icin +5 gibi pozitif eksiltme
    /// icin -5 gibi negatif deger girin.
    /// </summary>
    /// <param name="eklenecekElmas">Her collectible da ne kadar elmas eklenip cikarilacaksa parametre olarak o sayi verilmeli</param>
    public void SetElmas(int eklenecekElmas)
    {
        elmas += eklenecekElmas;
        // buradaki elmas artýnca totalScore da otomatik olarak artacak.. bu sebeple asagidaki kodlar eklendi.
        PlayerPrefs.SetInt("totalElmas", PlayerPrefs.GetInt("totalElmas" + eklenecekElmas));
        // UIController.instance.SetTotalElmasText(); // totalElmaslarýn yazili oldugu texti
    }


    /// <summary>
    /// Oyun sonu x ler hesaplanip kac ile carpilacaksa parametre olacak o sayi gonderilmeli.
    /// </summary>
    /// <param name="katsayi"></param>
    public void ScoreCarp(int katsayi)
    {
        if (PlayerController.instance.xVarMi) score *= katsayi;
        else score = 1 * score;
        PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") + score);
    }

}
