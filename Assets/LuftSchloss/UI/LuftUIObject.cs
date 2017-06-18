using LuftSchloss.Core;

namespace LuftSchloss.UI {
    public class LuftUIObject : LuftMonobehaviour {
        public override int Priority {
            get {
                return (int) LuftPriority.Medium;
            }
        }

        public UIRoot Root { get; private set; }

        public void InitializeUI(UIRoot root) {
            Root = root;
        }
    }
}
