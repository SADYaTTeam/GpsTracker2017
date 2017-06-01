// <copyright file="IMainDbContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.DataContext.Interfaces
{
    /// <summary>
    /// Represents interface for main db context of GpsTrackerDB
    /// </summary>
    public interface IMainDbContext
    {
        /// <summary>
        /// Gets or sets <see cref="Contexts.EventContext"/> 
        /// </summary>
        IDbContext<Models.Event, Event> Event { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.LogContext"/> 
        /// </summary>
        IDbContext<Models.Log, DataContext.Log> Log { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.PersonContext"/> 
        /// </summary>
        IDbContext<Models.Person, DataContext.Person> Person { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.UserContext"/> 
        /// </summary>
        IDbContext<Models.User, DataContext.User> User { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.MarkerContext"/> 
        /// </summary>
        IDbContext<Models.Marker, DataContext.Marker> Marker { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.TrackContext"/> 
        /// </summary>
        IDbContext<Models.Track, DataContext.Track> Track { get; set; }

        /// <summary>
        /// Begin transaction in EF db context
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit started transaction in EF db context
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollback started transaction in EF db context
        /// </summary>
        void RollbackTransaction();
    }
}
