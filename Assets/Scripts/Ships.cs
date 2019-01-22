using UnityEngine;
using System;
using System.Collections.Generic;

public class Ships : MonoBehaviour
{

    //This is our custom class with our variables
    [System.Serializable]
    public class Ship
    {
        public String name;
        public int price;
        public Sprite sprite;
        public bool unlocked;
        public List<Ship> colors = new List<Ship>(1);
    }

    //This is our list we want to use to represent our class as an array.
    public List<Ship> list = new List<Ship>(1);


    void AddNew()
    {
        //Add a new index position to the end of our list
        list.Add(new Ship());
    }

    void Remove(int index)
    {
        //Remove an index position from our list at a point in our list array
        list.RemoveAt(index);
    }
}