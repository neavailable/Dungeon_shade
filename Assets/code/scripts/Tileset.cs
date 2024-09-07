using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap), typeof(TilemapRenderer), typeof(Rigidbody2D))]
[RequireComponent(typeof(TilemapCollider2D), typeof(CompositeCollider2D))]


public class Tileset : MonoBehaviour
{
    public static System.Action touched; 


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) touched?.Invoke();
    }
}
