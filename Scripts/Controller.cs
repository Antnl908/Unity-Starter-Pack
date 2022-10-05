using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    IBridge bridge;
    AiCameraHolder holder;
    PlayerInput input;
    InputAction click;
    InputAction move;
    InputAction look;
    InputAction jumping;
    public Transform target;
    //Vector3 offset = new Vector3(0f, 20f, -25f);
    Vector3 offset = new Vector3(0f, 3f, 0f);
    public Transform pivot;
    public Transform camera;
    bool jump;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        click = input.actions["Click"];
        move = input.actions["Walk"];
        look = input.actions["Look"];
        jumping = input.actions["Jump"];
        bridge = target.GetComponent<IBridge>();
        //holder = GetComponent<AiCameraHolder>();
        //holder = new AiCameraHolder();
        //holder = transform.gameObject.AddComponent<AiCameraHolder>();
        //holder.SetController(pivot);
        //holder.SetTarget(bridge.GetViewPosition());
        
    }
    // Start is called before the first frame update
    void Start()
    {
        click.performed += Click;
        move.performed += Move;
        move.canceled += Move;
        look.performed += Look;
        look.canceled += Look;
        jumping.performed += Jump;
        jumping.canceled += Jump;

        pivot.position = Vector3.zero;
        camera.localPosition = Vector3.zero;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*if(bridge != null)
        {
            transform.position = bridge.GetPosition() + offset;
        }*/
        
    }

    void Click(InputAction.CallbackContext ctx)
    {
        if(bridge != null)
        {
            bridge.Click();
        }
    }
    
    void Move(InputAction.CallbackContext ctx)
    {
        if(bridge != null)
        {
            bridge.Move(ctx.ReadValue<Vector2>(), pivot/*, jump*/);
        }
    }
    
    void Look(InputAction.CallbackContext ctx)
    {
        if(bridge != null)
        {
            bridge.Look(ctx.ReadValue<Vector2>(), pivot, camera);
        }
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValue<float>());
        //Debug.Log(ctx.ReadValue<bool>());
        if (ctx.ReadValue<float>() > 0)
        {
            this.jump = true;
        }   
        else
        {
            this.jump = false;
        }
            
    }

    void OnEnable()
    {

    }

    /*void OnDisable()
    {
        click.performed -= Click;
        move.performed -= Move;
        move.canceled -= Move;
        look.performed -= Look;
        look.canceled -= Look;
    }*/
}
