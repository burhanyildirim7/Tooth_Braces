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

    [Header("Efektler")]
    [SerializeField] private ParticleSystem kotuEfekt;
    [SerializeField] private ParticleSystem iyiEfekt;

    public bool kopar;

    void Start()
    {
        mat = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
        fizik = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mat.color = new Color(0 + Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .6f, 1 - Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .6f, 0);

        if(Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) >= 1f)
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
        kemikler[0].transform.parent = transform;
        kemikler[1].transform.parent = transform;


        fizik.useGravity = true;
        fizik.velocity = Vector3.forward * -3f + Vector3.up * 5f + Vector3.right * 7;
    }
}
