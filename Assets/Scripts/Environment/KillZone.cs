using UnityEngine;

public class KillZone : MonoBehaviour
{
    public Transform respawnPoint; // Assign this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            PlayerController controller = other.GetComponent<PlayerController>();

            if (playerHealth != null && controller != null)
            {
                if (playerHealth.currentHealth > 1)
                {
                    // ✨ Only respawn first
                    playerHealth.RespawnDirectly(respawnPoint);

                    // ✅ Wait just a moment to apply damage, so animation isn't doubled
                    controller.StartCoroutine(DelayedTakeDamage(playerHealth, controller, 0.05f));
                }
                else
                {
                    // Let death handle it normally
                    playerHealth.TakeDamage(1);
                }
            }
        }
    }

    private System.Collections.IEnumerator DelayedTakeDamage(Health playerHealth, PlayerController controller, float delay)
    {
        yield return new WaitForSeconds(delay);

        playerHealth.TakeDamage(1);

        // This only gets called if health remains > 0 after damage
        if (playerHealth.currentHealth > 0)
        {
            controller.Invoke(nameof(controller.FinishRespawn), 0.8f);
        }
    }
}
