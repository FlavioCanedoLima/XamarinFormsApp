using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsApp.Models
{
    public class HomeItemDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public string Text { get; set; }

        public HomeItemDetail() { }
    }
}
