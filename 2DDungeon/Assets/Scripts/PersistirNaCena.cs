using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistirNaCena : MonoBehaviour
{
    int cenaInicial;

    private void Awake()
    {
    	int qntDePersistirNaCena = FindObjectsOfType<PersistirNaCena>().Length;
    	if(qntDePersistirNaCena > 1)
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
    	int cenaInicial = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
    	int cenaAtual = SceneManager.GetActiveScene().buildIndex;
    	if(cenaAtual != cenaInicial)
    	{
    		Destroy(gameObject);
    	}
    }
}
