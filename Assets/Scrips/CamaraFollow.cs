using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform jugador;
    public float offsetX = 0f;
    public float offsetY = 2f;
    public float velocidadCamara = 5f;

    void LateUpdate()
    {
        if (jugador == null) return;

        Vector3 posObjetivo = new Vector3(
            jugador.position.x + offsetX,
            jugador.position.y + offsetY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posObjetivo,
            velocidadCamara * Time.deltaTime
        );
    }
}