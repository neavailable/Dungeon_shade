using UnityEngine;


public abstract class Character : Moving_item
{
    protected enum states { do_nothing, is_standing, is_running, is_attacking, is_getting_damage, is_climbing_up, is_climbing_down, is_rolling };
    protected states current_state;

    [SerializeField] private float speed;

    protected int direction;
    protected bool facing_right;

    private Get_damage get_damage_script;

    protected Animator animator;


    protected void Start() 
    {
        current_state = states.is_standing;

        direction = 0;
        facing_right = true;

        get_damage_script = GameObject.Find("get_damage_animation_1").GetComponent<Get_damage>();

        animator = GetComponent<Animator>();
    }

    protected virtual void stand() {}

    protected virtual void set_animation() {}

    protected override void set_basic_animation()
    {
        if (current_state != states.is_getting_damage) GetComponent<Animator>().SetInteger("state", (int) current_state);
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
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void get_damage()
    {
        get_damage_script.set_pinned_object(this);

        current_state = states.is_getting_damage;
    }

    public void end_getting_damage()
    {
        current_state = states.is_standing;
    }

    private void Update() {}
}
