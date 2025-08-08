using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float offset = 5f;

    public Material material1;
    public Material material2;

    void GenerateTile(int x, int z)
    {

        Color pixelColor = map.GetPixel(x, z);
        Debug.Log(pixelColor);
        if (pixelColor.a == 0)
        {
            return;
        }
        foreach (ColorToPrefab colorMapping in colorMappings)
        {

            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, 0, z) * offset;
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }

    public void GenerareLabirynth()
    {
        for (int x=0; x<map.width; x++)
        {
            for (int z=0; z<map.height; z++)
            {
                GenerateTile(x, z);
            }
        }
        ColorChildren();
    }

    public void ColorChildren()
    {
        foreach(Transform child in transform)
        {
            if(child.tag=="WALL")
            {
                if(Random.Range(0, 5)==0)
                {
                    child.gameObject.GetComponent<Renderer>().material 
                        = material2;
                }
                else
                {
                    child.gameObject.GetComponent<Renderer>().material
                        = material1;
                }
            }
        }
    }
}
