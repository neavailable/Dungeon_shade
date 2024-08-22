using UnityEngine;

public abstract class Moving_item : MonoBehaviour
{
    private void Start() {}

    protected virtual void move() {}

    protected virtual void set_basic_animation() {}

    private void Update() {}
}


//    _________
//   | _ _ _ _ |
//   |  +   +  |
//   |    o    |
//    =========
//       -|-
//       / \
//      
//    це мішаня