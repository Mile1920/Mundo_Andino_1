using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    [Header("Deteccion de Suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.3f;
    public LayerMask capaSuelo;

    private Rigidbody rb;
    private bool enSuelo;
    private float inputX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Bloquear rotacion para que no se caiga
        rb.constraints = RigidbodyConstraints.FreezeRotation |
                         RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        // Input horizontal
        inputX = Input.GetAxisRaw("Horizontal");

        // Detectar suelo
        enSuelo = Physics.CheckSphere(
            puntoSuelo != null ? puntoSuelo.position : transform.position - Vector3.up * 0.9f,
            radioSuelo,
            capaSuelo
        );

        // Salto
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, fuerzaSalto, 0f);
        }

        // Voltear el personaje segun direccion
        if (inputX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (inputX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate()
    {
        // Movimiento horizontal - mantener Z fijo
        rb.linearVelocity = new Vector3(
            inputX * velocidad,
            rb.linearVelocity.y,
            0f
        );
    }
}
