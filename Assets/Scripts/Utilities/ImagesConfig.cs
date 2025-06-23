using UnityEngine;

namespace Utilities
{
	[CreateAssetMenu(fileName = "ImagesConfig", menuName = "Configs/ImagesConfig", order = 1)]
	public class ImagesConfig : ScriptableObject
	{
		[SerializeField]
		private ImageIdPair[] imagesPairs;
		public ImageIdPair[] ImagesPairs => imagesPairs;

		[SerializeField]
		private bool willUpdateIds = false;

		private void OnValidate()
		{
			if (willUpdateIds)
			{
				willUpdateIds = false;

				if (imagesPairs != null && imagesPairs.Length > 0)
				{
					for (var index = 0; index < imagesPairs.Length; index++)
					{
						imagesPairs[index].SetId(index);
					}
				}
			}
		}
	}
}