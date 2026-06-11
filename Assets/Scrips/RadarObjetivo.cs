using UnityEngine;

public class RadarObjetivo : MonoBehaviour
{
    public Transform jugador;

    private Transform objetivo;

    void Update()
    {
        BuscarObjetivo();

        if (objetivo == null)
            return;

        Vector3 direccion =
            objetivo.position - jugador.position;

        float angulo =
            Mathf.Atan2(direccion.x, direccion.z)
            * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0, 0, -angulo);
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