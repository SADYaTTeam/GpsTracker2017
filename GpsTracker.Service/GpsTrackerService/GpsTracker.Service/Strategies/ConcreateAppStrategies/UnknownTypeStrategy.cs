using GpsTracker.Service.Strategies.Base;
using GpsTracker.Service.Controllers;
using GpsTracker.Service.Controllers.App;

namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    public class UnknownTypeStrategy : Strategy
    {
        #region Constructors

        public UnknownTypeStrategy()
        { }

        public UnknownTypeStrategy(AppController controller) : base(controller) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}