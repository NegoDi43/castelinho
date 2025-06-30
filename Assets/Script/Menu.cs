using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip som;
    private AudioSource player;

    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void Jogar()
    {


    }
    public void comeco()
    {
        SceneManager.LoadScene("comeco");
    }
    public void h()
    {
        SceneManager.LoadScene("entrada");
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Final");
    }
    public void credito()
    {
        SceneManager.LoadScene("creditos");
    }


}