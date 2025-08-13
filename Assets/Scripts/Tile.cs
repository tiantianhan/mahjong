using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using TileModel = Model.Tile;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    public enum DisplayState
    {
        Hidden,
        Displayed,
        Revealed,
    }

    private DisplayState displayState;

    private bool selected;

    [SerializeField]
    private TileModel model;

    [SerializeField]
    private SpriteRenderer tileRenderer;

    public interface ITileClickedHandler
    {
        public void OnTileClicked(Tile tile);
    }

    public ITileClickedHandler TileSelectedHandler { get; set; }

    void Awake()
    {
        displayState = DisplayState.Hidden;
    }

    public static Tile SpawnWithModel(Tile prefab, TileModel tileModel, Transform container)
    {
        GameObject tileObject = Instantiate(prefab.gameObject);

        Tile tile = tileObject.GetComponent<Tile>();
        tile.SetModel(tileModel);
        tile.LoadSprite();
        tileObject.name = "Tile " + tile.GetUniqueNotation();
        tile.MoveToContainer(container);

        return tile;
    }

    public void MoveToContainer(Transform container)
    {
        gameObject.transform.parent = container;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
    }

    public void SetModel(TileModel model)
    {
        this.model = model;
    }

    public string GetNotation()
    {
        return model.GetNotation();
    }

    public string GetUniqueNotation()
    {
        return model.GetUniqueNotation();
    }

    public bool Compare(Tile other)
    {
        return other.GetNotation() == GetNotation();
    }

    public int GetOrder()
    {
        return model.order;
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

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Pointer Down");
        TileSelectedHandler.OnTileClicked(this);
    }

    public void ToggleSelected()
    {
        SetSelected(!selected);
    }

    public void SetSelected(bool selected)
    {
        this.selected = selected;
        float offset = 0.1f;
        if (selected)
            transform.localPosition += new Vector3(0, offset, 0);
        else
            transform.localPosition += new Vector3(0, -offset, 0);
    }

    public bool IsSelected()
    {
        return selected;
    }

    void LoadSprite()
    {
        tileRenderer.sprite = TileSetLoader.GetTileAssetForModel(model);
    }
}
