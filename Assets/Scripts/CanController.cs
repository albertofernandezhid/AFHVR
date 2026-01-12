using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanController : MonoBehaviour
{
    private List<GameObject> _cans; 
    private int _score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        _cans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cans"));
    }
    public void TargetHit(GameObject go)
    {
        Debug.Log("Puntuación: " + _score);
        if (_cans.Contains(go))
        {
            _score += 10;
            _cans.Remove(go);
            Debug.Log("Puntuación " + _score);
            Destroy(go, 5);
            scoreText.text = "Puntuación: " + _score;
        }
    }
}
