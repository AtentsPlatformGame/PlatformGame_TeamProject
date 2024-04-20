using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeteor : MonoBehaviour
{
    public Transform meteorPointVFX;
    public Transform meteorFireVFX;

    [SerializeField] int meteorCount;
    [SerializeField]
    Vector3[] meteorPoints;
    [SerializeField] Vector2 xClamp = new Vector2(-7,8);
    [SerializeField] Vector2 zClamp = new Vector2(-7, 8);
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        meteorPoints = new Vector3[meteorCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            ActiveMeteor();
        }
    }

    public void ActiveMeteor()
    {
        target = FindObjectOfType<PlayerController>().transform;
        if(target != null)
        {
            StartCoroutine(SpawningMeteorPoint(target));
        }

    }

    IEnumerator SpawningMeteorPoint(Transform _target)
    {
        int prevX = 0, prevZ = 0;
        for (int i = 0; i < meteorCount;)
        {
            int rndX = Random.Range((int)xClamp.x, (int)xClamp.y);
            int rndZ = Random.Range((int)zClamp.x, (int)zClamp.y);
            if (i == 0)
            {
                Instantiate(meteorPointVFX, new Vector3(target.position.x, 0.0f, target.position.z), Quaternion.identity, null);
                prevX = rndX;
                prevZ = rndZ;
            }
            else
            {
                int maxX = Mathf.Max(prevX, rndX);
                int minX = Mathf.Min(prevX, rndX);
                int maxZ = Mathf.Max(prevZ, rndZ);
                int minZ = Mathf.Min(prevZ, rndZ);

                if(maxX - minX <= 5 || maxZ - minZ <= 5)
                //if (Mathf.Abs(prevX - rndX) >= 5 || Mathf.Abs(prevZ - rndZ) >= 5)
                {
                    Instantiate(meteorPointVFX, new Vector3(target.position.x + rndX, 18.0f, target.position.z + rndZ), Quaternion.identity, null);
                    prevX = rndX;
                    prevZ = rndZ;
                    
                }
                else
                {
                    continue;
                }
            }

            meteorPoints[i] = new Vector3(target.position.x + prevX, 40.0f, target.position.z + prevZ);
            i++;
            yield return new WaitForSeconds(0.2f);
        }

        yield return StartCoroutine(SpawningMeteorFire());
    }

    IEnumerator SpawningMeteorFire()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < meteorCount; i++)
        {
            Instantiate(meteorFireVFX, meteorPoints[i], Quaternion.identity, null);
            new WaitForSeconds(0.5f);
        }
    }
}
