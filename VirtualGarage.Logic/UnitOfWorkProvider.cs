using KK.Data;

namespace BarterMarket.Logic
{
    /// <summary>
    /// give interfaced unit of work for your frontend app
    /// </summary>
    public static class UnitOfWorkProvider
    {
        /// <summary>
        /// Create new instance of unit of work. dont forget to dispose
        /// </summary>
        /// <returns></returns>
        public static IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}
