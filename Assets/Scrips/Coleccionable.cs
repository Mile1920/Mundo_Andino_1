using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [Header("Configuracion")]
    public string nombreObjeto = "Hoja de Coca";
    public float velocidadRotacion = 90f;
    public float amplitudFlotacion = 0.3f;
    public float velocidadFlotacion = 2f;

    private Vector3 posicionInicial;
    private GameManager gameManager;

    void Start()
    {
        posicionInicial = transform.position;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Rotacion continua
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);

        // Flotacion arriba y abajo
        float nuevaY = posicionInicial.y +
            Mathf.Sin(Time.time * velocidadFlotacion) * amplitudFlotacion;
        transform.position = new Vector3(
            transform.position.x,
            nuevaY,
            transform.position.z
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
                gameManager.RecolectarObjeto(nombreObjeto);

            Destroy(gameObject);
        }
    }
}
