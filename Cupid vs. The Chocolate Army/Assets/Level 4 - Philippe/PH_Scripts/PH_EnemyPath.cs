using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The scripts that tells enemies to move from one point to the other on the Waypoint Map.
/// </summary>


public class PH_EnemyPath : MonoBehaviour
{
    PH_WaveConfig EnemySettings;//Wave configuration file

    List<Transform> WayPointMap;//List of waypoints from the enemy path object
    List<Transform> DeadlyWayPointMap;//List of waypoints from the second enemy path object

    //related scripts
    PH_Enemy EnemyData;
    PH_GameSession session;

    //variables
    bool SuicidePathSet = false;
    int WaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        WayPointMap = EnemySettings.GetWayPoints();
        DeadlyWayPointMap = EnemySettings.GetSuicideWayPoints();
        transform.position = WayPointMap[WaypointIndex].transform.position;//the initail position is the first index point
        EnemyData = gameObject.GetComponent<PH_Enemy>();//script from the same object
        session = FindObjectOfType<PH_GameSession>();//game session for this scene
    }

    // Update is called once per frame
    void Update()
    {
        if (!session.GetGoalProgressionStatus())//while goal is not met
        {
        if (EnemySettings.GetSuicidal() && (EnemyData.GetHealth() <= EnemyData.GetHealthFormChange())) { KamiKaze(); }
        //if the enemy is set as suicidal and it's health is below the changing form point, use the suicide path
        else { March(); }//use the normal path
        }
    }

    public void SetWaveConfig(PH_WaveConfig waveConfig)//method to set the config file from another script
    {
        EnemySettings = waveConfig;
    }


    void March()
    {
        if (WaypointIndex < WayPointMap.Count)//while the end of the map hasn't been reached 
        {
            var targetPosition = WayPointMap[WaypointIndex].transform.position;
            var MovementThisFrame = EnemySettings.GetMoveSpeed() * EnemyData.GetSpeedMultiplier() * Time.deltaTime;//really just speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MovementThisFrame); //movetowards(startingpoint,destination,speed)   //when reached, use next Waypoint
            if (transform.position == targetPosition) { WaypointIndex++; }
        }

        else if (WaypointIndex == WayPointMap.Count && EnemySettings.GetStartingPosition() != 0)
        {// if a loop was set up, go back to where it starts
            WaypointIndex = EnemySettings.GetStartingPosition();//the first point of the loop
        }

        else { Destroy(gameObject); }//destroy the object when it reaches the last waypoint and a loop is not setup
    }

    void KamiKaze()
    {
        if (!SuicidePathSet) //start to use the suicide path if it hasn't been set yet
        {
            WaypointIndex = 0;
            foreach(Transform wayPoint in DeadlyWayPointMap)
            {   var PointPosition = wayPoint.position;
                float SuicideLateralOffset = EnemySettings.GetSuicideLateralOffset();
                Vector3 LateralOffset = new Vector3(Random.Range(-SuicideLateralOffset, SuicideLateralOffset), PointPosition.y, PointPosition.z);
                wayPoint.position = LateralOffset; }//randomize the waypoint positions for the kamikaze route
            SuicidePathSet = true;//path has ben set
        }
        
        if (WaypointIndex < DeadlyWayPointMap.Count)//current i < # of items in list
        {
            var targetPosition = DeadlyWayPointMap[WaypointIndex].transform.position;
            var MovementThisFrame = EnemySettings.GetMoveSpeed() * EnemySettings.GetSuicideSpeedMultiplier() * Time.deltaTime;//really just speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MovementThisFrame); //movetowards(startingpoint,destination,speed)   //when reached, use next Waypoint
            if (transform.position == targetPosition) { WaypointIndex++; }
        }

        else 
        {
            FindObjectOfType<PH_GameSession>().AddToScore(-EnemyData.GetPoints());
            EnemyData.Die();
        }//destroy the object when it reaches the last waypoint an a loop is not setup
    }//substract points so that the score doesn't go up from enemy selfdestruct
}
