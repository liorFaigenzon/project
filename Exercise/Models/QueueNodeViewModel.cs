using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercise.Models
{
    public enum StatusType
    {
        Wait = 0,
        GetService,
        Done
    }
    public class QueueNodeViewModel: IEquatable<QueueNodeViewModel>, IComparable<QueueNodeViewModel>
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [DisplayName("שם לקוח")]
        public string CustomerName { get; set; }

        [DisplayName("מספר תור")]
        public int QueueNumber { get; set; }

        [HiddenInput(DisplayValue = false)]
        public StatusType Status { get; set; }

        [DisplayName("שעת כניסה לתור")]
        [DisplayFormat(DataFormatString = "{0:hh.mm tt}", ApplyFormatInEditMode = true)]
        public DateTime TimeStamp { get; set; }


        public QueueNodeViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public QueueNodeViewModel(int queueNumber)
        {
            Id = Guid.NewGuid().ToString();
            QueueNumber = queueNumber;
            TimeStamp = DateTime.Now;
            Status = StatusType.Wait;
        }

        public int CompareTo(QueueNodeViewModel other)
        {
            return DateTime.Compare(TimeStamp, other.TimeStamp);
        }

        public bool Equals(QueueNodeViewModel other)
        {
           if ((QueueNumber == other.QueueNumber) &&
               (TimeStamp == other.TimeStamp) &&
               (Status == other.Status) &&
               (CustomerName == other.CustomerName))
            {
                return true;
            }

            return false;
        }
    }
}