using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private float umbralLanzamiento = 1.5f;
    [SerializeField] private float fuerza = 2f;
    [SerializeField] private float latencia = 0.5f;
    private float ultimoLanzamiento;
    //public HapticClip clip1;
    //private HapticClipPlayer player;
    //[SerializeField] private Oculus.Haptics.Controller controller;
    //public AudioSource audio;
    // void awake()
    // {
    //   player = new hapticclipplayer(clip1);
    // }

    void Update()
    {

        if (Time.time < (ultimoLanzamiento + latencia))
        {
            Debug.Log(ultimoLanzamiento + latencia);
            return;
        }
        else
        {
            Vector3 velocidadControlador = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

            Quaternion rotacionControlador = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);


            // magnitude devuelve la longitud del vector que es la raíz cuadrada de (x*x+y*y+z*z).

            float velocidad = velocidadControlador.magnitude;

            //Primero, normalizamos el vector velocidad con normalized  convertirlo en un vector unitario, es decir, un vector que apunta en la misma dirección pero con una magnitud de 1  y después usamos el método Dot para realizar el producto escalar de los dos vectores.

            // Para vectores normalizados, Dot devuelve 1 si apuntan exactamente en la misma dirección, -1 si apuntan en direcciones completamente opuestas.


            bool esLanzando = Vector3.Dot(velocidadControlador.normalized, rotacionControlador * Vector3.forward) > 0;

            if (velocidad > umbralLanzamiento && esLanzando)
            {
                Vector3 posicionControlador = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

                LanzarBala(posicionControlador, rotacionControlador, velocidadControlador);

                ultimoLanzamiento = Time.time;
                //StartCoroutine(VibrateForSeconds(0.5f, 1f, 0.3f, OVRInput.Controller.RTouch));
                // OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);

                // audio.Play();

                // player.Play(Oculus.Haptics.Controller.Right);

            }
        }
    }
    private void LanzarBala(Vector3 posicionControlador, Quaternion rotacionControlador, Vector3 velocidadControlador)
    {
        GameObject bala = Instantiate(balaPrefab, posicionControlador, rotacionControlador);

        Rigidbody rb = bala.GetComponent<Rigidbody>();

        rb.AddForce(rotacionControlador * Vector3.forward * velocidadControlador.magnitude * fuerza, ForceMode.VelocityChange);

        //Destroy(bala, 3f);
    }
    //  IEnumerator VibrateForSeconds(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    //{

    //  OVRInput.SetControllerVibration(frequency, amplitude, controller);
    //yield return new WaitForSeconds(duration);

    //OVRInput.SetControllerVibration(0, 0, controller);
    //}
}
