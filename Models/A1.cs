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
    
    public partial class A1
    {
        public A1()
        {
            this.A2 = new HashSet<A2>();
        }
    
        public virtual Instrument Instrument { get; set; }
        public virtual Stringer Stringer { get; set; }
        public virtual ICollection<A2> A2 { get; set; }
    }
}
