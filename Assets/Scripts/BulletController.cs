using System.Collections;
using UnityEngine;
using Oculus.Haptics;

public class BulletController : MonoBehaviour
{
    public GameObject Bullet;
    public float shootForce;
    public float impactForce;
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

        if (isHoldingHandTrigger && !gunCocked)
        {
            gunCockedAudio.Play();
            StartCoroutine(VibrateController(0.1f, 0.5f, 0.05f));
            gunCocked = true;
        }
        else if (!isHoldingHandTrigger)
        {
            gunCocked = false;
        }

        if (isHoldingHandTrigger && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            bulletAudio.PlayOneShot(bulletAudio.clip);

            GameObject bulletInstance = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();

            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);

            BulletImpact impact = bulletInstance.AddComponent<BulletImpact>();
            impact.force = impactForce;

            player.Play(Oculus.Haptics.Controller.Right);
        }
    }

    private IEnumerator VibrateController(float frequency, float amplitude, float duration)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
