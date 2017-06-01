namespace GpsTracker.Models.Mappers
{
    public static class IntoDb
    {
        public static DataContext.Event Convert(this Models.Event evnt)
        {
            return new DataContext.Event()
            {
                EventId = evnt.EventId,
                Name = evnt.Name
            };
        }

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

        public static DataContext.Marker Convert(this Models.Marker marker)
        {
            return new DataContext.Marker()
            {
                MarkerId = marker.MarkerId,
                Longtitude = marker.Longtitude,
                Latitude = marker.Latitude,
                UserId = marker.UserId,
                Timestamp = marker.Timestamp
            };
        }

        public static DataContext.Track Convert(this Models.Track track)
        {
            return new DataContext.Track()
            {
                TrackId = track.TrackId, 
                UserId = track.UserId,
                MarkerId = track.MarkerId
            };
        }
    }
}
