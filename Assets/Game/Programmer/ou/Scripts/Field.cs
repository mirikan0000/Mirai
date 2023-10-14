
using UnityEngine;

public class Field : MonoBehaviour
{
    // 次のメソッドを「public static」にして移して下さい
    /**
    * グリッド座標(X)をワールド座標(X)に変換
    */
    public GameObject floor;
    public GameObject wall;

    private Array2D map;
    private const float oneTile = 2.0f;
    private const float floorSize = 10.0f / oneTile;

    void Start()
    {
        Array2D mapdata = new Array2D(10, 10);
        mapdata.Set(1, 1, 1);
        Create(mapdata);
    }
    public void Create(Array2D mapdata)
    {
        map = mapdata;
        float floorw = map.width / floorSize;
        float floorh = map.height / floorSize;
        floor.transform.localScale = new Vector3(floorw, 1, floorh);


        float floorx = (map.width - 1) / 2.0f * oneTile;
        float floorz = (map.height - 1) / 2.0f * oneTile;
        floor.transform.position = new Vector3(floorx, 0, floorz);
        for (int z = 0; z < map.height; z++)
        {
            for (int x = 0; x < map.width; x++)
            {
                if (map.Get(x, z) > 0)
                {
                    GameObject block = Instantiate(wall);
                    float xblock = ToWorldX(x);
                    float zblock = ToWorldZ(z);
                    block.transform.localScale = new Vector3(oneTile, 2, oneTile);
                    block.transform.position = new Vector3(xblock, 1, zblock);
                    block.transform.SetParent(floor.transform.GetChild(0));
                }
            }
        }
    }

    /**
    * 生成したマップのリセット
*/
    public void Reset()
    {
        Transform walls = floor.transform.GetChild(0);
        for (int i = 0; i < walls.childCount; i++)
        {
            Destroy(walls.GetChild(i).gameObject);
        }
    }

    /**
    * 指定の座標が壁かどうかをチェック
*/
    public bool IsCollide(int xgrid, int zgrid)
    {
        return map.Get(xgrid, zgrid) != 0;
    }
    public static float ToWorldX(int xgrid) { return xgrid; }

    /**
    * グリッド座標(Z)をワールド座標(Z)に変換
    */
    public static float ToWorldZ(int zgrid) { return zgrid; }

    /**
    * ワールド座標(X)をグリッド座標(X)に変換
    */
    public static int ToGridX(float xworld) { return (int)xworld; }

    /**
    * ワールド座標(Z)をグリッド座標(Z)に変換
    */
    public static int ToGridZ(float zworld) { return (int)zworld; }
}
