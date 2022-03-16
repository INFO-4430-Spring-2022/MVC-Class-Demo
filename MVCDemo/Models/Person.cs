
using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models {
    public class Person : DatabaseRecord {
        #region Database String
        internal const string db_ID = "PersonID";
        internal const string db_FirstName = "FirstName";
        internal const string db_LastName = "LastName";
        internal const string db_DateOfBirth = "DateOfBirth";
        internal const string db_IsManager = "IsManager";
        internal const string db_Prefix = "Prefix";
        internal const string db_Postfix = "Postfix";
        internal const string db_Phone = "Phone";
        internal const string db_Email = "Email";
        internal const string db_Homepage = "Homepage";

        #endregion
        #region Private Variables
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

        #region Constructors
        public Person() {
        }
        internal Person(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Public Properties

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


        #region Public Functions
        /// <summary>
        /// Calls DAL function to add People to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = fDAL.AddPerson(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update People to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return fDAL.UpdatePerson(this);
        }

        /// <summary>
        /// Calls DAL function to remove People from the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbRemove() {
            return fDAL.RemovePerson(this);
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft.Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _FirstName = (string)dr[db_FirstName];
            _LastName = (string)dr[db_LastName];
            _DateOfBirth = (DateTime)dr[db_DateOfBirth];
            _IsManager = (bool)dr[db_IsManager];
            _Prefix = (string)dr[db_Prefix];
            _Postfix = (string)dr[db_Postfix];
            _Phone = (string)dr[db_Phone];
            _Email = (string)dr[db_Email];
            _Homepage = (string)dr[db_Homepage];
        }

        #endregion

        public override string ToString() {
            return String.Format("{0} | {1} {2}", this.ID, this.FirstName, this.LastName);
        }






    }
}
