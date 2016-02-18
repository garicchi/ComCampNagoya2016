using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BloodSurvey.Models
{
    public class BloodQuestion
    {
        public string Id { get; set; }
        public string Question { get; set; }
    }
    

    public class BloodFactory
    {
        public static List<BloodQuestion> GetQuestions()
        {
            var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/question.csv"),Encoding.UTF8);
            var list = new List<BloodQuestion>();
            string line = "";
            int counter = 1;
            while ((line = reader.ReadLine()) != null)
            {
                list.Add(new BloodQuestion
                {
                    Id = counter.ToString(),
                    Question = line.Split(',')[0]
                });
                counter++;
            }
            return list;
        }
    }
}
