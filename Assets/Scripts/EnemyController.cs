using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    // Tiempos configurables
    public float idle2Interval = 5f;  // cada 5 segundos se activa Idle2
    public float idle2Duration = 3f;  // dura 3 segundos la animación Idle2

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Idle2Routine());
    }

    IEnumerator Idle2Routine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(idle2Interval);

            // Activamos la animación Idle2
            animator.SetBool("Idle2", true);

            // Esperamos 3 segundos manteniendo la animación
            yield return new WaitForSeconds(idle2Duration);

            // Volver al Idle normal
            animator.SetBool("Idle2", false);
        }
    }

    // -----------------------------
    //     DETECCIÓN DE DISPAROS
    // -----------------------------
    private void OnCollisionEnter(Collision collision)
    {
        // Comprueba que te golpea la bala
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            isDead = true;

            // Activa la animación de muerte
            animator.SetBool("IsDead", true);

            // Opcional: destruir enemigo después de que termine la animación
            Destroy(gameObject, 5f);
        }
    }
}
