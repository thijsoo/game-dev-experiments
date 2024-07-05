
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public GameObject pipePrefab;
    public float horizontalSpeed = 0.1f;
    private List<GameObject> pipes = new List<GameObject>();
    
    void Start()
    {
        InvokeRepeating("SpawnPipe", 0f, Random.Range(0.8f, 1f));
    }
   
    void FixedUpdate()
    {
        if (pipes.Count > 0)
        {
            foreach (GameObject pipe in pipes.ToList())
            {
                pipe.transform.position = new Vector2(pipe.transform.position.x - horizontalSpeed, pipe.transform.position.y);

                if (pipe.transform.position.x < -11)
                {
                    pipes.Remove(pipe);
                    Destroy(pipe);
                }
            }
        }
    }


    private void SpawnPipe()
    {
        GameObject pipe = Instantiate(pipePrefab);
        pipe.transform.position = new Vector2(11,Random.Range(-1.4f, 3.3f));
        pipes.Add(pipe);
    }
}
