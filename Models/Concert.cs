//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Symphony.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Concert
    {
        public Concert()
        {
            this.A3 = new HashSet<A3>();
            this.A4 = new HashSet<A4>();
        }
    
        public virtual ICollection<A3> A3 { get; set; }
        public virtual ICollection<A4> A4 { get; set; }
    }
}
