using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subir : MonoBehaviour
{
    [SerializeField] float velocidadeDeSubida = 0.2f;

    private void Update()
    {
    	float movimentoVertical = velocidadeDeSubida * Time.deltaTime;
    	transform.Translate(new Vector2(0f, movimentoVertical));
    }
}
