﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Turn
{

    public Unit actor;
    public bool hasUnitMoved;
    public bool hasUnitActed;
    public bool lockMove;
    public Ability ability;
    public List<Tile> targets;
    Tile startTile;
    Directions startDir;
    public PlanOfAttack plan;
    public void Change(Unit current)
    {
        actor = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;
        startTile = actor.tile;
        startDir = actor.dir;
        plan = null;
    }
    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(startTile);
        actor.dir = startDir;
        actor.Match();
    }
}