using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTarget : MonoBehaviour
{
    [SerializeField]
    MovementTarget nextTarget;
    public MovementTarget NextTarget=>nextTarget;
}
