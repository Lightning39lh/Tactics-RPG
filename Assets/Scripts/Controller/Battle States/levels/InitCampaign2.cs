using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitCampaign2 : BattleState
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
    "Knight",
    "Cavalier",
    "Enemy Knight2",
    "Enemy White Mage",
    "Enemy Archer"
        };
    List<Tile> locations = new List<Tile>(board.tiles.Values);
    Tile TileBlödgarm = locations[80];
    Tile TileKnight = locations[94]; 
    Tile TileCavalier = locations[64];
    Tile TileEnemyKnight = locations[72];
    Tile TileEnemyWhiteMage = locations[86];
    Tile TileEnemyArcher = locations[102];


    int level = 2;
    GameObject instance = UnitFactory.Create(recipes[0], level);
    Unit unit = instance.GetComponent<Unit>();

    unit.Place(TileBlödgarm);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);

    instance = UnitFactory.Create(recipes[1], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileCavalier);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);
    
    instance = UnitFactory.Create(recipes[2], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileKnight);
    unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);
    level = 1;
    instance = UnitFactory.Create(recipes[3], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileEnemyWhiteMage);
    unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);
    
    instance = UnitFactory.Create(recipes[4], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileEnemyKnight);
    unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
    unit.Match();
    
    units.Add(unit);
    instance = UnitFactory.Create(recipes[5], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileEnemyArcher);
    unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);

        SelectTile(units[0].tile.pos);
}
void AddVictoryCondition()
    {
        DefeatAllEnemiesVictoryCondition vc = owner.gameObject.AddComponent<DefeatAllEnemiesVictoryCondition>();
       
    }
}