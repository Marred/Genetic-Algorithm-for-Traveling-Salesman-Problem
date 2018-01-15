using WdTIGS.Models;

namespace WdTIGS.Services
{
    interface ISelectionService
    {
        Subject[] Select(in Subject[] baseInstances, int groupSize = 4);
    }
}
