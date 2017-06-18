using LuftSchloss.Core;
using LuftSchloss.Events;

namespace Luderia.Game.MVC.Controllers {
    public class LuftController : LuftObject {
        public override int Priority {
            get {
                return (int) LuftPriority.Medium;
            }
        }
    }
}
