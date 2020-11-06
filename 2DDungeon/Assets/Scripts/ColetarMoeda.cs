using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetarMoeda : MonoBehaviour
{
    [SerializeField] AudioClip somDePegarMoeda;
    [SerializeField] int pontosPorMoeda = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	 FindObjectOfType<GerenciadorDeJogo>().AdicionarPontos(pontosPorMoeda);
    	 AudioSource.PlayClipAtPoint(somDePegarMoeda, Camera.main.transform.position);
    	 Destroy(gameObject);
    }
}
