using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisTeli : MonoBehaviour
{
    [Header("Hedef")]
    [SerializeField] private Transform hedef;

    [Header("OfsetAyarlari")]
    [SerializeField] private Vector3 offset;

    private bool haraketEdiyorMu;

    private WaitForSeconds beklemeSuresi = new WaitForSeconds(.075f);

    private AsamaControl asamaControl;



    void Start()
    {
        haraketEdiyorMu = false;
        asamaControl = GameObject.FindObjectOfType<AsamaControl>();

        StartCoroutine(MoveControl());
    }


    IEnumerator MoveControl()
    {
        while (!haraketEdiyorMu)
        {
            if (asamaControl.isMoveDentalBraces)
            {
                haraketEdiyorMu = true;
                StartCoroutine(Move());
                break;
            }
            yield return beklemeSuresi;
        }
    }

    IEnumerator Move()
    {
        while (Vector3.Distance(transform.position, hedef.position) >= .01f)
        {
            transform.position = Vector3.Lerp(transform.position, hedef.transform.GetChild(1).transform.position + offset, Time.deltaTime * 12);
            yield return null;
        }
    }
}
