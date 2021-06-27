using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle,  //showing no spheres
    UserInteraction,  //showing mixing spheres
    Mixing, //mixing animation
    ShowingResultSphere,  //showing result sphere
}
