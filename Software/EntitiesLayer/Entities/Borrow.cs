namespace EntitiesLayer.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Borrow")]
    public partial class Borrow {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Book_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Member_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime borrow_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? return_date { get; set; }

        public int borrow_status { get; set; }

        public virtual Book Book { get; set; }

        public virtual Member Member { get; set; }
    }
}
