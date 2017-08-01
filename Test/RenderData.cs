using UnityEngine;

namespace XRenderer.Test
{
	class TestDataBase : DataBase<RenderData>
	{
		public Vector2 v;

		public TestDataBase(Vector2 v) : base()
		{
			this.v = v;
		}

		protected override RenderData initMono()
		{
			return GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<RenderData>();
		}

		public void Upt(KeyCode keyCode)
		{
			var t = 0.02f;
			if (keyCode == KeyCode.A)
			{
				this.v -= Vector2.right * t;
			}
			else if (keyCode == KeyCode.D)
			{
				this.v += Vector2.right * t;
			}
			DoAction(() => mono.transform.position = v);
		}
	}

	public class RenderData : MonoBehaviour
	{

	}
}