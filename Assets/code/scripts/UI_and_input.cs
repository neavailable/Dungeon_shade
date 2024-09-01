using UnityEngine;
using UnityEngine.InputSystem;


public class UI_and_input : MonoBehaviour
{
    [SerializeField] private Player player_class;


    private void player_input()
    {
        if (Keyboard.current.aKey.isPressed) player_class.move_to_side(-1);
        
        else if (Keyboard.current.dKey.isPressed) player_class.move_to_side(1);
        
        else if (Keyboard.current.fKey.isPressed) player_class.start_rolling();
        
        else ( (Character) player_class ).stand();
    }

    private void Update()
    {
        if ( player_class.is_rolling() ) player_class.roll();

        else player_input();
    }
}
