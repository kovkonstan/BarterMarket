using KK.Data;
using BarterMarket.Logic.DataModel;
using BarterMarket.Logic.Repository;

namespace BarterMarket.Logic
{
    internal class UnitOfWork : UnitOfWorkEF<BarterMarketEntities>
    {
        public override TRepoInterface CreateInterfacedRepo<TRepoInterface>()
        {           
            if (typeof(TRepoInterface) == typeof(IUserRepo))
                return (new UserRepo(this)) as TRepoInterface;
            
            return base.CreateInterfacedRepo<TRepoInterface>();
        }

        public override IRepository<TEntity> CreateRepo<TEntity>()
        {          
            return base.CreateRepo<TEntity>();
        }
    }
}