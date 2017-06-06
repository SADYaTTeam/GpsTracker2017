// <copyright file="FromDb.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Mappers
{
    /// <summary>
    /// Class represents opportunity to map DataContext and Models
    /// </summary>
    public static class FromDb
    {
        /// <summary>
        /// Map <see cref="DataContext.Event"/> to <see cref="Models.Event"/>
        /// </summary>
        public static Models.Event Convert(this DataContext.Event evnt)
        {
            return new Models.Event()
            {
                EventId = evnt.EventId,
                Name = evnt.Name
            };
        }

        /// <summary>
        /// Map <see cref="DataContext.Log"/> to <see cref="Models.Log"/>
        /// </summary>
        public static Models.Log Convert(this DataContext.Log log)
        {
            return new Models.Log()
            {
                LogId = log.LogId,
                EventId = log.EventId,
                EventDate = log.EventDate,
                Message = log.Message,
                DeviceId = log.DeviceId
            };
        }

        /// <summary>
        /// Map <see cref="DataContext.Person"/> to <see cref="Models.Person"/>
        /// </summary>
        public static Models.Person Convert(this DataContext.Person person)
        {
            return new Models.Person()
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
        /// Map <see cref="DataContext.User"/> to <see cref="Models.User"/>
        /// </summary>
        public static Models.User Convert(this DataContext.User user)
        {
            return new Models.User()
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
        /// Map <see cref="DataContext.Marker"/> to <see cref="Models.Marker"/>
        /// </summary>
        public static Models.Marker Convert(this DataContext.Marker marker)
        {
            return new Models.Marker()
            {
                MarkerId = marker.MarkerId,
                Longtitude = marker.Longtitude,
                Latitude = marker.Latitude,
                UserId = marker.UserId,
                Timestamp = marker.Timestamp
            };
        }

        /// <summary>
        /// Map <see cref="DataContext.Zone"/> to <see cref="Models.Zone"/>
        /// </summary>
        public static Models.Zone Convert(this DataContext.Zone zone)
        {
            return new Models.Zone()
            {
                UserId = zone.UserId,
                Longitude = zone.Longitude,
                Latitude = zone.Latitude,
                Name = zone.Name,
                Radius = zone.Radius,
                ZoneId = zone.ZoneId
            };
        }

        /// <summary>
        /// Map <see cref="DataContext.Friendlist"/> to <see cref="Models.Friendlist"/>
        /// </summary>
        public static Models.Friendlist Convert(this DataContext.Friendlist item)
        {
            return new Models.Friendlist()
            {
                ItemId = item.ItemId,
                Marked = item.Marked,
                Sender = item.Sender
            };
        }
    }
}
