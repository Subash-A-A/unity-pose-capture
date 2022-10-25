using UnityEngine;
using System.Collections.Generic;

public class LineManager : MonoBehaviour
{   
    [System.Serializable]
    public class LineFromTo
    {
        public int fromIndex;
        public int toIndex;
    }

    public List<LineFromTo> lineFromTo;
    [SerializeField] GameObject LinePrefeb;
    [SerializeField] Transform lineParent;
    [SerializeField] Transform points;

    private void Start()
    {
        CreateLines();
    }
    private void CreateLines()
    {
        foreach(LineFromTo fromTo in lineFromTo)
        {
            GameObject line = Instantiate(LinePrefeb, lineParent);
            line.name = "Line " + fromTo.fromIndex + "-" + fromTo.toIndex;
            
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, points.GetChild(fromTo.fromIndex).position);
            lineRenderer.SetPosition(1, points.GetChild(fromTo.toIndex).position);
        }
    }

    public void DrawLines()
    {
        int index = 0;
        foreach(Transform line in lineParent)
        {
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();

            LineFromTo pointFromTo = lineFromTo[index];

            Vector3 start = points.GetChild(pointFromTo.fromIndex).position;
            Vector3 end = points.GetChild(pointFromTo.toIndex).position;

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            index++;
        }
    }
}
