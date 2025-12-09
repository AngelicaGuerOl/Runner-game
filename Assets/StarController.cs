using UnityEngine;

public class StarController : MonoBehaviour
{
    public float spinSpeed = 100f;         // velocidad de giro
    public AudioClip collectSound;         // sonido al recoger

    void Update()
    {
        // Girar estrella
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StarPlayer player = other.GetComponent<StarPlayer>();
            if (player != null)
            {
                player.AddStar();  // aumenta contador
                if (collectSound != null)
                    AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            gameObject.SetActive(false); // desactivar estrella
        }
    }
}
