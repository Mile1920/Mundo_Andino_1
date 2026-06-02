using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject mapaPanel;

    void Start()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        mapaPanel.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void AbrirMapa()
    {
        SceneManager.LoadScene("MapaScene");
        /*menuPanel.SetActive(false);
        mapaPanel.SetActive(true);*/
    }

    public void AbrirOpciones()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void Volver()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        mapaPanel.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
