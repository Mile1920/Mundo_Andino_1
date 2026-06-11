using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FpsController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadCaminar = 4f;
    public float velocidadCorrer = 7f;

    [Header("Salto")]
    public float fuerzaSalto = 5f;
    public float gravedad = -20f;

    [Header("Agacharse")]
    public float alturaNormal = 2f;
    public float alturaAgachado = 1f;

    [Header("Mouse")]
    public float sensibilidadMouse = 2f;
    public float limiteVertical = 80f;

    [Header("Referencias")]
    public Transform camaraJugador;
    public Light linterna;

    [Header("Interacción")]
    public float distanciaInteraccion = 3f;

    private CharacterController cc;
    private Vector3 velocidadVertical;
    private float rotacionX;
    private bool estabaEnSuelo; // para evitar el frame de delay de isGrounded

    void Start()
    {
        cc = GetComponent<CharacterController>();

        if (camaraJugador == null)
        {
            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null)
                camaraJugador = cam.transform;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Mirar();
        Mover();
        Saltar();
        Agacharse();
        ControlLinterna();
        Interactuar();
    }

    void Mirar()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensibilidadMouse;

        transform.Rotate(Vector3.up * mouseX);

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -limiteVertical, limiteVertical);

        camaraJugador.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }

    void Mover()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = (transform.right * h + transform.forward * v).normalized;

        float velocidadActual = Input.GetKey(KeyCode.LeftShift)
            ? velocidadCorrer
            : velocidadCaminar;

        cc.Move(movimiento * velocidadActual * Time.deltaTime);
    }

    void Saltar()
    {
        bool enSuelo = cc.isGrounded || estabaEnSuelo; // doble verificación

        if (enSuelo && velocidadVertical.y < 0)
            velocidadVertical.y = -2f;

        if (enSuelo && Input.GetButtonDown("Jump"))
            velocidadVertical.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);

        velocidadVertical.y += gravedad * Time.deltaTime;
        cc.Move(velocidadVertical * Time.deltaTime);

        estabaEnSuelo = cc.isGrounded; // guarda el estado para el siguiente frame
    }

    void Agacharse()
    {
        cc.height = Input.GetKey(KeyCode.LeftControl) ? alturaAgachado : alturaNormal;
    }

    void ControlLinterna()
    {
        if (linterna == null) return;

        if (Input.GetKeyDown(KeyCode.F))
            linterna.enabled = !linterna.enabled;
    }

    void Interactuar()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        RaycastHit hit;

        if (Physics.Raycast(camaraJugador.position, camaraJugador.forward, out hit, distanciaInteraccion))
        {
            Debug.Log("Interactuando con: " + hit.collider.name);
            hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }
}