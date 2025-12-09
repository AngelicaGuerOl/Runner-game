using UnityEngine;
using TMPro;

public class GamemasterController : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text message;
    public GameObject readyButton;
    public GameObject[] stars;

    public bool gameStarted = false;
    public float startTime = 0f;

    void Start()
    {
        canvas.SetActive(false);
        readyButton.SetActive(false);

        stars = GameObject.FindGameObjectsWithTag("Star");
        foreach (var star in stars)
            star.SetActive(false);
    }

    public void PlayerReady()
    {
        gameStarted = true;
        startTime = Time.time;
        canvas.SetActive(false);
        readyButton.SetActive(false);

        foreach (var star in stars)
            star.SetActive(true);
    }

    public bool AllStarsCollected()
    {
        foreach (var star in stars)
        {
            if (star.activeSelf) return false;
        }
        return true;
    }

    // -------- Triggers para mostrar mensaje y botón --------
    private void OnTriggerEnter(Collider other)
    {
        if (!gameStarted && other.CompareTag("Player"))
        {
            canvas.SetActive(true);
            readyButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!gameStarted && other.CompareTag("Player"))
        {
            canvas.SetActive(false);
            readyButton.SetActive(false);
        }
    }

    // -------- Función conectada al botón Ready --------
    public void ReadyButtonClicked()
{
    // Desactivar collider para que Player no quede atrapado
    Collider col = GetComponent<Collider>();
    if (col != null)
        col.enabled = false;

    // Iniciar movimiento del Player
    PlayerController player = FindObjectOfType<PlayerController>();
    if (player != null)
        player.StartRunning();

    // 🔹 Iniciar conteo de tiempo en StarPlayer
    StarPlayer starPlayer = FindObjectOfType<StarPlayer>();
    if (starPlayer != null)
        starPlayer.StartRunning();

    PlayerReady();
}

    public void RestartGame()
{
    PlayerController player = FindObjectOfType<PlayerController>();
    if (player != null)
        player.ResetPlayer();

    Collider col = GetComponent<Collider>();
    if (col != null)
        col.enabled = true;

    canvas.SetActive(false);
    readyButton.SetActive(false);

    foreach (var star in stars)
        star.SetActive(false);

    StarPlayer starPlayer = FindObjectOfType<StarPlayer>();
    if (starPlayer != null)
    {
        starPlayer.ResetUI();
        starPlayer.StopRunning(); // 🔹 detener conteo al reiniciar
    }

    gameStarted = false;
}





}
