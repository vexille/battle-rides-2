
namespace LuftSchloss.Util {
    public static class GridUtil {
        public static int GetTop<T>(this Grid<T> grid, int index) {
            var y = grid.GetY(index);
            if (y == 0) {
                return -1;
            }

            var x = grid.GetX(index);
            return grid.GetIndex(x, y - 1);
        }

        public static int GetBottom<T>(this Grid<T> grid, int index) {
            var y = grid.GetY(index);
            if (y == grid.Height - 1) {
                return -1;
            }

            var x = grid.GetX(index);
            return grid.GetIndex(x, y + 1);
        }

        public static int GetLeft<T>(this Grid<T> grid, int index) {
            var x = grid.GetX(index);
            if (x == 0) {
                return - 1;
            }

            var y = grid.GetY(index);
            return grid.GetIndex(x - 1, y);
        }

        public static int GetRight<T>(this Grid<T> grid, int index) {
            var x = grid.GetX(index);
            if (x == grid.Width - 1) {
                return -1;
            }

            var y = grid.GetY(index);
            return grid.GetIndex(x + 1, y);
        }
    }
}
