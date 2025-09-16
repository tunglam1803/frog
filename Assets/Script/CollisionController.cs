using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("DeathZone"))
        {
            animator.SetTrigger("is_hitted");
            if (playerController != null)
            {
                StartCoroutine(ResetPlayerPositionWithDelay(0.5f));
            }
        }
    }

    private IEnumerator ResetPlayerPositionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerController.transform.position = playerController.initialPosition;
        playerController.rb.velocity = Vector2.zero;
    }
}
