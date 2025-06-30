using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mudar : MonoBehaviour
{

    [SerializeField] private string nomeDaProximaFase = "";
    [SerializeField] private float tempoDeTransicao = 1.0f;
    [SerializeField] private GameObject efeitoFade;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = efeitoFade.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!string.IsNullOrEmpty(nomeDaProximaFase))
        {
            StartCoroutine(TransicaoParaProximaFase());
        }

    }

    IEnumerator TransicaoParaProximaFase()
    {
        // Inicia o efeito de fade
        animator.SetTrigger("FadeOut");
        // Espera o tempo de transição
        yield return new WaitForSeconds(tempoDeTransicao);
        // Carrega a próxima fase
        SceneManager.LoadScene(nomeDaProximaFase);
    }

    public void MudarParaMasmorra()
    {
        SceneManager.LoadScene("entrada");
    }

    public void MudarParaCreditos()
    {
        SceneManager.LoadScene("creditos");
    }

    public void MudarParaInicio()
    {
        SceneManager.LoadScene("comeco");
    }

    public void MudarPara()
    {
        SceneManager.LoadScene("Final");
    }

}