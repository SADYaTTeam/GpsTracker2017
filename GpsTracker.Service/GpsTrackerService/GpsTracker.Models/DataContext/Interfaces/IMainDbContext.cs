using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.DataContext.Interfaces
{
    public interface IMainDbContext
    {
        IDbContext<Models.Event, DataContext.Event> Event { get; set; }
        IDbContext<Models.Log, DataContext.Log> Log { get; set; }
        IDbContext<Models.Person, DataContext.Person> Person { get; set; }
        IDbContext<Models.User, DataContext.User> User { get; set; }
        IDbContext<Models.Marker, DataContext.Marker> Marker { get; set; }
        IDbContext<Models.Track, DataContext.Track> Track { get; set; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
