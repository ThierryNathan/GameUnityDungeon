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

    // Start is called before the first frame update
    void Start()
    {
        meuRigidBody = GetComponent<Rigidbody2D>();
        meuAnimator = GetComponent<Animator>();
        meuColisor = GetComponent<Collider2D>();
        gravidadeInicial = meuRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Correr();
        GirarSprite();
        Pular();
        SubirEscadas();
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
        if(!meuColisor.IsTouchingLayers(LayerMask.GetMask("Chao")))
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
        if(!meuColisor.IsTouchingLayers(LayerMask.GetMask("Escada")))
        {
            meuAnimator.SetBool("Escalando", false);
            meuRigidBody.gravityScale = gravidadeInicial; 
            return;
        }

       float subir = CrossPlatformInputManager.GetAxis("Horizontal"); 
       Vector2 direcaoDeSubida = new Vector2(meuRigidBody.velocity.x, subir * velocidadeDeSubida); 
       meuRigidBody.velocity = direcaoDeSubida;
       meuRigidBody.gravityScale = 0f;

       //Ativar animação escalada.
       bool jogadorTemVelocidadeVertical = Mathf.Abs(meuRigidBody.velocity.y) > Mathf.Epsilon;
       meuAnimator.SetBool("Escalando", jogadorTemVelocidadeVertical);

    }
}
