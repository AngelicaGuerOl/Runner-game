using UnityEngine;
using TMPro;

public class StarPlayer : MonoBehaviour
{
    public int stars = 0;

    [Header("UI")]
    public TMP_Text starsText;  
    public TMP_Text timeText;   

    public float elapsedTime = 0f;
    public bool isRunning = false;

    void Start()
    {
        UpdateStarsUI();
        UpdateTimeUI();
    }

    void Update()
    {
        if (!isRunning) return;

        // contar tiempo
        elapsedTime += Time.deltaTime;
        UpdateTimeUI();
    }

    public void AddStar()
    {
        stars++;
        UpdateStarsUI();
    }

    public void UpdateStarsUI()
    {
        if (starsText != null)
            starsText.text = "Estrellas: " + stars;
    }

    public void UpdateTimeUI()
    {
        if (timeText != null)
            timeText.text = "Tiempo: " + elapsedTime.ToString("F1") + "s";
    }

    public void StartRunning()
    {
        isRunning = true;
    }

    public void StopRunning()
    {
        isRunning = false;
    }
    public void ResetUI()
{
    stars = 0;
    elapsedTime = 0f;
    UpdateStarsUI();
    UpdateTimeUI();
}
}
