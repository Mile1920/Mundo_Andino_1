using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Coleccionables del Nivel")]
    public List<string> objetosRequeridos = new List<string>
    {
        "Hoja de Coca",
        "Vela de Minero",
        "Casco de Minero"
    };

    [Header("UI")]
    public Text textoContador;
    public Text textoMensaje;
    public GameObject panelVictoria;

    [Header("Siguiente Nivel")]
    public string nombreMenuPrincipal = "menuPanel";

    private List<string> objetosRecolectados = new List<string>();
    private int totalRequeridos;
    
    public bool tieneMoneda = false;

    void Start()
    {
        totalRequeridos = objetosRequeridos.Count;

        if (panelVictoria != null)
            panelVictoria.SetActive(false);

        ActualizarUI();
    }

    public void RecolectarObjeto(string nombreObjeto)
    {
        if (!objetosRecolectados.Contains(nombreObjeto))
        {
            objetosRecolectados.Add(nombreObjeto);
            Debug.Log($"Recolectado: {nombreObjeto}");
            ActualizarUI();
            MostrarMensaje($"¡{nombreObjeto} obtenido!");

            if (objetosRecolectados.Count >= totalRequeridos)
            {
                ObtenerMoneda();
            }
        }
    }

    void ActualizarUI()
    {
        if (textoContador != null)
            textoContador.text =
                $"Ofrendas al Tío: {objetosRecolectados.Count}/{totalRequeridos}";
    }

    void MostrarMensaje(string mensaje)
    {
        if (textoMensaje != null)
        {
            textoMensaje.text = mensaje;
            CancelInvoke(nameof(OcultarMensaje));
            Invoke(nameof(OcultarMensaje), 2f);
        }
    }

    void OcultarMensaje()
    {
        if (textoMensaje != null)
            textoMensaje.text = "";
    }
    void ObtenerMoneda()
    {
        if (tieneMoneda)
            return;

        tieneMoneda = true;

        Debug.Log("Moneda de Plata obtenida");

        MostrarMensaje("¡Moneda de Plata obtenida!");

        Invoke(nameof(NivelCompletado), 2f);
    }

    void NivelCompletado()
    {
        Debug.Log("¡Nivel completado!");

        if (panelVictoria != null)
            panelVictoria.SetActive(true);

        MostrarMensaje("¡Felicidades! Has completado el nivel.");

        Invoke(nameof(VolverAlMenu), 5f);
    }

    void VolverAlMenu()
    {
        Debug.Log("VOLVIENDO AL MENU");
        SceneManager.LoadScene(nombreMenuPrincipal);
    }
}
