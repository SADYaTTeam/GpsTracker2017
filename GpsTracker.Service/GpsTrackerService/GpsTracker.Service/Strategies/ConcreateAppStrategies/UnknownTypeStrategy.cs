// <copyright file="UnjnownTypeStrategy.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    #region using...
    using Base;
    using Controllers.App;
    #endregion

    /// <summary>
    /// Represents strategy to treate unknown-type messages
    /// </summary>
    public class UnknownTypeStrategy : Strategy
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownTypeStrategy"/> class
        /// </summary>
        public UnknownTypeStrategy()
        {
            
        }

        /// <summary>
        /// Initialized a new instance of the <see cref="UnknownTypeStrategy"/> class
        /// </summary>
        /// <param name="controller">App contoller instance</param>
        public UnknownTypeStrategy(AppController controller) : base(controller)
        {
            
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}