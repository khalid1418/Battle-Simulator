using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class TargetFinderSystem : SystemBase
{
    public event System.EventHandler TeamBlueWin;
    public event System.EventHandler TeamRedWin;
    private List<Entity> teamAUnits;
    private List<Entity> teamBUnits;
    private GameManagerComponentData gameManager;

    protected override void OnStartRunning()
    {
        teamAUnits = new List<Entity>();
        teamBUnits = new List<Entity>();
        Entities.ForEach((ref GameManagerComponentData gameManagerComponentData) =>
        {
            gameManager = gameManagerComponentData;
            gameManager.battleStart = false;
            Debug.Log(gameManager.battleStart);
        }).WithoutBurst().Run();
    }

    protected override void OnUpdate()
    {
        var allTeamADies = true;
        var allTeamBDies = true;



        Entities.ForEach((in Entity unit, in UnitComponentData unitComponent) =>
        {

            if (unitComponent.isTeamBlue == true)
            {
                teamAUnits.Add(unit);
            }
            else
            {
                teamBUnits.Add(unit);
            }

        }).WithoutBurst().Run();


        Entities.ForEach((Entity unit, ref FindTargetData unitFinder) =>
        {
            var target = unitFinder.targetGameObject;
            if (target != Entity.Null)
            {
                var targetData = SystemAPI.GetComponent<UnitComponentData>(target);
                if (targetData.hp <= 0)
                {
                    unitFinder.targetGameObject = Entity.Null;
                }
            }

            var unitComponent = SystemAPI.GetComponent<UnitComponentData>(unit);

            // Check winners:
            if (unitComponent.hp > 0 && unitComponent.isTeamBlue)
            {
                allTeamADies = false;
            }
            else if (unitComponent.hp > 0 && unitComponent.isTeamBlue == false)
            {
                allTeamBDies = false;
            }
            // Find a random target from the opposite team:
            if (unitFinder.targetGameObject == Entity.Null)
            {
                if (unitComponent.isTeamBlue)
                {
                    var newTarget = teamBUnits[Random.Range(0, teamBUnits.Count)];
                    var newTargettData = SystemAPI.GetComponent<UnitComponentData>(newTarget);
                    if (newTargettData.hp > 0)
                    {
                        unitFinder.targetGameObject = newTarget;
                    }
                }
                else if (unitComponent.isTeamBlue == false)
                {
                    var newTarget = teamAUnits[Random.Range(0, teamAUnits.Count)];
                    var newTargettData = SystemAPI.GetComponent<UnitComponentData>(newTarget);
                    if (newTargettData.hp > 0)
                    {
                        unitFinder.targetGameObject = newTarget;
                    }
                }
            }

        }).WithoutBurst().Run();
        if(allTeamADies && allTeamBDies)
        {
            return;
        }
        else { 
        if (allTeamADies)
        {
                TeamRedWin.Invoke(this, System.EventArgs.Empty);
            }

            if (allTeamBDies)
        {
                TeamBlueWin.Invoke(this, System.EventArgs.Empty);

            }
        }
        }
}

