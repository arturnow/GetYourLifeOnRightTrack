namespace WasterWebAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ReadTaskViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDateTime { get; set; }
    }

    public class DetailTaskViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDateTime { get; set; }

        public int Progress { get; set; }

        public int Priority { get; set; }
    }

    public class CreateTaskViewModel
    {
        //[HiddenInput(DisplayValue = false)]
        //public Guid GoalId { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDateTime { get; set; }

        [Range(0,100)]
        public int Progress { get; set; }

        [Range(0,5)]
        public int Priority { get; set; }
    }

    public class TaskViewItem
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDateTime { get; set; }

        [Range(0, 100)]
        public int Progress { get; set; }

        [Range(0, 5)]
        public int Priority { get; set; }
    }
}