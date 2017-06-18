using UnityEngine;

namespace LuftSchloss.Util {
	public static class TextureUtil {
        public static Color[] GetColorBlock(Texture2D baseTexture, Rect rect) {
            return baseTexture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        }

        public static Color[] GetColorBlock(Texture2D baseTexture, IntRect rect) {
            return baseTexture.GetPixels(rect.x, rect.y, rect.width, rect.height);
        }
	}
}
