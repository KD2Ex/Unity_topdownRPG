using UnityEngine;

public class Runes : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    private bool playerNearby;

    private float AlphaColor => sprite.color.a;
    private Color SpriteColor => sprite.color;

    void Update()
    {
        if (playerNearby)
        {
            if (AlphaColor >= 1f) return;
            SetAlpha(AlphaColor + Time.deltaTime);
        }
        else
        {
            if (AlphaColor <= 0f) return;
            SetAlpha(AlphaColor - Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = false;
    }

    private void SetAlpha(float value)
    {
        sprite.color = new Color(SpriteColor.r, SpriteColor.g, SpriteColor.b, value);
    }
}
