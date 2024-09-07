using UnityEngine;


public class Background_mover : MonoBehaviour
{
    private Transform player_transform;
    [SerializeField] private GameObject background_left, background_right;

    private float bottom_left_x, bottom_right_x, size;
    private float left_border, right_border;


    // in Start we set value of specailized (unity) objects
    private void Start()
    {
        left_border = 2.5f; right_border = 5f;

        player_transform = GameObject.Find("player").transform;

        size = background_right.GetComponent<SpriteRenderer>().bounds.size.x;
    
        bottom_right_x = background_right.transform.position.x + size;
        bottom_left_x = background_right.transform.position.x - size;
    }

    private void change_backgorud_position()
    {
        background_left.transform.position = new Vector2(left_border, background_right.transform.position.y);
        background_right.transform.position = new Vector2(right_border, background_right.transform.position.y);
    }

    // that is move background script. how it works?
    // left_border and right_border are like box where player are locating
    // when player across left_border or right_border two images relocate by their size
    // borders relocate by size also
    private void move()
    {
        if (player_transform.position.x > right_border)
        {
            right_border += size;
            left_border = right_border - size;

            change_backgorud_position();
        }

        else if (player_transform.position.x <= left_border)
        {
            left_border -= size;
            right_border = left_border + size;

            change_backgorud_position();
        }
    }

    //there we will call methods which are updating every frame
    private void Update()
    {
        move();
    }
}