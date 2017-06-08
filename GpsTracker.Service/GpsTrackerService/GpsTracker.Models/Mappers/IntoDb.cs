// <copyright file="IntoDb.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Mappers
{
    /// <summary>
    /// Class represents opportunity to map Models and DataContext
    /// </summary>
    public static class IntoDb
    {
        /// <summary>
        /// Map <see cref="Models.Event"/> to <see cref="DataContext.Event"/>
        /// </summary>
        public static DataContext.Event Convert(this Models.Event evnt)
        {
            return new DataContext.Event()
            {
                EventId = evnt.EventId,
                Name = evnt.Name
            };
        }

        /// <summary>
        /// Map <see cref="Models.Log"/> to <see cref="DataContext.Log"/>
        /// </summary>
        public static DataContext.Log Convert(this Models.Log log)
        {
            return new DataContext.Log()
            {
                LogId = log.LogId,
                EventId = log.EventId,
                EventDate = log.EventDate,
                Message = log.Message,
                DeviceId = log.DeviceId
            };
        }

        /// <summary>
        /// Map <see cref="Models.Person"/> to <see cref="DataContext.Person"/>
        /// </summary>
        public static DataContext.Person Convert(this Models.Person person)
        {
            return new DataContext.Person()
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                MiddleName = person.MiddleName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                Phone = person.Phone,
                Email = person.Email,
                Photo = person.Photo,
                UserId = person.UserId
            };
        }

        /// <summary>
        /// Map <see cref="Models.Person"/> to <see cref="DataContext.Person"/>
        /// </summary>
        public static DataContext.User Convert(this Models.User user)
        {
            return new DataContext.User()
            {
                UserId = user.UserId,
                DeviceId = user.DeviceId,
                Login = user.Login,
                Password = user.Password,
                DateCreatedAt = user.DateCreatedAt,
                IsAdmin = user.IsAdmin,
            };
        }

        /// <summary>
        /// Map <see cref="Models.Marker"/> to <see cref="DataContext.Marker"/>
        /// </summary>
        public static DataContext.Marker Convert(this Models.Marker marker)
        {
            return new DataContext.Marker()
            {
                MarkerId = marker.MarkerId,
                Longitude = marker.Longitude,
                Latitude = marker.Latitude,
                UserId = marker.UserId,
                Timestamp = marker.Timestamp
            };
        }
    }
}
