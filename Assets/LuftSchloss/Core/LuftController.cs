
namespace LuftSchloss.Core {
    public class LuftController : LuftObject {
        public override int Priority {
            get {
                return (int) LuftPriority.Medium;
            }
        }
    }
}
