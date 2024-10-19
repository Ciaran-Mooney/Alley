using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    /// <summary>
    ///     What day is it.
    /// </summary>
    public static int day = 0;

    /// <summary>
    ///     Track the current mission. None is set when the mission has not started.
    /// </summary>
    public static Mission mission = Mission.Freebie;

    /// <summary>
    ///     To indicate what section of the mission the player is on.
    /// </summary>
    public static MissionState missionState = MissionState.Pre;

    /// <summary>
    ///     Object currently held in the players hands
    /// </summary>
    public static FPSInteractable heldObject = null;

    /// <summary>
    ///     The current state of the player.
    /// </summary>
    public static PlayerState playerState = PlayerState.WakingUp;

    /// <summary>
    ///     Has the player talked to the entity.
    /// </summary>
    public static bool hasTalked = false;
}

public enum HeldObject
{
    None,
    Drugs,
    Cat
}

public enum Mission
{
    Freebie,
    Piss,
    Cat,
    None
}

public enum MissionState
{
    Pre,
    Active,
    Completed
}


public enum PlayerState
{
    Moving,
    Talking,
    UsingDrugs,
    WakingUp
}
