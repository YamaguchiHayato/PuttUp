using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private GameObject spawnedObject; // 現在表示中のオブジェクト
    public GameObject spawnPrefab;        // 生成するPrefab
    public Transform sphereTransform;     // スフィアのTransformをアサイン
    public Vector3 offset = new Vector3(1, 0, 0); // 生成位置オフセット
    public Vector3 spawnScale = Vector3.one;
    public Vector3 spawnRotationEuler = Vector3.zero;


    void Update()
    {
      

        // マウス左ボタンを押している間だけ処理
        if (Input.GetMouseButton(0)) // 左クリック押下中
        {
            // スフィアをマウスで移動させる処理（例）
            MoveSphereWithMouse();

            // まだ生成されていないなら生成する
            if (spawnedObject == null)
            {
                Quaternion rot = Quaternion.Euler(spawnRotationEuler);
                Vector3 pos = sphereTransform.position + offset;
                spawnedObject = Instantiate(spawnPrefab, pos, rot);
                spawnedObject.transform.localScale = spawnScale;
            }
            else
            {
                // すでに生成済みなら位置を追従させる
                spawnedObject.transform.position = sphereTransform.position + offset;
            }
        }
        else
        {
            // ボタンを離したら消す
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
                spawnedObject = null;
            }
        }


        void MoveSphereWithMouse()
        {
            // ここにスフィアをマウスで動かす処理を書く
            // 例：マウス位置をRayで取得してスフィアを移動させる（仮）
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                sphereTransform.position = hit.point;
            }
        }

    }

   
}
