using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Character
{
    private bool on_ground;
    private Vector2 goal;

    [SerializeField] private float roll_lenght;


    private void Start() 
    {
        base.Start();

        on_ground = true;
    }

    protected override void stand()
    {
        direction = 0;

        current_state = states.is_standing;
    }

    protected override void set_animation() 
    {
        set_basic_animation();
    }

    private void move_left()
    {
        if (facing_right)
        {
            flip();
            facing_right = false;
        }

        direction = -1;
        move();
    }

    private void move_right()
    {
        if (!facing_right)
        {
            flip();
            facing_right = true;
        }

        direction = 1;
        move();
    }

    private void roll()
    {
        goal = new Vector2(transform.position.x + roll_lenght * direction, transform.position.y);
        
        direction = 1;
        current_state = states.is_rolling;

        animator.SetTrigger("is_rolling");
    }

    private void cath_keys()
    {
        if (Keyboard.current.aKey.isPressed) move_left();
        
        else if (Keyboard.current.dKey.isPressed) move_right();
        
        else if (Keyboard.current.fKey.isPressed) roll();
        
        else stand();
    }

    private void move_to()
    {
        move_right();

        current_state = states.is_rolling;
    }

    private void Update()
    {
        if (current_state == states.is_rolling) move_to();

        else cath_keys();
        
        set_animation();
    }
};
