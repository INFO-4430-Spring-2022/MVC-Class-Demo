using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models {
    public class Role : DatabaseRecord {
        #region Database String
        internal const string db_ID = "RoleID";
        internal const string db_Name = "Name";
        internal const string db_IsAdmin = "IsAdmin";
        internal const string db_CanViewPerson = "CanViewPerson";

        internal const string db_CanAddPerson = "CanAddPerson";
        internal const string db_CanEditPerson = "CanEditPerson";

        internal const string db_CanViewThing = "CanViewThing";
        internal const string db_CanAddThing = "CanAddThing";

        internal const string db_CanEditThing = "CanEditThing";
        internal const string db_CanViewThingType = "CanViewThingType";

        internal const string db_CanAddThingType = "CanAddThingType";
        internal const string db_CanEditThingType = "CanEditThingType";

        internal const string db_CanViewUser = "CanViewUser";
        internal const string db_CanAddUser = "CanAddUser";
        internal const string db_CanEditUser = "CanEditUser";

        #endregion
        #region Private Variables
        private string _Name;
        private bool _IsAdmin = false;
        private bool _CanViewPerson = false;
        private bool _CanAddPerson = false;
        private bool _CanEditPerson = false;
        private bool _CanViewThing = false;
        private bool _CanAddThing = false;
        private bool _CanEditThing = false;
        private bool _CanViewThingType = false;
        private bool _CanAddThingType = false;
        private bool _CanEditThingType = false;
        private bool _CanViewUser = false;
        private bool _CanAddUser = false;
        private bool _CanEditUser = false;


        #endregion

        #region Constructors
        public Role() {
        }
        internal Role(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Public Properties

        [Display(Name = "Name")]
        [Required]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        public bool IsAdmin { get { return _IsAdmin; } set { _IsAdmin = value; } }
        
        public bool CanViewPerson { get { return _CanViewPerson; } set { _CanViewPerson = value; } }
        public bool CanAddPerson { get { return _CanAddPerson; } set { _CanAddPerson = value; } }
        public bool CanEditPerson { get { return _CanEditPerson; } set { _CanEditPerson = value; } }
        public bool CanViewThing { get { return _CanViewThing; } set { _CanViewThing = value; } }
        public bool CanAddThing { get { return _CanAddThing; } set { _CanAddThing = value; } }
        public bool CanEditThing { get { return _CanEditThing; } set { _CanEditThing = value; } }
        public bool CanViewThingType { get { return _CanViewThingType; } set { _CanViewThingType = value; } }
        public bool CanAddThingType { get { return _CanAddThingType; } set { _CanAddThingType = value; } }
        public bool CanEditThingType { get { return _CanEditThingType; } set { _CanEditThingType = value; } }
        public bool CanViewUser { get { return _CanViewUser; } set { _CanViewUser = value; } }
        public bool CanAddUser { get { return _CanAddUser; } set { _CanAddUser = value; } }
        public bool CanEditUser {
            get { return _CanEditUser; }
            set { _CanEditUser = value; }
        }

        #endregion


            #region Public Functions
            /// <summary>
            /// Calls DAL function to add People to the database.
            /// </summary>
            /// <remarks></remarks>
        public override int dbAdd() {
            _ID = fDAL.AddRole(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update People to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return fDAL.UpdateRole(this);
        }

        /// <summary>
        /// Calls DAL function to remove People from the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbRemove() {
            return fDAL.RemoveRole(this);
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(Microsoft.Data.SqlClient.SqlDataReader dr) {
            _ID = (int)dr[db_ID];
            _Name = (string)dr[db_Name];
            _IsAdmin = (bool)dr[db_IsAdmin];
            _CanViewPerson = (bool)dr[db_CanViewPerson];
            _CanAddPerson = (bool)dr[db_CanAddPerson];
            _CanEditPerson = (bool)dr[db_CanEditPerson];
            _CanViewThing = (bool)dr[db_CanViewThing];
            _CanAddThing = (bool)dr[db_CanAddThing];
            _CanEditThing = (bool)dr[db_CanEditThing];
            _CanViewThingType = (bool)dr[db_CanViewThingType];
            _CanAddThingType = (bool)dr[db_CanAddThingType];
            _CanEditThingType = (bool)dr[db_CanEditThingType];
            _CanViewUser = (bool)dr[db_CanViewUser];
            _CanAddUser = (bool)dr[db_CanAddUser];
            _CanEditUser = (bool)dr[db_CanEditUser];
        }

        #endregion


    }
}
