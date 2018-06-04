using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WasterWebAPI.Models
{
    public class PatternModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }
        public string Pattern { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// To raczej nie będzie potrzebne
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? UpdateDate { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsWorking { get; set; }
    }
}