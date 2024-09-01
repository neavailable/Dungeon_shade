using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class Player : Character
{
    private float start_time, end_time;

    private Collider2D player_collider;

    private List<Collider2D> enemy_colliders;

    [SerializeField] private Transform ladder_transform;


    private void Start() 
    {
        base.Start();

        ladder_transform = GameObject.Find("ladders").GetComponent<Transform>();
        
        start_time = -1f; end_time = 3f;

        player_collider = GetComponent<Collider2D>();

        enemy_colliders = new List<Collider2D>();

        find_all_enemies_colliders();
    }

    private void cath_keys()
    {
        if (current_state == states.is_rolling || current_state == states.is_getting_damage) return;


        if (Keyboard.current.aKey.isPressed) move_to_side(-1);

        else if (Keyboard.current.dKey.isPressed) move_to_side(1);

        else if (Keyboard.current.fKey.isPressed) start_rolling();

        else base.stand();
    }

    private void find_all_enemies_colliders()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag == "enemy") enemy_colliders.Add(obj.gameObject.GetComponent<Collider2D>());
        }
    }

    protected override void set_animation() => base.set_animation();
    
    public override void stand()
    {
        base.stand();


        foreach (Collider2D enemy_collider in enemy_colliders)
        {
            Physics2D.IgnoreCollision(player_collider, enemy_collider, false);
        }

        decrease_speed_after_rolling();
    }

    public void move_to_side(int direction_)
    {
        if (direction == -direction_) flip();

        direction = direction_;
        move();
    }

    public bool is_rolling() => current_state == states.is_rolling;


    public void start_rolling()
    {
        if (Time.time - start_time < end_time) return;


        current_state = states.is_rolling;

        animator.SetTrigger("is_rolling");
        
        foreach (Collider2D enemy_collider in enemy_colliders)
        {
            Physics2D.IgnoreCollision(player_collider, enemy_collider, true);
        }

        start_time = Time.time;

        increase_speed_while_rolling();
    }

    public void roll()
    {
        move();

        current_state = states.is_rolling;
    }

    private void Update()
    { 
        set_animation();
    }
};
