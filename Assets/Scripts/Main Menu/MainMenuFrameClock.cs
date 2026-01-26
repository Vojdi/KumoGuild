using UnityEngine;
using System.Collections.Generic;

public class MainMenuFrameClock : MonoBehaviour
{
    [SerializeField] List<GameObject> clouds;
    List<Vector3> startPositions;
    int endPosX = 74;

    private void Start() 
    {
        startPositions = new List<Vector3>();
        foreach (GameObject cloud in clouds) { 
            startPositions.Add(cloud.transform.position);
        }
        
    }
    void Tick()
    {
        foreach (var cloud in clouds)
        {
            if(cloud.transform.position.x >= startPositions[clouds.IndexOf(cloud)].x + endPosX)
            {
                cloud.transform.position = startPositions[clouds.IndexOf(cloud)];
            }
            cloud.transform.Translate(Vector2.right * 6 * Time.deltaTime);
        }
    }
}
