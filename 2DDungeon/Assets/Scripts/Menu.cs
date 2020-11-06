using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void IniciarJogo(int level)
    {
    	SceneManager.LoadScene(level);
    }
}
