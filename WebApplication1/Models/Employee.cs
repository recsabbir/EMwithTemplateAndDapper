//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Web;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Users = new HashSet<User>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string nid { get; set; }
        public string bgroup { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string pic { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public Boolean CheckEqual(Employee emp)
        {
            if (this.id == emp.id && this.name == emp.name && this.dob == emp.dob
                && this.nid == emp.nid && this.bgroup == emp.bgroup && this.email == emp.email
                && this.phone == emp.phone && this.address == emp.address && this.pic == emp.pic)
            {
                return true;
            }

            return false;
        }

    }
}
