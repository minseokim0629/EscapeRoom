using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject[] cubes;
    public int amount;
    public float minRange;
    public float range;
    public float minSize;
    public float maxSize;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amount; ++i)
        {
            var obj = Instantiate(GenerateCube());
            obj.transform.parent = transform;
        }
    }

    public GameObject GenerateCube()
    {
        int index = Random.Range(0, cubes.Length);
        GameObject cube = cubes[index];

        Vector3 position;

        do
        {
            position = new Vector3(
                Random.Range(-range, range),
                Random.Range(-range, range),
                Random.Range(-range, range));
        } while (range < Vector3.Magnitude(position) || Vector3.Magnitude(position) < minRange || (Mathf.Abs(position.x) < 2 && Mathf.Abs(position.y) < 2 && position.z < 0));

        cube.transform.position = position;
        cube.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

        return cube;

    }

}
