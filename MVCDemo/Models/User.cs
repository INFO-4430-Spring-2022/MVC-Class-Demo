using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models {
    public class User :DatabaseRecord{
        #region Database String
        internal const string db_ID = "UserID";
        internal const string db_UserName = "UserName";
        internal const string db_Password = "Password";
        internal const string db_Email = "Email";
        internal const string db_Role = "RoleID";

        #endregion
        #region Private Variables
        private string _UserName;
        private string _Password;
        private string _Email;
        private int _RoleID;
        

        #endregion

        #region Constructors
        public User() {
        }
        internal User(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Public Properties

        [Display(Name = "User Name")]
        [Required]
        public string UserName {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [Display(Name = "Password")]
        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }
                
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email {
            get { return _Email; }
            set { _Email = value; }
        }

        [Display(Name = "Role")]
        public int RoleID {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        #endregion


        #region Public Functions
        /// <summary>
        /// Calls DAL function to add People to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = fDAL.AddUser(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update People to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return fDAL.UpdateUser(this);
        }

        /// <summary>
        /// Calls DAL function to remove People from the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbRemove() {
            return fDAL.RemoveUser(this);
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft.Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _UserName = (string)dr[db_UserName];
            _Password = (string)dr[db_Password];
            _Email = (string)dr[db_Email];
            _RoleID = (int)dr[db_Role];
        }

        #endregion

    }
}
