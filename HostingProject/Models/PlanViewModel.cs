using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostingProject.Models
{
    public class PlanViewModel : SharedService
    {

        [Key]
        public int planId { get; set; }
        public string planName { get; set; }
        public string planType { get; set; }
        public string serverType { get; set; }
        public string storage { get; set; }
        public string RAM { get; set; }
        public string database { get; set; }
        public double bandwidth { get; set; }
        public string CPU { get; set; }
        public string protection { get; set; }
        public bool ssl { get; set; }
        public bool upgrade { get; set; }

        [Column("price")]
        public double price { get; set; }

    }
}
