using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Importa os scripts do CrossPlatformInput.
using UnityStandardAssets.CrossPlatformInput;

public class Jogador : MonoBehaviour
{
	Rigidbody2D meuRigidBody;
	[SerializeField] float velocidadeDeCorrida = 5f;
    Animator meuAnimator;
    bool estaVivo = true;
    [SerializeField] float velocidadePulo = 5f;
    Collider2D meuColisor;
    [SerializeField] float velocidadeDeSubida = 5f;
    float gravidadeInicial;
    BoxCollider2D colisorDosPes;
    // Direção na qual o jogador é lançado ao morrer.
    [SerializeField] Vector2 direcaoDeMorte = new Vector2(25f, 25f);

    // Start is called before the first frame update
    void Start()
    {
        meuRigidBody = GetComponent<Rigidbody2D>();
        meuAnimator = GetComponent<Animator>();
        meuColisor = GetComponent<Collider2D>();
        gravidadeInicial = meuRigidBody.gravityScale;
        colisorDosPes = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(estaVivo == false)
        {
            return;
        }
        Correr();
        GirarSprite();
        Pular();
        SubirEscadas();
        Morrer();
    }

    private void Correr()
    {
    	float direcao = CrossPlatformInputManager.GetAxis("Horizontal"); // Valor entre -1 e 1. 
    	Vector2 velocidade = new Vector2(direcao * velocidadeDeCorrida, meuRigidBody.velocity.y);
    	meuRigidBody.velocity = velocidade;

        bool jogadorTemVelocidadeHorizontal = Mathf.Abs(meuRigidBody.velocity.x) > Mathf.Epsilon;
        meuAnimator.SetBool("Correndo", jogadorTemVelocidadeHorizontal); // Define animação corrida.
    }

    private void GirarSprite()
    {
        bool jogadorTemVelocidadeHorizontal = Mathf.Abs(meuRigidBody.velocity.x) > Mathf.Epsilon; //Nome nada prático. 
        if (jogadorTemVelocidadeHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(meuRigidBody.velocity.x), 1f);
        } 
    }

    private void Pular()
    {
        if(!colisorDosPes.IsTouchingLayers(LayerMask.GetMask("Chao")))
        {
            return;
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 velocidadeDePuloAdicionar = new Vector2(0f, velocidadePulo);
            meuRigidBody.velocity += velocidadeDePuloAdicionar;
        }
    }

    private void SubirEscadas()
    {
        if(!colisorDosPes.IsTouchingLayers(LayerMask.GetMask("Escada")))
        {
            meuAnimator.SetBool("Escalando", false);
            meuRigidBody.gravityScale = gravidadeInicial; 
            return;
        }

       float subir = CrossPlatformInputManager.GetAxis("Vertical"); 
       Vector2 direcaoDeSubida = new Vector2(meuRigidBody.velocity.x, subir * velocidadeDeSubida); 
       meuRigidBody.velocity = direcaoDeSubida;
       meuRigidBody.gravityScale = 0f;

       //Ativar animação escalada.
       bool jogadorTemVelocidadeVertical = Mathf.Abs(meuRigidBody.velocity.y) > Mathf.Epsilon;
       meuAnimator.SetBool("Escalando", jogadorTemVelocidadeVertical);

    }

    private void Morrer()
    {
        if(meuColisor.IsTouchingLayers(LayerMask.GetMask("Inimigos", "Espinhos")))
        {
            estaVivo = false;
            meuAnimator.SetTrigger("Morrendo");
            GetComponent<Rigidbody2D>().velocity = direcaoDeMorte;
            FindObjectOfType<GerenciadorDeJogo>().ProcessarMorte();
        }
    }
}
