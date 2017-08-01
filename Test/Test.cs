using System.Threading;
using UnityEngine;

namespace XRenderer.Test
{
    public class Test:MonoBehaviour
    {
        private Thread th = null;
        private KeyCode _keyCode = KeyCode.None;
        
        void Start()
        {
            Renderer.Init();
            th = new Thread(() =>
            {
                var test = new TestDataBase(Vector2.zero);
                while (true)
                {
                    test.Upt(_keyCode);
                    Thread.Sleep(33);
                }
            });
            th.Start();
            Debug.Log("start");
        }
        
        void Update()
        {
            _keyCode = KeyCode.None;
            if (Input.GetKey(KeyCode.A))
            {
                _keyCode = KeyCode.A;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                _keyCode = KeyCode.D;
            }
        }

        void OnDestroy()
        {
            th.Abort();
            Debug.Log("end");
        }
    }
}