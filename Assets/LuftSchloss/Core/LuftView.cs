using LuftSchloss.Core;

namespace LuftSchloss.MVC.Views {
    public class LuftView : LuftMonobehaviour {
        public override int Priority {
            get {
                return (int) LuftPriority.Low;
            }
        }
    }
}
