using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class PointAnimation : MonoBehaviour
{
    public bool useCam = false;

    [SerializeField] string animationFileName;
    [SerializeField] GameObject point;
    [SerializeField] Transform body;
    
    private LineManager lineManager;
    private List<string> lines;
    private int counter = 0;

    private void Start()
    {
        lines = new List<string>();

        if (!useCam)
        {
            lines = System.IO.File.ReadLines("Assets/"+ animationFileName + ".txt").ToList();
        }

        lineManager = GetComponent<LineManager>();

        SetupPoints();
    }

    private void Update()
    {
        AnimatePoints();
    }

    private void SetupPoints()
    {
        for(int i = 0; i <= 32; i++)
        {
            Instantiate(point, body);
        }
    }

    private void AnimatePoints()
    {
        string[] points = (!useCam) ? lines[counter].Split(",") : UDPReceive.data.Split(",");
        
        for(int i = 0; i <= 32; i++)
        {   
            float x = float.Parse(points[0 + i * 3]) / 100;
            float y = float.Parse(points[1 + i * 3]) / 100;
            float z = float.Parse(points[2 + i * 3]) / 100;

            body.GetChild(i).localPosition = new Vector3(x, y, z);
        }

        counter += 1;

        if(counter == lines.Count)
        {
            counter = 0;
        }

        lineManager.DrawLines();

        Thread.Sleep(30);
    }
}
