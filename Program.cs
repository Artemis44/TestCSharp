using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.ComponentModel;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Converting a list of object to a JSON*/
            List<User> userList = new List<User>();
            User user = new User();
            user.Age = 20;
            user.Name = "Johny";
            user.City = "California";
            user.Address = "Ford street, New york.";

            userList.Add(user);

            User user1 = new User();
            user1.Age = 20;
            user1.Name = "Adam";
            user1.City = "Sydney";
            user1.Address = "xyz street, CA.";

            userList.Add(user1);

            string jsonString = JsonConvert.SerializeObject(userList);
            Console.WriteLine("Converting a list of object to json "+jsonString); //prints the json

            /*convert an object to JSON*/
            User user2 = new User();
            user2.Age = 20;
            user2.Name = "John";
            user2.City = "Mumbai";
            user2.Address = "Man road, Mumbai.";

            jsonString = JsonConvert.SerializeObject(user2);
            Console.WriteLine("Convert an object to JSON "+jsonString);

            /*JSON to XML*/
            jsonString = JsonConvert.SerializeObject(user2);
            XNode node = JsonConvert.DeserializeXNode(jsonString, "User");
            Console.WriteLine("JSON to XML "+node.ToString());

            /*XML to JSON*/
            jsonString = JsonConvert.SerializeXNode(node);
            Console.WriteLine("XML to JSON"+jsonString);

            /*Deserialize JSON to an object*/
            Human objUser = JsonConvert.DeserializeObject<Human>(jsonString);
            Console.WriteLine("And the adress is : " + objUser.user.Address);
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(objUser.user))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(objUser.user);
                Console.WriteLine("{0}={1}", name, value);
            }

            /*Its a new beginning*/
            string json = @"{
  'disclaimer': 'This data is collected from various providers ...',
  'license': 'Data collected from various providers with public-facing APIs ...',
  'timestamp': 1336741253,
  'devise': 'USD',
  'rates': {
    'AED': 3.6731,
    'AFN': 48.419998,
    'ALL': 107.949997,
    'AMD': 393.410004,
    'ANG': 1.79,
    'AOA': 94.949997
  }
}";

            RootObject ro = JsonConvert.DeserializeObject<RootObject>(json);

            Console.WriteLine("And the devise is : " + ro.devise);
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(ro))
            {
                string name = descriptor.Name;
                object value = null;
                if (name == "rates")
                {
                    var props = typeof(Rates).GetProperties();
                    foreach (var prop in props)
                    {
                        Console.WriteLine("{0}\t", prop.Name + " = " + prop.GetValue(ro.rates, null));
                    }
                }
                else
                {
                    value = descriptor.GetValue(ro);
                    Console.WriteLine("{0}={1}", name, value);
                }
            }
        }


        private double pythagorean(double s1, double s2)
        {
            double hypot;
            string str;

            Console.WriteLine("Enter length of first side: ");
            str = Console.ReadLine();
            s1 = Double.Parse(str);

            Console.WriteLine("Enter length of second side: ");
            str = Console.ReadLine();
            s2 = Double.Parse(str);

            hypot = Math.Sqrt(s1 * s1 + s2 * s2);
            Console.WriteLine("Hypotenuse is " + hypot); 
            return hypot;
        }
    }


    public class Human
    {
        public User user { get; set; }
    }

    public class User
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class Rates
    {
        public double AED { get; set; }
        public double AFN { get; set; }
        public double ALL { get; set; }
        public double AMD { get; set; }
        public double ANG { get; set; }
        public double AOA { get; set; }
    }

    public class RootObject
    {
        public string disclaimer { get; set; }
        public string license { get; set; }
        public int timestamp { get; set; }
        public string devise { get; set; }
        public Rates rates { get; set; }
    }
}