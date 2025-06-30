using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeVida : MonoBehaviour
{
    [SerializeField] private int vida = 100;
    [SerializeField] private int mana = 100;
    [SerializeField] private GameObject telaDeMorte;
    [SerializeField] private Slider manaIndicador;
    [SerializeField] private Slider vidaIndicador;
    private bool estaVivo = true;
    private bool levarDano = true;
    private PlayerMovement pMove;
    private bool podeRecarregarMana = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        procuraReferencia();

        pMove = GetComponent<PlayerMovement>();
        if (telaDeMorte.activeSelf == true)
        {
            telaDeMorte.SetActive(false);
        }
    }

        public void procuraReferencia()
    {


        if (manaIndicador == null)
        {
            manaIndicador = GameObject.Find("Mana").GetComponent<Slider>();
            manaIndicador.maxValue = mana;
            manaIndicador.value = mana;
        }

        if (vidaIndicador == null)
        {
            vidaIndicador = GameObject.Find("Vida").GetComponent<Slider>();
            vidaIndicador.maxValue = vida;
            vidaIndicador.value = vida;
        }

        pMove = GetComponent<PlayerMovement>();
    }


    // Update is called once per frame
    void Update()
    {
        procuraReferencia();
    }

    public bool EstaVivo()
    {
        return estaVivo;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fatal") && estaVivo && levarDano)
        {
            StartCoroutine(LevarDano(10));
        }
        if (collision.gameObject.CompareTag("IK") && estaVivo && levarDano)
        {
            StartCoroutine (LevarDano(50));
        }
    }

    IEnumerator LevarDano(int dano)
    {
        levarDano = false;

        if (vida > 0)
        {
            pMove.Hit();
            vida -= dano;
            vidaIndicador.value = vida;
            VerificarVida();
            yield return new WaitForSeconds(0.5f);
            levarDano = true;
        }
    }

    private void VerificarVida()
    {
        if (vida <= 0)
        {
            vida = 0;
            estaVivo = false;
            TelaDeMorte();
        }
    }

    private void TelaDeMorte()
    {
        telaDeMorte.SetActive(true);
    }
    public void CargaMana(int carga)
    {
        mana += carga;
        manaIndicador.value = mana;
        if (mana > 100)
        {
            mana = 100;
            manaIndicador.value = vida;
        }
    }

    public void CargaVida(int carga)
    {
        vida += carga;
        vidaIndicador.value = vida;
        if (vida > 100)
        {
            vida = 100;
            vidaIndicador.value = vida;
        }
    }

    public void UsarMana()
    {
        mana -= 10;
        manaIndicador.value = mana;
        if (podeRecarregarMana)
        {
            StartCoroutine("RecarregaMana");
        }
    }

    public int GetMana()
    {
        return mana;
    }

    IEnumerable RecarregaMana()
    {
        podeRecarregarMana = false;
        while (mana < 100)
        {
            mana += 5;
            manaIndicador.value = mana;
            yield return new WaitForSeconds(0.1f);
        }
        mana = 100;
        podeRecarregarMana = true;
    }
}