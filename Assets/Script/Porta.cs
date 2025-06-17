using UnityEngine;

public class Porta : MonoBehaviour
{
    [SerializeField] private int numeroPorta;
    [SerializeField] private bool portaTrancada = false;
    [Header("Caso Trancada,Defina o Sprite de aviso")]
    [SerializeField] private Sprite spriteAvisoPorta;
    private Animator animator;
    private Avisos avisoPorta;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (portaTrancada)
        {
            avisoPorta = GetComponent<Avisos>();
        }
    }
    public void AbrirPorta(int nChave = 0)
    {
        if (nChave == 0 && !portaTrancada)
        {
            animator.SetTrigger("abrir");
        }
        else if (nChave == numeroPorta && portaTrancada)
        {
            animator.SetTrigger("abrir");
            portaTrancada = false;
            avisoPorta.DefineTroca(spriteAvisoPorta, "Porta Destrancada", Color.green);
        }
    }
    public bool EstaTrancada()
    {
        return portaTrancada;
    }
}