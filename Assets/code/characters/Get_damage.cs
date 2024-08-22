using UnityEngine;


public class Get_damage : Moving_item
{
    private bool is_pinned;

    private Transform current_transform;
    private Character pinned_character;

    private void Start()
    {
        current_transform = GetComponent<Transform>();

        is_pinned = false;
    }

    protected override void move()
    {
        transform.position = new Vector2(current_transform.position.x - 0.1f, current_transform.position.y + 0.8f);
    }

    protected override void set_basic_animation()
    {
        GetComponent<Animator>().SetBool("is_pinned", is_pinned);
    }

    private void move_and_set_animation()
    {
        move();

        set_basic_animation();
    }

    public void set_pinned_object(Character character)
    {
        current_transform.position = new Vector2(character.transform.position.x, character.transform.position.y);

        pinned_character = character;

        is_pinned = true;

        move_and_set_animation();
    }

    private void set_default_position()
    {
        current_transform.position = new Vector2(0f, 100f);

        pinned_character.end_getting_damage();

        is_pinned = false;

        move_and_set_animation();
    }

    private void Update() { }
}