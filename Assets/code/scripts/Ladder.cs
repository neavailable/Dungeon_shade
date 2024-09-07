using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(PlatformEffector2D))]


public class Ladder : MonoBehaviour
{
    public static System.Action<float> touched;
    public static System.Action didnt_touch;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float world_y = transform.TransformPoint(transform.position).y;
            touched?.Invoke(world_y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) didnt_touch?.Invoke();
    }
}
