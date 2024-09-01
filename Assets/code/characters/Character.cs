using UnityEngine;


public abstract class Character : Moving_item
{
    private Get_damage get_damage_script;

    protected enum states { do_nothing, is_standing, is_running, is_attacking, is_getting_damage, is_climbing_up, is_climbing_down, is_rolling };
    protected states current_state;

    [SerializeField] protected float speed;

    protected int direction;

    protected Animator animator;


    protected void Start() 
    {
        current_state = states.is_standing;

        direction = 1;

        get_damage_script = GameObject.Find("get_damage_animation_1").GetComponent<Get_damage>();

        animator = GetComponent<Animator>();
    }

    protected virtual void set_animation() => set_basic_animation();
    

    protected override void set_basic_animation()
    {
        if (current_state != states.is_getting_damage) GetComponent<Animator>().SetInteger("state", (int)current_state);
    }

    protected override void move()
    {
        Vector2 position = transform.position;
        position.x += speed * direction;
        transform.position = position;

        current_state = states.is_running;
    }

    protected void flip()
    {
        Vector2 scaler = transform.localScale;
        scaler.x = -scaler.x;
        transform.localScale = scaler;
    }

    protected void decrease_speed_after_rolling() => speed = 0.015f;
    

    protected void increase_speed_while_rolling() => speed = 0.02f;
    

    public virtual void stand() => current_state = states.is_standing;
    

    public void get_damage()
    {
        get_damage_script.set_pinned_object(this);

        if (current_state != states.is_rolling) current_state = states.is_getting_damage;
    }

    public void end_getting_damage() => current_state = states.is_standing;
}