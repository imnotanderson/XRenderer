namespace XRenderer
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    internal abstract class DataBase<T> :IData where T : MonoBehaviour 
    {
        protected T mono;
        private Queue<Action> quque = new Queue<Action>();
        object ququeLock = new object();

        protected DataBase()
        {
            Renderer.instance.RegData(this);
            lock (ququeLock)
            {
                quque.Enqueue(() => mono = initMono());
            }
        }

        protected abstract T initMono();

        protected void DoAction(Action f)
        {
            lock (ququeLock)
            {
                quque.Enqueue(f);
            }
        }

        public Action[] GetActions()
        {
            Action[] actions = null;
            lock (ququeLock)
            {
                actions = new Action[quque.Count];
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i] = quque.Dequeue();
                }
            }
            return actions;
        }
    }


}