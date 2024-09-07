using System.Collections.Generic;
using UnityEngine;


public class Collision_handler : MonoBehaviour
{
    private List<Collider2D> enemy_colliders;
    private Collider2D player_collider;

    private void Start()
    {
        enemy_colliders = new List<Collider2D>();

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag == "enemy") enemy_colliders.Add(obj.gameObject.GetComponent<Collider2D>());

            else if (obj.tag == "Player") player_collider = obj.gameObject.GetComponent<Collider2D>();
        }
    }

    private void OnEnable() => Player.player_roll += ignore_collision;
    

    private void OnDisable() => Player.player_roll -= ignore_collision;
    

    private void ignore_collision(bool ignore_collision)
    {
        foreach (Collider2D enemy_collider in enemy_colliders)
        {
            Debug.Log(1);
            Physics2D.IgnoreCollision(player_collider, enemy_collider, ignore_collision);
        }
    }
}
