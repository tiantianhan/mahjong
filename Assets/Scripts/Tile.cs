using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private string type;

    [SerializeField]
    private int number;

    public enum DisplayState
    {
        Hidden,
        Displayed,
        Revealed,
    }

    private DisplayState displayState;

    void Awake()
    {
        displayState = DisplayState.Hidden;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetNotation()
    {
        return number <= 0 ? type : type + number; 
    }

    public bool Compare(Tile other)
    {
        return other.GetNotation() == GetNotation();
    }

    public DisplayState GetDisplayState()
    {
        return displayState;
    }

    public void SetDisplayState(DisplayState state)
    {
        displayState = state;
    }

    public static bool IsAllIdentical(Tile[] tiles)
    {
        throw new NotImplementedException();
    }

    public static bool IsRun(Tile[] tiles)
    {
        throw new NotImplementedException();
    }
}
