using System;

namespace Model.ViewModels.Reports
{
    public class LogVM
    {
        public DateTime LogDate { get; set; }

        public int Hits { get; set; }

        public Guid PublicationID { get; set; }
    }
}