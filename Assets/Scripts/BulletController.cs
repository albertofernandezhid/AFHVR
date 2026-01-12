using System.Collections;
using UnityEngine;
using Oculus.Haptics;

public class BulletController : MonoBehaviour
{
    public GameObject Bullet;
    public float shootForce;
    public Transform shootPoint;
    public AudioSource bulletAudio;
    public AudioSource gunCockedAudio;
    public HapticClip clip1;
    private HapticClipPlayer player;

    private bool gunCocked = false;

    private void Awake()
    {
        player = new HapticClipPlayer(clip1);
    }

    void Update()
    {
        bool isHoldingHandTrigger = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

        // --- 1️⃣ Sonido de "cocked" una sola vez ---
        if (isHoldingHandTrigger && !gunCocked)
        {
            gunCockedAudio.Play();
            StartCoroutine(VibrateController(0.1f, 0.5f, 0.05f));
            gunCocked = true;
        }
        else if (!isHoldingHandTrigger)
        {
            gunCocked = false; // Se resetea cuando sueltas el gatillo de la mano
        }

        // --- 2️⃣ Disparo ---
        if (isHoldingHandTrigger && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // Reproduce sonido de bala sin cortar el anterior
            bulletAudio.PlayOneShot(bulletAudio.clip);

            // Instancia la bala y aplica fuerza
            Rigidbody rb = Instantiate(Bullet, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            player.Play(Oculus.Haptics.Controller.Right);
        }
    }

    private IEnumerator VibrateController(float frequency, float amplitude, float duration)
    {
        // Inicia vibración
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);

        // Espera el tiempo especificado
        yield return new WaitForSeconds(duration);

        // Detiene vibración
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
