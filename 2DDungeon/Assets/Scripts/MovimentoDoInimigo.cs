using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDoInimigo : MonoBehaviour
{
	[SerializeField] float velocidadeDeMovimento = 1f;
	Rigidbody2D meuRigidBody2D;
	bool estaOlhandoParaDireita;  


    // Start is called before the first frame update
    void Start()
    {
        meuRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (EstaOlhandoParaDireita())
    	{
    		meuRigidBody2D.velocity = new Vector2(velocidadeDeMovimento, 0);
    	}		   
    	else
    	{
    		meuRigidBody2D.velocity = new Vector2(-velocidadeDeMovimento, 0);
    	}
    }

    bool EstaOlhandoParaDireita()
    {
    	return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(meuRigidBody2D.velocity.x)), 1f); 
    }
}