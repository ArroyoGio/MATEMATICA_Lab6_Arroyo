using UnityEngine;

public class GameManagerPlane : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private int hits = 0;
    [SerializeField] private int maxHits = 3;
    [SerializeField] private int puntosPorSegundo = 10;

    private bool isGameOver = false;

    private void Update()
    {
        if (isGameOver) return;

        score += Mathf.RoundToInt(Time.deltaTime * puntosPorSegundo);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameOver) return;

        hits++;
        if (hits >= maxHits)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER, Score final: " + score);
    }

}
