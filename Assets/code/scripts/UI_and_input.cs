using UnityEngine;


public class UI_and_input : MonoBehaviour
{
    [SerializeField] private Player player_class;


    private void player_moving()
    {
        if (Input.GetKey(KeyCode.A)) player_class.move_x(-1);
        
        else if (Input.GetKey(KeyCode.D)) player_class.move_x(1);

        else if (Input.GetKeyDown(KeyCode.Space)) player_class.start_rolling();
                
        else if (Input.GetKeyDown(KeyCode.E)) player_class.start_climbing();

        else ((Character)player_class).stand(); 
    }

    private void Update()
    {
        if (player_class.is_rolling()) player_class.roll();

        else if (player_class.is_climbing()) player_class.climb();

        else player_moving();
    }
}
