using UnityEngine;
 

public abstract class Enemy : Character
{
    private bool change_action_when_rest, change_action_when_follow, change_action_when_attack;
    
    private float start_time_of_standing, end_time_of_standing;
    [SerializeField] private float left_position_border, right_position_border, spot_box_width, spot_box_height;

    private float attack_box_width, attack_box_height;

    private int standing_probility, damage;

    private Player player;

    private Vector2 goal;


    // notice_x and notice_y are sides of box. when player come to the box enemy start move to player

    //  notice_box_width
    //      <---->
    //    __________
    //   |          |
    //   |          |^
    //   |    E     || notice_box_height
    //   |          |v
    //   |__________|

    //    E - Enemy


    protected void Start()
    {
        base.Start();

        change_action_when_rest = true; change_action_when_follow = true; change_action_when_attack = true;

        end_time_of_standing = 1f;

        attack_box_width = 1.8f; attack_box_height = 0.5f;

        standing_probility = 30; damage = 0;

        start_time_of_standing = Time.time;

        player = GameObject.Find("player").GetComponent<Player>();
    }

    protected override void set_animation() 
    {
        set_basic_animation();
    }

    private bool is_in_box(Vector2 object_position, float box_width, float box_height)
    {
        return (transform.position.x < object_position.x + box_width)  &&
               (transform.position.x > object_position.x - box_width)  &&
               (transform.position.y < object_position.y + box_height) &&
               (transform.position.y > object_position.y - box_height) ?
               true : false;
    }

    private bool has_player_been_spotted()
    {
        return is_in_box(player.transform.position, spot_box_width, spot_box_height) ? true : false;
    }

    private bool should_attack_player()
    {
        return is_in_box(player.transform.position, attack_box_width, attack_box_height) || current_state == states.is_attacking ? true : false;
    }

    private void should_flip()
    {
        int previous_direction = direction;

        if (goal.x < transform.position.x) direction = -1;        

        else direction = 1;
        

        if (direction != previous_direction) flip();
    }

    private void move_to()
    {
        if ( is_in_box(goal, 1f, 1f) )
        {
            change_action_when_rest = true; change_action_when_follow = true; change_action_when_attack = true;
        }

        else move();
    }

    private void start_choosing_new_action(ref bool can_change_action)
    {
        current_state = states.do_nothing;

        can_change_action = true;
    }

    private void set_can_change_action(ref bool can_change_action)
    {
        if (current_state == states.is_standing && Time.time - start_time_of_standing > end_time_of_standing) start_choosing_new_action(ref can_change_action);
    }

    private void start_damaging()
    {
        if ( is_in_box(player.transform.position, attack_box_width, attack_box_height) )
        {
            damage = 20;

            player.get_damage();
        }
    }

    private void end_damaging()
    {
        damage = 0;

        start_choosing_new_action(ref change_action_when_attack);
    }

    protected override void stand()
    {
        start_time_of_standing = Time.time;
        
        standing_probility = current_state == states.is_standing ? 0 : 30;
            
        base.stand();

        standing_probility = 0;
    }

    private void set_pos_as_goal(float x, float y)
    {
        goal = new Vector2(x, y);

        start_time_of_standing = -1f;

        current_state = states.is_running;

        change_action_when_attack = true;
    }

    private void attack()
    {
        current_state = states.is_attacking; change_action_when_rest = true; change_action_when_follow = true;

        standing_probility += 30;

        animator.SetTrigger("is_attacking");
    }

    private void generate_action(ref bool can_change_action, int stand_probability, int run_probaility, float end_time_of_standing_, float goal_x)
    {
        set_can_change_action(ref can_change_action);

        if (can_change_action)
        {
            int probability = new System.Random().Next(0, 101);

            if (probability >= 0 && probability <= stand_probability)
            {
                end_time_of_standing = end_time_of_standing_;
                stand();
            }

            else if (probability > stand_probability && probability <= stand_probability + run_probaility) set_pos_as_goal(goal_x, transform.position.y);

            else attack();
            
            can_change_action = false;
        }
    }

    private void do_at_resting_state()
    {
        generate_action( ref change_action_when_rest, 40, 60, (float) (new System.Random().NextDouble() * (2.5f - 0.5f) + 0.5f), 
            (float) (new System.Random().NextDouble() * (right_position_border - left_position_border) + left_position_border) );

        change_action_when_follow = true; change_action_when_attack = true;

        standing_probility = 30;
    }

    private void do_at_follow_mode()
    {
        generate_action(ref change_action_when_follow, 10, 90, (float) (new System.Random().NextDouble() * (2.5f - 0.5f) + 0.5f), player.transform.position.x);

        change_action_when_attack = true;

        standing_probility = 30;
    }

    private void do_at_attack_mode()
    {
        generate_action(ref change_action_when_attack, standing_probility, 0, (float) (new System.Random().NextDouble() * (1f - 0.5f) + 0.5f), player.transform.position.x);
        
        change_action_when_follow = true;
    }

    private void do_if_player_is_spotted()
    {
        if ( should_attack_player() ) do_at_attack_mode();
        
        else do_at_follow_mode();
        
        change_action_when_rest = true;

        goal = player.transform.position;
    }

    protected void Update()
    {
        if ( has_player_been_spotted() ) do_if_player_is_spotted();

        else do_at_resting_state();

        should_flip();
        if (current_state == states.is_running) move_to();

        set_animation();
    }
}