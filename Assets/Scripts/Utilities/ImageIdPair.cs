using System;
using UnityEngine;

namespace Utilities
{
	[Serializable]
	public class ImageIdPair
	{
		[SerializeField]
		private int id;
		public int ID => id;
		[SerializeField]
		private Sprite sprite;
		public Sprite Sprite => sprite;

		public void SetId(int id) => this.id = id;
	}
}