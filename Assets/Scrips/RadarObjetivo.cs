using UnityEngine;

public class RadarObjetivo : MonoBehaviour
{
    public Transform jugador;

    private Transform objetivo;

    public float velocidadRotacion = 5f;

    void Update()
    {
        BuscarObjetivo();

        if (objetivo == null)
            return;

        Vector3 direccion =
            objetivo.position - jugador.position;

        direccion.y = 0f;

        float angulo =
            Mathf.Atan2(direccion.x, direccion.z)
            * Mathf.Rad2Deg;

        Quaternion rotacionObjetivo =
            Quaternion.Euler(0, 0, -angulo);

        transform.rotation =
            Quaternion.Lerp(
                transform.rotation,
                rotacionObjetivo,
                velocidadRotacion * Time.deltaTime
            );
    }

    void BuscarObjetivo()
    {
        GameObject[] coleccionables =
            GameObject.FindGameObjectsWithTag("Coleccionable");

        float distanciaMinima = Mathf.Infinity;

        Transform masCercano = null;

        foreach (GameObject obj in coleccionables)
        {
            float distancia =
                Vector3.Distance(
                    jugador.position,
                    obj.transform.position);

            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                masCercano = obj.transform;
            }
        }

        objetivo = masCercano;
    }
}