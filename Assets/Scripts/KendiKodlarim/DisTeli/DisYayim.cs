using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisYayim : MonoBehaviour
{
    [Header("Kemikler")]
    [SerializeField] private GameObject[] kemikler;

    [Header("Material")]
    private Material mat;

    [Header("KuuvetUygulamaIcin")]
    private Rigidbody fizik;

    [Header("Controllerler")]
    private AsamaControl asamaControl;

    [Header("Efektler")]
    [SerializeField] private ParticleSystem kotuEfekt;
    [SerializeField] private ParticleSystem iyiEfekt;

    public bool kopar;

    void Start()
    {
        mat = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
        asamaControl = GameObject.FindObjectOfType<AsamaControl>();
        fizik = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mat.color = new Color(0 + Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .2f, 1 - Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .2f, 0);

        if (Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) >= 2f)
        {
            kopar = true;
        }
        else
        {
            kopar = false;
        }
    }

    public void KuvvetUygula()
    {
        StartCoroutine(KuvvetUygulaGeciktir());
    }

    IEnumerator KuvvetUygulaGeciktir()
    {
        bool haraketEttiMi = false;
        while(!haraketEttiMi)
        {
            if (asamaControl.isMoveTooth)
            {
                kemikler[0].transform.parent = transform;
                kemikler[1].transform.parent = transform;


                fizik.useGravity = true;
                fizik.velocity = Vector3.forward * -3f + Vector3.up * 5f + Vector3.right * 7;

                haraketEttiMi = true;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
