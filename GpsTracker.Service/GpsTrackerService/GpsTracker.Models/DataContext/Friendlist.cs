//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GpsTracker.Models.DataContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Friendlist
    {
        public int ItemId { get; set; }
        public int Sender { get; set; }
        public int Marked { get; set; }
        public System.DateTime CreatedAt { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
