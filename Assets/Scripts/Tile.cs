using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum DisplayState
    {
        Hidden,
        Displayed,
        Revealed,
    }

    private DisplayState displayState;


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

    public static Tile SpawnWithAttributes(Tile prefab, Attributes attributes, Transform container)
    {
        GameObject tileObject = Instantiate(prefab.gameObject);
        tileObject.transform.parent = container;

        Tile tile = tileObject.GetComponent<Tile>();
        tile.SetAttributes(attributes);
        tile.LoadSprite();

        tileObject.name = "Tile " + tile.GetNotation();

        return tile;
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

    void LoadSprite()
    {
        if (attributes.asset != null)
        {
            tileRenderer.sprite = Resources.Load<Sprite>("pixel_art_tile_sprites/" + attributes.asset);
        }
    }
}
