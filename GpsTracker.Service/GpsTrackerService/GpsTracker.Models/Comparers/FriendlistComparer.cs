using System;
using System.Collections.Generic;
using GpsTracker.Models.DataContext;

namespace GpsTracker.Models.Comparers
{
    public class FriendlistComparer: IEqualityComparer<Friendlist>
    {
        public bool Equals(Friendlist x, Friendlist y)
        {
            return x.Marked == y.Sender && x.Sender == y.Marked;
        }

        public int GetHashCode(Friendlist obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 0;
            }
            return obj.Marked.GetHashCode() ^ obj.Sender.GetHashCode();
        }
    }
}