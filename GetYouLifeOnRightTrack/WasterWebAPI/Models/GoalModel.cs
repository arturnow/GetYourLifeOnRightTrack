namespace WasterWebAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class GoalListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Descirption { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDateTime { get; set; }

        public int Progress { get; set; }

    }

    public class CreateGoalViewModel
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Category { get; set; }

        public string Descirption { get; set; }

        [DataType(DataType.Date)]
        [WasterWebAPI.Validation.NowDatetimeValidation("A coœ tam siê sta³o")]
        public DateTime? DueDateTime { get; set; }

        [Range(0,100)]
        public int Progress { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start date is required")]
        [WasterWebAPI.Validation.NowDatetimeValidation("A coœ tam siê sta³o")]
        public DateTime? StartTime { get; set; }
        [Range(0, 999)]
        public int Duration { get; set; }
    }

    public class EditGoalViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Descirption { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDateTime { get; set; }

        public int Progress { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        public int Duration { get; set; }
    }

    public class GoalViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Descirption { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDateTime { get; set; }

        public int Progress { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        public int Duration { get; set; }
    }
}