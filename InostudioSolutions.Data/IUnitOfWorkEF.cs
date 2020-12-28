using System.Data.Entity;

namespace KK.Data
{
    public interface IUnitOfWorkEF : IUnitOfWork
    {
        /// <summary>
        /// context object
        /// </summary>
        DbContext Context { get; }
    }
}
