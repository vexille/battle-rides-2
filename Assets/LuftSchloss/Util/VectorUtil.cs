namespace LuftSchloss.Util {
	public static class VectorUtil {
        public static Vector2i Add(Vector2i lhs, Vector2i rhs) {
            return new Vector2i(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2i Add(Vector2i lhs, int x, int y) {
            return new Vector2i(lhs.x + x, lhs.y + y);
        }

        public static Vector2i AddX(Vector2i lhs, int x) {
            return new Vector2i(lhs.x + x, lhs.y);
        }

        public static Vector2i AddY(Vector2i lhs, int y) {
            return new Vector2i(lhs.x, lhs.y + y);
        }
	}
}
