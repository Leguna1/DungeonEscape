using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 2.0f;
    //Player smoothly rotates and faces camera's Y axiss.
    //Player moves forward to whatever direction his facing and play moveForwardAnimation.
    //Player can move backward with S key and play moveBackward animation.
    //Player can move to left with A input and play leftStrafe animation.
    //Player can move to right with D input and
    
    void Update()
    {
        
    }
    
}
