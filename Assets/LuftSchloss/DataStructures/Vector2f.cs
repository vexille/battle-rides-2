using UnityEngine;

namespace LuftSchloss {
    [System.Serializable]
    public struct Vector2f {
        public float x;
        public float y;

        public Vector2f(float both) {
            this.x = both;
            this.y = both;
        }

        public Vector2f(float x, float y) {
            this.x = x;
            this.y = y;
        }

        // TODO: Convert this into a factory method in a separate class
        public Vector2f(UnityEngine.Vector2 vec) {
            this.x = (float) vec.x;
            this.y = (float) vec.y;
        }

        public float MagnitudeSquared() {
            return Mathf.Pow(x, 2) + Mathf.Pow(y, 2);
        }

        public float Magnitude() {
            return Mathf.Sqrt(MagnitudeSquared());
        }

        public static float DistanceSquared(Vector2f v1, Vector2f v2) {
            return (v2 - v1).MagnitudeSquared();
        }

        public static float Distance(Vector2f v1, Vector2f v2) {
            return (v2 - v1).Magnitude();
        }

        public static Vector2f operator +(Vector2f v1, Vector2f v2) {
            return new Vector2f(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2f operator -(Vector2f v1, Vector2f v2) {
            return new Vector2f(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2f operator *(Vector2f v, float d) {
            return new Vector2f(v.x * d, v.y * d);
        }

        public static Vector2f operator /(Vector2f v, float d) {
            return new Vector2f(v.x / d, v.y / d);
        }
    }
}
