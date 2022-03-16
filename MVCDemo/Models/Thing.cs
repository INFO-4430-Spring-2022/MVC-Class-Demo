//Created By: Jon Holmes (using Code generator)
//Created On: 3/14/2022 11:36:48 AM
using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
namespace MVCDemo.Models {
    /// <summary>
    /// TODO: Comment this
    /// </summary>
    /// <remarks></remarks>

    public class Thing : DatabaseRecord {
        #region Constructors
        public Thing() {
        }
        internal Thing(Microsoft.Data.SqlClient.SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "ThingID";
        internal const string db_Name = "Name";
        internal const string db_Description = "Description";
        internal const string db_Type = "TypeID";

        #endregion

        #region Private Variables
        private string _Name;
        private string _Description;
        private int _Type;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Name for this Creation_and_Inserts.Thing object.
        /// </summary>
        /// <remarks></remarks>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the Description for this Creation_and_Inserts.Thing object.
        /// </summary>
        /// <remarks></remarks>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the Type for this Creation_and_Inserts.Thing object.
        /// </summary>
        /// <remarks></remarks>
        public int Type {
            get {
                return _Type;
            }
            set {
                _Type = value;
            }
        }


        #endregion

        #region Public Functions
        /// <summary>
        /// Calls DAL function to add Thing to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbAdd() {
            _ID = fDAL.AddThing(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update Thing to the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbUpdate() {
            return fDAL.UpdateThing(this);
        }

        /// <summary>
        /// Calls DAL function to remove Thing from the database.
        /// </summary>
        /// <remarks></remarks>
        public override int dbRemove() {
            return fDAL.RemoveThing(this);
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
            _Description = (string)dr[db_Description];
            _Type = (int)dr[db_Type];
        }

        #endregion

        public override string ToString() {
            return String.Format("{0} | {1}", this.ID, this.Name);
        }

    }
}
