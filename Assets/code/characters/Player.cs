using UnityEngine;


public class Player : Character
{
    [SerializeField] private float running_speed, rolling_speed, climbing_speed;

    private float start_time, end_time, ladder_world_y;

    private bool can_climb;

    private Rigidbody2D rigidbody;

    private Collider2D player_collider;

    public static System.Action<bool> player_roll;


    private void Start() 
    {
        base.Start();

        current_speed = running_speed;

        start_time = -3f; end_time = -start_time;

        can_climb = false;

        rigidbody = GetComponent<Rigidbody2D>();

        player_collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Ladder.touched += set_can_climb;

        Ladder.didnt_touch += () => can_climb = false;
        Ladder.didnt_touch += stop_climbing;

        Tileset.touched += () =>
        {
            if (direction_y == -1) stand();
        };

    }

    private void OnDisable()
    {
        Ladder.touched -= set_can_climb;

        Ladder.didnt_touch -= () => can_climb = false;
        Ladder.didnt_touch -= stop_climbing;

        Tileset.touched -= () =>
        {
            if (direction_y == -1) stand();
        };

        can_climb = false;
    }

    private void set_can_climb(float ladder_world_y_)
    {
        ladder_world_y = ladder_world_y_;

        can_climb = true;
    }

    private void stop_climbing()
    {
        direction_y = 0;
        rigidbody.gravityScale = 1;

        stand();
    }

    private void set_running_speed() => current_speed = running_speed;

    private void set_rolling_speed() => current_speed = rolling_speed;

    protected override void set_animation() => base.set_animation();

    public override void stand()
    {
        base.stand();

        player_roll?.Invoke(false);

        set_running_speed();
    }

    public void move_x(int direction_)
    {
        if (direction_x == -direction_) flip();


        direction_x = direction_; direction_y = 0;

        move();
    }
    
    public void start_rolling()
    {
        if (Time.time - start_time < end_time) return;


        current_state = states.is_rolling;

        animator.SetTrigger("is_rolling");

        player_roll?.Invoke(true);

        start_time = Time.time;

        set_rolling_speed();
    }

    public void roll()  
    {
        move();

        current_state = states.is_rolling;
    }

    public void start_climbing()
    {
        if (!can_climb || current_state == states.is_climbing) return;


        if (transform.TransformPoint(transform.position).y > ladder_world_y)
        {
            direction_y = -1;

            transform.position = new Vector2(transform.position.x, transform.position.y + direction_y);
        }

        else direction_y  = 1;

        rigidbody.gravityScale = 0;

        current_state = states.is_climbing;
        animator.SetTrigger("is_climbing");
    }

    public void base_stand() => base.stand();

    public bool is_getting_damage() => current_state == states.is_getting_damage;

    public bool is_rolling() => current_state == states.is_rolling;

    public bool is_climbing() => current_state == states.is_climbing;

    public void climb()
    {
        move();

        current_state = states.is_climbing;
    }

    private void Update() => set_animation();
};
