using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadInput : MonoBehaviour
{
    // 1.NewInputAction = name of New Input Action "NewInputAction"
    // 2.BasicActionMap = name of Action Map "BasicActionMap"
    // 3.LeftAnalog = name of Action "LeftAnalog"
    // 4.GamepadInput = name of this C# script

    NewInputAction NewInputActionScript;

    public Vector2 LeftAnalogVector2;
    public Dictionary<string, bool> onButtonHold = new Dictionary<string, bool>();
    public Dictionary<string, bool> onButtonDown = new Dictionary<string, bool>();
    public Dictionary<string, bool> onButtonUp = new Dictionary<string, bool>();

    public static GamepadInput Singleton { get; private set; } //Singleton , remove duplicates
    private void Awake()
    {
        NewInputActionScript = new NewInputAction();

        if (Singleton != null && Singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Singleton = this;
        }
    }


    private void OnEnable()
    {
        NewInputActionScript.Enable();
    }

    private void OnDisable()
    {
        NewInputActionScript.Disable();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); //keep running on all scenes

        foreach (var item in NewInputActionScript.BasicActionMap.Get().actions) //on each Buttons
        {
            onButtonHold.Add(item.name, false);
            onButtonDown.Add(item.name, false);
            onButtonUp.Add(item.name, false);

            //delegate : Listerner , triggered when Button status is changed
            item.performed += delegate { onButtonHold[item.name] = true; };
            item.canceled += delegate { onButtonHold[item.name] = false; };

            item.canceled += delegate { onButtonUp[item.name] = true; };
        }
    }


    void Update() //begin of frame
    {
        LeftAnalogVector2 = NewInputActionScript.BasicActionMap.LeftAnalog.ReadValue<Vector2>(); //update LeftAnalog

        foreach (var item in NewInputActionScript.BasicActionMap.Get().actions) //each Button
        {
            onButtonDown[item.name] = item.triggered; //true if Button is pressed Down
        }
    }

    private void LateUpdate() //end of frame
    {
        foreach (var item in NewInputActionScript.BasicActionMap.Get().actions) //each Button
        {
            onButtonUp[item.name] = false; //false if Button is pressed Up
        }
    }
}
