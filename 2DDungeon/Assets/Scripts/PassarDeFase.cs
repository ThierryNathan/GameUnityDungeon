using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassarDeFase : MonoBehaviour
{
	[SerializeField] float esperaParaCarregarFase = 2f;
	[SerializeField] float fatorDeCameraLenta = 0.2f;
    [SerializeField] int proximaFase;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		StartCoroutine(CarregarProximaFase());
	}

    IEnumerator CarregarProximaFase()
    {
    	Time.timeScale = fatorDeCameraLenta;
    	yield return new WaitForSecondsRealtime(esperaParaCarregarFase);
    	Time.timeScale = 1f;

    	//int indiceDaFaseAtual = SceneManager.GetActiveScene().buildIndex;
    	SceneManager.LoadScene(proximaFase);
    }
}
