using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    //GameObject info in ObjectPool
    public class ObjectPoolInfo
    {
        public GameObject prefab;
        public GameObject parent;
        public Stack<GameObject> poolStack;
    }

    //ObjectPool instantiate parent object info
    [System.Serializable]
    public class PoolInfo
    {
        public GameObject poolingObj;
        public int initSize;
        //public bool isDontDestroy;
    }

    //Single ObjectPool
    public class ObjectPool
    {
        Stack<GameObject> pool;
        PoolInfo poolInfo;
        Transform parents;

        public ObjectPool(PoolInfo poolInfo, Transform parents)
        {
            pool = new Stack<GameObject>();
            this.poolInfo = poolInfo;
            this.parents = parents;
            Init();
        }
        public void Init()
        {
            AddPool(poolInfo.initSize);
        }
        public void AddPool(int size = 1)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject addObj = GameObject.Instantiate(poolInfo.poolingObj, parents);
                addObj.SetActive(false);
                pool.Push(addObj);
            }
        }

        public GameObject PopObj()
        {
            if (pool.Count <= 0)
                AddPool((int)(poolInfo.initSize / 3));

            GameObject popObj = pool.Pop();
            popObj.SetActive(true);

            return popObj;
        }

        public void ReturnPool(GameObject returnObj)
        {
            returnObj.SetActive(false);
            returnObj.transform.SetParent(parents);
            pool.Push(returnObj);
        }
    }

    //Multi ObjectPool
    public class ObjectPoolManager : SingleTon<ObjectPoolManager>
    {
        //public static ObjectPoolManager instance = null;

        [SerializeField] List<PoolInfo> poolInfoList;
        Dictionary<string, ObjectPool> poolDic;
        
        void Start()
        {
            poolDic = new Dictionary<string, ObjectPool>();
            Init();
        }

        void Init()                            //등록된 Prefab들에 대한 부모와 Dic생성
        {
            foreach (var info in poolInfoList)
            {
                string objName = info.poolingObj.name;
                GameObject objParent = new GameObject(objName + "-Pool");

                ObjectPool objectPool = new ObjectPool(info, objParent.transform);
                poolDic.Add(objName, objectPool);
                poolDic.Add(objName + "(Clone)", objectPool);

            }
        }

        public GameObject PopObj(GameObject obj, Vector3 pos, Quaternion rot, Transform transform = null)     //Dictionary에서 원하는 GameObject 호출
        {
            string objName = obj.name;
            if (!poolDic.ContainsKey(objName))
                return null;
            GameObject popObj = poolDic[objName].PopObj();
            popObj.transform.position = pos;
            popObj.transform.rotation = rot;
            popObj.transform.parent = transform;
            return popObj;
        }

        public void ReturnPool(GameObject returnObj)    //Dictionary에 다시 등록
        {
            if (!poolDic.ContainsKey(returnObj.name))
                return;
            poolDic[returnObj.name].ReturnPool(returnObj);
        }
    }
}
