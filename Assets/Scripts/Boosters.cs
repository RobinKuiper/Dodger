using UnityEngine;
using System;
using System.Collections.Generic;

public class Boosters : MonoBehaviour
{

    //This is our custom class with our variables
    [System.Serializable]
    public class Booster
    {
        public String name;
        public Sprite sprite;
        public GameObject prefab;
    }

    //This is our list we want to use to represent our class as an array.
    public List<Booster> list = new List<Booster>(1);


    void AddNew()
    {
        //Add a new index position to the end of our list
        list.Add(new Booster());
    }

    void Remove(int index)
    {
        //Remove an index position from our list at a point in our list array
        list.RemoveAt(index);
    }
}