//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogicLayer.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department_Expense
    {
        public int id { get; set; }
        public Nullable<int> department_id { get; set; }
        public Nullable<int> expense_id { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Expense Expense { get; set; }
    }
}
