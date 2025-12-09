using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform center_pos;
    [SerializeField] Transform left_pos;
    [SerializeField] Transform right_pos;
    public Animator player_Animator;
    [SerializeField] Rigidbody rb;

    public Transform startPosition; // posición inicial al presionar Ready

    int currunt_pos = 0; // 0 = Center, 1 = Left, 2 = Right
    public float side_speed = 5f;
    public float running_speed = 5f;
    public float walk_speed = 3f;
    public float jump_height = 4f;
    public float jump_duration = 0.6f;
    public float jump_forward = 3f;

    public bool isGameOver = false;
    public bool isGameStarted = false;
    public bool isMovingSide = false;
    public bool isJumping = false;

    void Start()
    {
        currunt_pos = 0;
        isGameOver = false;
        isGameStarted = false;

        // Opcional: iniciar en una posición predeterminada
        if (startPosition != null)
            transform.position = startPosition.position;
    }

    void Update()
    {
        if (isGameOver) return;

        if (!isGameStarted)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(h, 0, v).normalized;

            if (move.magnitude > 0.1f)
            {
                rb.MovePosition(transform.position + move * walk_speed * Time.deltaTime);
                transform.forward = move;
                player_Animator.SetBool("isWalking", true);
            }
            else
            {
                player_Animator.SetBool("isWalking", false);
            }
            return;
        }

        // Solo correr si el juego sigue activo
        if (isGameStarted)
        {
            rb.MovePosition(rb.position + Vector3.forward * running_speed * Time.deltaTime);

            if (!isMovingSide)
            {
                if (currunt_pos == 0)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        StartCoroutine(MoveToPosition(left_pos.position.x, 1));
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                        StartCoroutine(MoveToPosition(right_pos.position.x, 2));
                }
                else if (currunt_pos == 1 && Input.GetKeyDown(KeyCode.RightArrow))
                    StartCoroutine(MoveToPosition(center_pos.position.x, 0));
                else if (currunt_pos == 2 && Input.GetKeyDown(KeyCode.LeftArrow))
                    StartCoroutine(MoveToPosition(center_pos.position.x, 0));
            }

            if (!isJumping && Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(Jump());
        }
    }



    IEnumerator MoveToPosition(float targetX, int newPos)
    {
        isMovingSide = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(targetX, startPos.y, startPos.z);
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * side_speed;
            transform.position = Vector3.Lerp(startPos, endPos, elapsed);
            yield return null;
        }

        currunt_pos = newPos;
        isMovingSide = false;
    }

    IEnumerator Jump()
    {
        isJumping = true;
        player_Animator.SetBool("isJumping", true);

        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < jump_duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / jump_duration;

            float yOffset = Mathf.Sin(progress * Mathf.PI) * jump_height;
            float zOffset = Mathf.Lerp(0f, jump_forward, progress);

            transform.position = new Vector3(
                startPos.x,
                startPos.y + yOffset,
                startPos.z + zOffset
            );

            yield return null;
        }

        isJumping = false;
        player_Animator.SetBool("isJumping", false);
    }

    public void StartRunning()
    {
        isGameStarted = true;
        player_Animator.SetBool("isRunning", true);

        if (startPosition != null)
        {
            Vector3 safePos = startPosition.position + new Vector3(0f, 0f, 1f);
            rb.position = safePos;
            rb.velocity = Vector3.zero; // elimina cualquier velocidad residual
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("object"))
        {
            isGameOver = true;
            isGameStarted = false;
            player_Animator.applyRootMotion = true;
            player_Animator.SetBool("isDied", true);

            // Reiniciar todo después de 1.5 segundos
            Invoke("CallRestart", 1.8f);
        }
    }

    void CallRestart()
    {
        GamemasterController gm = FindObjectOfType<GamemasterController>();
        if (gm != null)
            gm.RestartGame();
    }


    public void ResetPlayer()
    {
        // Reset de estado
        isGameOver = false;
        isGameStarted = false;
        isMovingSide = false;
        isJumping = false;
        currunt_pos = 0;

        // Reset de animaciones: todo apagado, solo Idle
        player_Animator.applyRootMotion = false;
        player_Animator.SetBool("isDied", false);
        player_Animator.SetBool("isWalking", false);
        player_Animator.SetBool("isRunning", false);
        player_Animator.SetBool("isJumping", false);

        // Opcional: asegurarnos que el Animator muestre Idle
        player_Animator.Play("idle"); // reemplaza "Idle" con el nombre exacto de tu animación de Idle

        // Reset de posición y física
        if (startPosition != null)
            rb.position = startPosition.position;
        rb.velocity = Vector3.zero;

        // Restaurar rotación si la animación de muerte la alteró
        transform.rotation = Quaternion.identity;
    }
    public void StopRunning()
    {
        isGameOver = true;
        isGameStarted = false;

        // Detener animaciones
        player_Animator.SetBool("isRunning", false);
        player_Animator.SetBool("isWalking", true);
        player_Animator.SetBool("isJumping", false);

        // Detener física residual
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Opcional: cancelar corrutinas activas
        StopAllCoroutines();
    }


}
