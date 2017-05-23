using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.Mappers
{
    public static class FromDb
    {
        public static Models.Event Convert(this DataContext.Event evnt)
        {
            return new Models.Event()
            {
                EventId = evnt.EventId,
                Name = evnt.Name
            };
        }

        public static Models.Log Convert(this DataContext.Log log)
        {
            return new Models.Log()
            {
                LogId = log.LogId,
                EventId = log.EventId,
                EventDate = log.EventDate,
                Message = log.Message
            };
        }

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

        public static Models.Marker Convert(this DataContext.Marker marker)
        {
            return new Models.Marker()
            {
                MarkerId = marker.MarkerId,
                Longtitude = (float)marker.Longtitude,
                Latitude = (float)marker.Latitude,
                UserId = marker.UserId,
                Timestamp = marker.Timestamp
            };
        }

        public static Models.Track Convert(this DataContext.Track track)
        {
            return new Models.Track()
            {
                TrackId = track.TrackId,
                UserId = track.UserId,
                MarkerId = track.MarkerId
            };
        }
    }
}
