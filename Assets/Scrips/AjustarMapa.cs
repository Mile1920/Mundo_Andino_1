using UnityEngine;

public class AjustarMapa : MonoBehaviour
{
    void Start()
    {
        // Calcula el tamaño real del mapa
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer r in renderers)
            bounds.Encapsulate(r.bounds);

        // Centra el mapa en el origen
        transform.position = -bounds.center;
        transform.position = new Vector3(
            transform.position.x, 0, 
            transform.position.z
        );

        // Ajusta cámara automáticamente
        float mapSize = Mathf.Max(bounds.size.x, bounds.size.z);
        Camera.main.transform.position = new Vector3(
            bounds.center.x, 
            mapSize * 1.2f, 
            bounds.center.z
        );
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);

        Debug.Log("Tamaño del mapa: " + mapSize);
    }
}
