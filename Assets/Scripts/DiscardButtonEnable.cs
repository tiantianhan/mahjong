using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardButtonEnable : MonoBehaviour
{
    
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void EnableOnTileSelected(bool selected, Tile tile)
    {
        // TODO: Needs debugging - this is not being called
        // even though it is registered with a UnityEvent on button selected
        Debug.Log("Button Interactable " + selected);
        button.interactable = selected;
    }
}
