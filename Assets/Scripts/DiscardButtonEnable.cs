using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardButtonEnable : MonoBehaviour
{
    [SerializeField]
    Button button;

    public void EnableOnTileSelected(bool selected, Tile tile)
    {
        Debug.Log("Button Interactable " + selected);
        button.interactable = selected;
    }
}
