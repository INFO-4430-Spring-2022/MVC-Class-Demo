using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models {
    public class ThingType : DatabaseRecord {
        #region Database String
        internal const string db_ID = "ThingTypeID";
        internal const string db_Name = "Name";
        internal const string db_CanShare = "CanShare";

        #endregion
        #region Private Variables
        private string _Name;
        private bool _CanShare;
        #endregion

        #region Constructors
        public ThingType() {
        }
        internal ThingType(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Properties
        ////Accessor 
        //public int getID() { return _ID; }
        //// Mutator
        //public void setID(int value) { _ID = value; }

        // Moved to Base Class
        //[Key]
        //public int ID {
        //    get { return _ID; }
        //    set { _ID = value; }
        //}

        [Display(Name = "Name")]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        [Display(Name = "Can share this type of item")]
        public bool CanShare {
            get { return _CanShare; }
            set { _CanShare = value; }
        }

        #endregion


        #region Public Functions
        /// <summary>
        /// Calls DAL function to add ThingType to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = fDAL.AddThingType(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update ThingType to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return fDAL.UpdateThingType(this);
        }

        /// <summary>
        /// Calls DAL function to remove ThingType from the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbRemove() {
            return fDAL.RemoveThingType(this);
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
            _CanShare = (bool)dr[db_CanShare];
        }

        #endregion

        public override string ToString() {
            return String.Format("{0} | {1}", this.ID, this.Name);
        }


    }
}
