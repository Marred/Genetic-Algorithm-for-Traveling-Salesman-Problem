using WdTIGS.Models;

namespace WdTIGS.Services
{
    interface ISelectionService
    {
        Subject[] Select(Subject[] baseInstances, int groupSize = 4);
    }
}
