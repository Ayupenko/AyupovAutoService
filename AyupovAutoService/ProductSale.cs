//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AyupovAutoService
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSale
    {
        public int ID { get; set; }
        public System.DateTime SaleDate { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> ClientServiceID { get; set; }
    
        public virtual ClientService ClientService { get; set; }
        public virtual Product Product { get; set; }
    }
}
