using UnityEngine;

public class ObjetoRecolectable : MonoBehaviour
{
    public enum TipoObjeto
    {
        Coca,
        Casco,
        Vela
    }

    public TipoObjeto tipo;

    public void Interact()
    {
        InventarioJugador inventario =
            FindObjectOfType<InventarioJugador>();

        switch (tipo)
        {
            case TipoObjeto.Coca:
                inventario.tieneCoca = true;
                break;

            case TipoObjeto.Casco:
                inventario.tieneCasco = true;
                break;

            case TipoObjeto.Vela:
                inventario.tieneVela = true;
                break;
        }

        Destroy(gameObject);
    }
}