using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models {
    public class Person {
        #region Private Variables
        private int _ID;
        private string _FirstName;
        private string _LastName;
        private DateTime _DateOfBirth;
        private bool _IsManager;
        private string _Prefix;
        private string _Postfix;
        private string _Phone;
        private string _Email;
        private string _Homepage;


        #endregion

        #region Public Properties

        [Key]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        [Display(Name = "Surname")]
        public string LastName {
            get { return _LastName; }
            set { _LastName = value; }
        }

        [Display(Name = "Date Born")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        [Display(Name = "Is a Manager")]
        public bool IsManager {
            get { return _IsManager; }
            set { _IsManager = value; }
        }

        [Display(Name = "Title Prefix")]
        [StringLength(6)]
        public string Prefix {
            get { return _Prefix; }
            set { _Prefix = value; }
        }

        [Display(Name = "Title Postfix")]
        [MaxLength(8)]
        public string Postfix {
            get { return _Postfix; }
            set { _Postfix = value; }
        }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone {
            get { return _Phone; }
            set { _Phone = value; }
        }


        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email {
            get { return _Email; }
            set { _Email = value; }
        }


        [Display(Name = "Web Presence")]
        [DataType(DataType.Url)]
        public string Homepage {
            get { return _Homepage; }
            set { _Homepage = value; }
        }




        #endregion
    }
}
