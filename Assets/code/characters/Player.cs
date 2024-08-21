using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Character
{
    private bool on_ground;

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

    private void cath_keys()
    {
        if (Keyboard.current.aKey.isPressed) move_left();

        else if (Keyboard.current.dKey.isPressed) move_right();

        else stand();
    }

    private void Update()
    {
        cath_keys();

        set_animation();
    }
};
