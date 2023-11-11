using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectType
{
    Desk,
    Wall,
    Floor,
    Window

}
public class MyObjectClassifier : MonoBehaviour
{
   public ObjectType objectType;
}
