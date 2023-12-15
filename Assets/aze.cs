using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class aze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<string> Deserialize(string data)
    {
        return data.Split('c').ToList();
    }
}
