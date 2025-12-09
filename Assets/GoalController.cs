using UnityEngine;
using TMPro;

public class GoalController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text winText;       // Texto de victoria en el Canvas
    public float displayTime = 2f; // Tiempo que se muestra antes de reiniciar
    public TMP_Text coinsText; // Texto que muestra las monedas

    private void Start()
    {
        // Ocultar texto al inicio
        if (winText != null)
            winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Llegaste a la meta!");

            // Mostrar mensaje de victoria
            if (winText != null)
                winText.gameObject.SetActive(true);

            if (coinsText != null)
            coinsText.text = "Coins: 10"; // 👈 aquí defines el valor

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.StopRunning(); // 👈 ahora detiene al Player correctamente
            }
            
            // Reiniciar juego después de unos segundos
            Invoke("CallRestart", displayTime);
        }
    }

    void CallRestart()
    {
        // Ocultar texto antes de reiniciar
        if (winText != null)
            winText.gameObject.SetActive(false);

        if (coinsText != null)
            coinsText.text = "Coins: 0"; // 👈 aquí defines el valor

        
        // Llamar al Gamemaster para reiniciar
        GamemasterController gm = FindObjectOfType<GamemasterController>();
        if (gm != null)
            gm.RestartGame();
    }
}
