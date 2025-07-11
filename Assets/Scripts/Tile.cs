using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [Serializable]
    public class AttributesFile
    {
        public Attributes[] attributes;
    }

    [Serializable]
    public class Attributes
    {
        public string type;
        public int number;
        public int count;

        public string asset;

        public int order;

        public Attributes()
        {
        }

        public Attributes(Attributes attributes)
        {
            this.type = attributes.type;
            this.number = attributes.number;
            this.count = attributes.count;
            this.asset = attributes.asset;
            this.order = attributes.order;
        }

        public Attributes Clone()
        {
            return new Attributes(this);
        }
    }

    [SerializeField]
    private Attributes attributes;


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

    public static Tile SpawnWithAttributes(Tile prefab, Attributes attributes, Transform container)
    {
        GameObject tileObject = Instantiate(prefab.gameObject);

        Tile tile = tileObject.GetComponent<Tile>();
        tile.SetAttributes(attributes);
        tile.LoadSprite();

        tileObject.name = "Tile " + tile.GetNotation();

        tile.MoveToContainer(container);

        return tile;
    }

    public void MoveToContainer(Transform container)
    {
        gameObject.transform.parent = container;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
    }

    public void SetAttributes(Attributes attributes)
    {
        this.attributes = attributes.Clone();
    }

    public string GetNotation()
    {
        return attributes.number <= 0 ? attributes.type : attributes.type + attributes.number;
    }

    public bool Compare(Tile other)
    {
        return other.GetNotation() == GetNotation();
    }

    public int GetOrder()
    {
        return attributes.order;
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
        if (attributes.asset != null)
        {
            tileRenderer.sprite = Resources.Load<Sprite>("pixel_art_tile_sprites/" + attributes.asset);
        }
    }
}
