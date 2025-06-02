using UnityEngine;

public class PlayerMoviment : MonoBehaviour

{
    private Rigidbody rb;
    private float inputH;
    private float inputV;
    private Animator animator;
    private bool estaNoChao = true;
    private float velocidadeAtual;
    private bool contato = false;
    private bool morrer = true;
    private SistemaDeVida sVida;
    private Vector3 anguloRotacao = new Vector3(0, 90, 0);
    [SerializeField] private float velocidadeAndar;
    [SerializeField] private float velocidadeCorrer;
    [SerializeField] private float forcaPulo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sVida = GetComponent<SistemaDeVida>();
        velocidadeAtual = velocidadeAndar;
    }

    // Update is called once per frame
    void Update()
    {
        if (sVida.EstaVivo())
        {
            andar();
            girar();
            pular();
            correr();
            //atacar();
            perfurar();
        }
        else if (!sVida.EstaVivo() && morrer)
        {
            Morrer();
        }
    }

    private void andar()
    {
        inputV = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * inputV;
        Vector3 moveForward = rb.position + moveDirection * velocidadeAtual * Time.deltaTime;
        rb.MovePosition(moveForward);

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("andar", true);
            animator.SetBool("andarTras", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("andarTras", true);
            animator.SetBool("andar", false);
        }
        else
        {
            animator.SetBool("andarTras", false);
            animator.SetBool("andar", false);
        }
    }

    private void girar()
    {
        inputH = Input.GetAxis("Horizontal");
        Quaternion deltaRotation =
            Quaternion.Euler(anguloRotacao * inputH * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if (Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.LeftArrow) ||
                            Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("andar", true);
        }
    }

    private void pular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            animator.SetTrigger("pular");
        }
    }

    private void correr()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            velocidadeAtual = velocidadeCorrer;
            animator.SetBool("correr", true);
        }
        else
        {
            velocidadeAtual = velocidadeAndar;
            animator.SetBool("correr", false);
        }
    }

    private void Morrer()
    {
        morrer = false;
        animator.SetBool("estaVivo", false);
        animator.SetTrigger("morrer");
        rb.Sleep();
    }

    private void interagir()
    {
        animator.SetTrigger("interagir");
    }

    private void pegar()
    {
        animator.SetTrigger("pegar");
    }

    private void atacar()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        animator.SetTrigger("atacar");
        //}
    }

    private void perfurar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("perfurar");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Quebra"))
        {
            atacar();
        }
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            estaNoChao = true;
            animator.SetBool("estaNoChao", true);
        }
        if (collision.gameObject.CompareTag("Quebra") && Input.GetMouseButtonDown(0))
        {
            atacar();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            estaNoChao = false;
            animator.SetBool("estaNoChao", false);
        }
        if (collision.gameObject.CompareTag("Quebra"))
        {

            contato = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && Input.GetKey(KeyCode.E))
        {
            pegar();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Porta") && Input.GetKey(KeyCode.E))
        {
            interagir();
            other.gameObject.GetComponent<Animator>().SetTrigger("abrir");
        }
    }
}
