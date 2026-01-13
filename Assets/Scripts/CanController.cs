using System.Collections;
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
        if (!_cans.Contains(go)) return;

        _score += 10;
        _cans.Remove(go);

        scoreText.text = "Puntuación: " + _score;

        StartCoroutine(HandleCanLifecycle(go));
    }

    private IEnumerator HandleCanLifecycle(GameObject go)
    {
        yield return new WaitForSeconds(2f);

        MeshCollider meshCollider = go.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.enabled = false;
        }

        yield return new WaitForSeconds(3f);

        go.SetActive(false);
        Destroy(go);
    }
}