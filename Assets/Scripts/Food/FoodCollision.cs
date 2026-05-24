using UnityEngine;

public class FoodCollision : MonoBehaviour
{
    [SerializeField] private bool isGood;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip badFoodSound;
    [SerializeField] private AudioClip goodFoodSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skillet"))
        {

        if (isGood)
        {
            FoodScoreManager.Instance.AddScore(1);
            
            AudioSource.PlayClipAtPoint(
                goodFoodSound,
                Camera.main.transform.position,
                1.0f
            );
        }
        else
        {
            FoodScoreManager.Instance.AddScore(-1);

            AudioSource.PlayClipAtPoint(
                badFoodSound,
                Camera.main.transform.position,
                0.15f
            );
        }

            // Instantiate explosion effect as child of skillet, "other" is Skillet
            GameObject explosion = Instantiate(
                explosionPrefab,
                other.transform
            );

            // Set local position relative to skillet
            explosion.transform.localPosition =
                new Vector3(-0.08f, 1.5f, -1f);

            // Set scale
            //explosion.transform.localScale =
            //    new Vector3(1f, 1f, 1f);

            // Destroy after 0.1 seconds
            Destroy(explosion, 0.1f);

            Destroy(gameObject);
        }
    }

    public void SetGood(bool value)
    {
        isGood = value;
    }
}