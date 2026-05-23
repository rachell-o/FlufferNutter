using UnityEngine;

public class FoodCollision : MonoBehaviour
{
    [SerializeField] private bool isGood;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skillet"))
        {
            if (isGood)
                FoodScoreManager.Instance.AddScore(1);
            else
                FoodScoreManager.Instance.AddScore(-1);

            Destroy(gameObject);
        }
    }

    public void SetGood(bool value)
    {
        isGood = value;
    }
}