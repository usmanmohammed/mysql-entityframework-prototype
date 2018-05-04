using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrainPunch.Models
{
    public class Game_Schedule
    {
        [Key]
        [Display(Name = "Schedule ID")]
        public int ScheduleID { get; set; }
        [Display(Name = "First Opponent")]
        public int FirstOpponentID { get; set; }
        [Display(Name = "Second Opponent")]
        public int SecondOpponentID { get; set; }
        [Display(Name = "Schedule Date")]
        public DateTime ScheduleDate { get; set; }
        [Display(Name = "Winner")]
        public int WinnerID { get; set; }
    }
}