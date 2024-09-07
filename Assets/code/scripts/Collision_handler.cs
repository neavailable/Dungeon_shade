using System.Collections.Generic;
using UnityEngine;


public class Collision_handler : MonoBehaviour
{
    private List<Collider2D> enemy_colliders;


    private void Start()
    {
        enemy_colliders = new List<Collider2D>();

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag == "enemy") enemy_colliders.Add(obj.gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnEnable()
    {
        Player.player_started_roll += ignore_collision;
        Player.player_stoped_roll  += ignore_collision;
    }

    private void OnDisable()
    {
        Player.player_started_roll -= ignore_collision;
        Player.player_stoped_roll  -= ignore_collision;
    }

    private void ignore_collision(Collider2D player_collider, bool ignore_collision)
    {
        foreach (Collider2D enemy_collider in enemy_colliders)
        {
            Physics2D.IgnoreCollision(player_collider, enemy_collider, ignore_collision);
        }
    }
}
