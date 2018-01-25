using WdTIGS.Services;

namespace WdTIGS.Models
{
    struct Subject
    {
        public int[] Cities;
        public int Distance
        {
            get
            {
                return PathService.GetSumDistance(this.Cities);
            }
        }
    }
}