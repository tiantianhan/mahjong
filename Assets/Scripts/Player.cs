using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Hand hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw(Deck deck)
    {
        hand.Add(deck.DrawFromTop());
        hand.Order();
        hand.Layout();
    }
}
