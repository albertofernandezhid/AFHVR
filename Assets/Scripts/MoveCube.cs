using UnityEngine;
using static OVRInput;

public class MoveCube : MonoBehaviour
{
    [SerializeField] private float speed = 1.04f;
    [SerializeField] private Controller controller;
    [SerializeField] private Renderer cubeRenderer;

    public GameObject Bullet;
    public float shootForce;
    public Transform shootPoint;
    void Update()
    {
        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
        transform.Translate(new Vector3(axis.x, 0, axis.y) * speed * Time.deltaTime);
        if (OVRInput.Get(OVRInput.Button.One)) transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (OVRInput.Get(OVRInput.Button.Two)) transform.Translate(Vector3.down * speed * Time.deltaTime);
        //if (OVRInput.Get(OVRInput.Touch.One)) transform.Translate(Vector3.up * speed * Time.deltaTime);
        //if (OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger)) transform.Translate(Vector3.down * speed * Time.deltaTime);
        float triggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
        cubeRenderer.material.color = Color.Lerp(Color.red, Color.green, triggerPress);

        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Instantiate(Bullet, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);
            }
            Destroy(Bullet, 2f);
        }
        Debug.Log(triggerPress);

        Debug.Log($"Eje x: {axis.x}, Eje y: {axis.y}");
    }
}
