using System;
using System.Collections.Generic;
using UnityEngine;

namespace XRenderer
{
    public interface IData
    {
        Action[] GetActions();
    }
    
    public class Renderer:MonoBehaviour
    {
        private static Renderer m;

        public static Renderer instance
        {
            get
            {
                if (!object.ReferenceEquals(m, null)) return m;
                var NO_INIT_EXCEPTION = new Exception("renderer no init,run Renderer.Init() from the main thread first.");
                Debug.LogError(NO_INIT_EXCEPTION.ToString());
                throw NO_INIT_EXCEPTION;
            }
        }
        List<IData> dataList = new List<IData>();
        List<Action> actions = new List<Action>();
        public static void Init()
        {
            if (object.ReferenceEquals(m, null))
                new GameObject("[XRenderer]").AddComponent<Renderer>();
        }

        void Awake()
        {
            m = this;
        }

        public void RegData(IData data)
        {
            lock (dataList)
            {
                dataList.Add(data);
            }
        }

        void Update()
        {
            lock (dataList)
            {
                for (int i = 0; i < dataList.Count; i++)
                {
                    var a = dataList[i].GetActions();
                    if (a != null)
                    {
                        actions.AddRange(a);
                    }
                }
            }
            while (actions.Count>0)
            {
                actions[0]();
                actions.RemoveAt(0);
            }
        }
    }
}