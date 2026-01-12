using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanController : MonoBehaviour
{
    private List<GameObject> _cans; // Lista de elementos de objetivos
    private int _score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        _cans = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cans"));
    }
    public void TargetHit(GameObject go)
    {
        Debug.Log("Score: " + _score);
        if (_cans.Contains(go))
        {
            _score += 10;
            _cans.Remove(go);
            Debug.Log("Score " + _score);
            Destroy(go, 5);
            scoreText.text = "Score: " + _score;
        }
    }
}
