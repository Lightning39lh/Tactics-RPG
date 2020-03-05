using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitCampaign1 : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        SelectTile(p);
        SpawnTestUnits();
        AddVictoryCondition();
        owner.round = owner.gameObject.AddComponent<TurnOrderController>().Round();
        yield return null;
        owner.ChangeState<CutSceneState>();
    }

    void SpawnTestUnits()
    {
        string[] recipes = new string[]
        {
    "Blödgarm",
    "Enemy Knight"
        };
        List<Tile> locations = new List<Tile>(board.tiles.Values);
        Tile TileBlödgarm = locations[29];
        Tile TileCavalier = locations[33];
        int level = 2;
        GameObject instance = UnitFactory.Create(recipes[0], level);
        Unit unit = instance.GetComponent<Unit>();
        
        unit.Place(TileBlödgarm);
        unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
        unit.Match();
        units.Add(unit);

        
        instance = UnitFactory.Create(recipes[1], level);
        unit = instance.GetComponent<Unit>();
        unit.Place(TileCavalier);
        unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
        unit.Match();
        units.Add(unit);

        SelectTile(units[0].tile.pos);
    }
    void AddVictoryCondition()
    {
        DefeatTargetVictoryCondition vc = owner.gameObject.AddComponent<DefeatTargetVictoryCondition>();
        Unit enemy = units[units.Count - 1];
        vc.target = enemy;
        Health health = enemy.GetComponent<Health>();
        health.MinHP = 35;
    }
}