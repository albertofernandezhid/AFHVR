using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public CanController canController;

    void OnCollisionEnter(Collision obj)
    {
        Debug.Log("Derribado"); 
        canController.TargetHit(obj.gameObject);
    }
}
