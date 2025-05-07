using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public ScoreManager scoreManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyHitbox"))
        {
            scoreManager.DecreaseProgressAmount(20);
        }
    }
}
