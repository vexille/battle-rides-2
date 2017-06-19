using LuftSchloss.Core;

namespace LuftSchloss.Core {
    public class LuftView : LuftMonobehaviour {
        public override int Priority {
            get {
                return (int) LuftPriority.Low;
            }
        }
    }
}
