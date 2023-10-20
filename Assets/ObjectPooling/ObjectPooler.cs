using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleObjectPooling
{
    public class ObjectPooler : Singleton<ObjectPooler>
    {
        [System.Serializable]
        public class Pool
        {
            public PoolMember Name;
            public GameObject Prefabs;
            public int InitialSize;
        }

        public List<Pool> Pools;
        Dictionary<PoolMember, Queue<GameObject>> poolDictionary;

        protected override void Awake()
        {
            MakeSingleton(true);
            poolDictionary = new Dictionary<PoolMember, Queue<GameObject>>();

            foreach (Pool pool in Pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int index = 0; index < pool.InitialSize; index++)
                {
                    GameObject obj = Instantiate(pool.Prefabs);
                    obj.SetActive(false);
                    obj.transform.parent = transform;
                    objectPool.Enqueue(obj);
                }
                poolDictionary.Add(pool.Name, objectPool);
                
            }
        }

        #region Pull GameObject method
        public GameObject Pull(PoolMember memberName)
        {
            if (!poolDictionary.ContainsKey(memberName))
                throw new MissingMemberException($"Pool with tag {tag} doesn't exit!");

            GameObject objectToGet; 
            if (poolDictionary[memberName].Count > 0)
                objectToGet = poolDictionary[memberName].Dequeue();
            else
                objectToGet = Instantiate(GetPrefabsWithTag(memberName));

            objectToGet.SetActive(true);
            return objectToGet;
        }

        public GameObject Pull(PoolMember memberName,Vector2 position,Transform parent = null)
        {
            GameObject obj = Pull(memberName);
            obj.transform.position = position;
            obj.transform.parent = parent;
            return obj;
        }
        public GameObject Pull(PoolMember memberName, Vector2 position,Quaternion rotation,Transform parent = null)
        {
            GameObject obj = Pull(memberName,position,parent);
            obj.transform.rotation = rotation;
            return obj;
        }
        public GameObject Pull(PoolMember memberName, Vector3 position, Transform parent = null)
        {
            GameObject obj = Pull(memberName);
            obj.transform.position = position;
            obj.transform.parent = parent;
            return obj;
        }
        public GameObject Pull(PoolMember memberName, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject obj = Pull(memberName, position, parent);
            obj.transform.rotation = rotation;
            return obj;
        }
        #endregion

        #region Pull object method
        public T Pull<T>(PoolMember memberName) where T : MonoBehaviour, IPoolable
        {
            GameObject obj = Pull(memberName);
            T t = obj.GetComponent<T>();
            return t;
        }
        public T Pull<T>(PoolMember memberName, Vector2 position, Transform parent = null) where T : MonoBehaviour, IPoolable
        {
            T t = Pull<T>(memberName);
            t.transform.position = position;
            t.transform.parent = parent;
            return t;
        }
        public T Pull<T>(PoolMember memberName, Vector2 position,Quaternion rotation, Transform parent = null) where T : MonoBehaviour, IPoolable
        {
            T t = Pull<T>(memberName,position,parent);
            t.transform.rotation = rotation;
            return t;
        }
        public T Pull<T>(PoolMember memberName, Vector3 position, Transform parent = null) where T : MonoBehaviour, IPoolable
        {
            T t = Pull<T>(memberName);
            t.transform.position = position;
            t.transform.parent = parent;
            return t;
        }
        public T Pull<T>(PoolMember memberName, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour, IPoolable
        {
            T t = Pull<T>(memberName, position, parent);
            t.transform.rotation = rotation;
            return t;
        }
        #endregion

        public void Push(PoolMember memberName, GameObject obj)
        {
            if (!poolDictionary.ContainsKey(memberName))
            {
                Debug.LogWarning($"Member name {memberName.ToString()} có thể chưa có pool");
                return;
            }
            obj.transform.parent = this.transform;
            obj.SetActive(false);

            poolDictionary[memberName].Enqueue(obj);
        }

        GameObject GetPrefabsWithTag(PoolMember memberName)
        {
            foreach (Pool pool in Pools)
            {
                if (pool.Name == memberName)
                    return pool.Prefabs;
            }
            return null;
        }
    }
}


