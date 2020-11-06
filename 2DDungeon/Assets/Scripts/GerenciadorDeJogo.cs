using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GerenciadorDeJogo : MonoBehaviour
{
    [SerializeField] int vidas = 3;
    [SerializeField] int pontos = 0;
    [SerializeField] Text textoVidas;
    [SerializeField] Text textoPontos;

    private void Awake()
    {
    	int qtdGerenciadorDeJogo = FindObjectsOfType<GerenciadorDeJogo>().Length;

    	if(qtdGerenciadorDeJogo > 1)
    	{
    		Destroy(gameObject);
    	}
    	else
    	{
    		DontDestroyOnLoad(gameObject);
    	}
    }

    private void Start()
    {
    	textoVidas.text = vidas.ToString();
    	textoPontos.text = pontos.ToString();
    }

    public void AdicionarPontos(int pontosGanhos)
    {
    	pontos += pontosGanhos;
    	textoPontos.text = pontos.ToString();
    }

    public void ProcessarMorte()
    {
    	if(vidas > 1)
    	{
    		TirarVida();
    	}
    	else
    	{
    		ReiniciarJogo();
    	}
    }

    private void TirarVida()
    {
    	vidas--;
    	var faseAtual = SceneManager.GetActiveScene().buildIndex; 
    	SceneManager.LoadScene(faseAtual);
    	textoVidas.text = vidas.ToString();
    }

    private void ReiniciarJogo()
    {
    	SceneManager.LoadScene(0);
    	Destroy(gameObject);
    }
}
