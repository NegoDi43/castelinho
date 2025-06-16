using UnityEngine;

public class objetoQuebra : MonoBehaviour
{
    [SerializeField] private int vidaObl;
    [SerializeField] private GameObject efeitoQuebra;

    public void Quebrar(int dano)
    {
        vidaObl -= dano;
        if (vidaObl <= 0)
        {
            Instantiate(efeitoQuebra, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
