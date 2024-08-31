using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class Player : Character
{
    private Collider2D player_collider;

    private List<Collider2D> enemy_colliders;

    private void Start() 
    {
        base.Start();

        player_collider = GetComponent<Collider2D>();

        enemy_colliders = new List<Collider2D>();

        find_all_enemies_colliders();
    }

    protected override void stand()
    {
        base.stand();

        foreach (Collider2D enemy_collider in enemy_colliders)
        {
            Physics2D.IgnoreCollision(player_collider, enemy_collider, false);
        }
    }

    protected override void set_animation() 
    {
        set_basic_animation();
    }

    private void find_all_enemies_colliders()
    {
        foreach ( GameObject obj in FindObjectsOfType<GameObject>() )
        {
            if (obj.tag == "enemy") enemy_colliders.Add( obj.gameObject.GetComponent<Collider2D>() );
        }
    }

    private void move_to_side(int direction_)
    {
        if (direction == -direction_) flip();

        direction = direction_;
        move();
    }

    private void roll()
    {        
        current_state = states.is_rolling;

        animator.SetTrigger("is_rolling");

        foreach (Collider2D enemy_collider in enemy_colliders)
        {
            Physics2D.IgnoreCollision(player_collider, enemy_collider, true);
        }
    }

    private void cath_keys()
    {
        if (Keyboard.current.aKey.isPressed) move_to_side(-1);
        
        else if (Keyboard.current.dKey.isPressed) move_to_side(1);
        
        else if (Keyboard.current.fKey.isPressed) roll();
        
        else stand();
    }

    private void move_to()
    {
        move();

        current_state = states.is_rolling;
    }

    private void Update()
    {
        if (current_state == states.is_rolling) move_to();

        else cath_keys();
        
        set_animation();
    }
};
