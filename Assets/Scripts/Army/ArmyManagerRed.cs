using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.AI;
//using static System.Console;

public class ArmyManagerRed : ArmyManager
{

	public override void ArmyElementHasBeenKilled(GameObject go)
	{
		base.ArmyElementHasBeenKilled(go);
		if (m_ArmyElements.Count == 0)
		{
			GUIUtility.systemCopyBuffer = "0\t" +((int)Timer.Value).ToString()+"\t0\t0\t0";
		}
	}
	public void GreenArmyIsDead(string deadArmyTag)
    {
        int nDrones = 0, nTurrets = 0, health = 0;
        ComputeStatistics(ref nDrones, ref nTurrets, ref health);
		GUIUtility.systemCopyBuffer = "1\t" + ((int)Timer.Value).ToString() + "\t"+nDrones.ToString()+"\t"+nTurrets.ToString()+"\t"+health.ToString();
		
		RefreshHudDisplay(); //pour une dernière mise à jour en cas de victoire
	}

    public override GameObject getWeakDroneByDistanceByType<T>(Vector3 centerPos, float minRadius, float maxRadius)
    {
        var weakEnemies = GetAllEnemiesOfTypeByDistance<Drone>(true, centerPos, minRadius, maxRadius)
        .Where(element => element is T && element.GetComponentInChildren<Health>().Value < 40)
        .ToList();

        if (weakEnemies.Count > 0)
        {
            int randomIndex = Random.Range(0, weakEnemies.Count);
            return weakEnemies[randomIndex].gameObject;
        }
        else
        {
            return null; // Retourne null s'il n'y a pas d'ennemis faibles.
        }
        //return GetRandomWeakEnemies<Drone>(GetAllEnemiesOfTypeByDistance<Drone>(true, centerPos, minRadius, maxRadius));
        
    }

    public override GameObject GetRandomWeakEnemies<T>() 
    {
        var weakEnemies = GetAllEnemiesOfType<Turret>(true)
        .Where(element => element is T && element.GetComponentInChildren<Health>().Value < 80)
        .ToList();
        //Console.WriteLine("Hello, World!");

        if (weakEnemies.Count > 0)
        {
            int randomIndex = Random.Range(0, weakEnemies.Count);
            return weakEnemies[randomIndex].gameObject;
        }
        else
        {
            return null; // Retourne null s'il n'y a pas d'ennemis faibles.
        }
    }

}