using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSpheres : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject sphereFrame;
    public List<GameObject> sphereList;
    public int maxSpheres;
    public Vector3[] points;
    public AudioAnalysis audioAnalysis;
    //public int band;
    public GameObject spherePrefab;

    public Material mat1;

    // Start is called before the first frame update
    void Start()
    {
        points = GetPointsOnSphere(maxSpheres);
        int countOfBand = 0;

        foreach (Vector3 point in points)
        {
            GameObject sphere = (GameObject)Instantiate(spherePrefab);
            sphere.transform.position = point * 5;
            sphere.transform.parent = sphereFrame.transform;
            //sphere.GetComponent<Renderer>().material = mat1;

            int band = countOfBand % 7;
            sphere.GetComponent<SphereParticle>().audioBand = band;
            countOfBand++;

            sphereList.Add(sphere);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            for (int i = 0; i < sphereList.Count; i++)
            {
                int band = sphereList[i].GetComponent<SphereParticle>().audioBand;
                sphereList[i].transform.position = Vector3.Lerp(points[i] * 5, sphereList[i].transform.position * 1.3f, audioAnalysis.audioBandBuffer[band]);
            }
        }
    }

    Vector3[] GetPointsOnSphere(int numOfPoints)
    {
        /*
        http://web.archive.org/web/20120421191837/http://cgafaq.info/wiki/Evenly_distributed_points_on_sphere

        dlong := pi*(3-sqrt(5))   //~2.39996323
        dz    := 2.0/N
        long := 0
        z    := 1 - dz/2
        for k := 0 .. N-1
            r    := sqrt(1-z*z)
            node[k] := (cos(long)*r, sin(long)*r, z)
            z    := z - dz
            long := long + dlong
        */
        
        Vector3[] points = new Vector3[numOfPoints];

        float dlong = Mathf.PI * (3 - Mathf.Sqrt(5));
        float dz = 2.0f / (float)numOfPoints;
        float longitude = 0;
        float z = 1 - dz / 2;

        for (int i = 0; i < numOfPoints; i++)
        {
            float r = Mathf.Sqrt(1 - (z * z));
            
            points[i] = new Vector3(Mathf.Cos(longitude) * r, Mathf.Sin(longitude) * r, z);
            z = z - dz;
            longitude = longitude + dlong;
        }

        return points; 
    }
}
