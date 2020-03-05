using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitCampaign3 : BattleState
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
    "Black Mage",
    "Bomb",
    "Bomb",
    "Bomb"
         };
    List<Tile> locations = new List<Tile>(board.tiles.Values);
    Tile TileBlödgarm = locations[49];
    Tile TileKnight = locations[25];
    Tile TileCavalier = locations[37];
    Tile TileBlackMage = locations[36];
    Tile TileBomb1 = locations[69];
    Tile TileBomb2 = locations[93];
    Tile TileBomb3 = locations[103];

    int level = 3;
    GameObject instance = UnitFactory.Create(recipes[0], level);
    Unit unit = instance.GetComponent<Unit>();

    unit.Place(TileBlödgarm);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);

    instance = UnitFactory.Create(recipes[1], level);
    unit = instance.GetComponent<Unit>();
        unit.Place(TileKnight);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);
    
    instance = UnitFactory.Create(recipes[2], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileCavalier);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);
    
    instance = UnitFactory.Create(recipes[3], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileBlackMage);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);
    
    instance = UnitFactory.Create(recipes[4], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileBomb1);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    
    units.Add(unit);
    instance = UnitFactory.Create(recipes[5], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileBomb2);
    unit.dir = (Directions) UnityEngine.Random.Range(0, 4);
    unit.Match();
    units.Add(unit);

    instance = UnitFactory.Create(recipes[6], level);
    unit = instance.GetComponent<Unit>();
    unit.Place(TileBomb3);
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