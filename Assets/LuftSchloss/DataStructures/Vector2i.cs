namespace LuftSchloss {
    [System.Serializable]
	public struct Vector2i {
        public int x;
        public int y;

        public Vector2i(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Vector2i(Vector2f vec) {
            this.x = (int) vec.x;
            this.y = (int) vec.y;
        }

        public override string ToString() {
            return string.Format("({0}, {1})", this.x, this.y);
        }

        // TODO: Convert this into a factory method in a separate class
        public Vector2i(UnityEngine.Vector2 vec) {
            this.x = (int) vec.x;
            this.y = (int) vec.y;
        }
	}
}
