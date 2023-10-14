using UnityEngine;
using System.Xml.Linq;

public class LoadFieldMap : MonoBehaviour
{
    public string mapName;
    public Field field;

    // Start is called before the first frame update
    void Start()
    {
        field.Reset();
        Array2D mapdata = readMapFile(mapName);
        if (mapdata != null)
        {
            field.Create(mapdata);
        }
    }

    /**
    * TMXファイルからマップデータを取得する
    */
    private Array2D readMapFile(string path)
    {
        try
        {
            XDocument xml = XDocument.Load(path);
            XElement map = xml.Element("map");
            Array2D data = null;
            int w = 0, h = 0;
            foreach (var layer in map.Elements("layer"))
            {
                switch (layer.Attribute("id").Value)
                {
                    case "1":
                        string[] sdata = (layer.Element("data").Value).Split(',');
                        w = int.Parse(layer.Attribute("width").Value);
                        h = int.Parse(layer.Attribute("height").Value);
                        data = new Array2D(w, h);
                        for (int z = 0; z < h; z++)
                        {
                            for (int x = 0; x < w; x++)
                            {
                                data.Set(x, z, int.Parse(sdata[ToMirrorX(x, w) + z * w]) - 1);
                            }
                        }
                        break;
                }
            }
            return data;
        }
        catch (System.Exception i_exception)
        {
            Debug.LogErrorFormat("{0}", i_exception);
        }
        return null;
    }
    private int ToMirrorX(int xgrid, int mapWidth)
    {
        return mapWidth - xgrid - 1;
    }
}
