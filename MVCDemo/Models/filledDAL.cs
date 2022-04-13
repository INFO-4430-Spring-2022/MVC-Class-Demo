using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Configuration;


namespace MVCDemo.Models {
    public class fDAL {
        private static string ReadOnlyConnectionString = "Server=localhost; Database=MVCDemo;Trusted_Connection=True;Encrypt=False";
        private static string EditOnlyConnectionString = "Server=localhost; Database=MVCDemo;Trusted_Connection=True;Encrypt=False";
        private fDAL() {
        }
        internal enum dbAction {
            Read,
            Edit
        }

        #region Database Connections
        internal static void ConnectToDatabase(SqlCommand comm, dbAction action = dbAction.Read) {
            try {
                if (action == dbAction.Edit)
                    comm.Connection = new SqlConnection(EditOnlyConnectionString);
                else
                    comm.Connection = new SqlConnection(ReadOnlyConnectionString);

                comm.CommandType = System.Data.CommandType.StoredProcedure;
            } catch (Exception ex) {
                DisplayException(ex);
            }
        }
        public static SqlDataReader GetDataReader(SqlCommand comm) {
            SqlDataReader reader = null;
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                ConnectToDatabase(comm);
                comm.Connection.Open();
                reader = comm.ExecuteReader();
            } catch (Exception ex) {
                DisplayException(ex);
                reader = null;
            }
            return reader;
        }



        internal static int AddObject(SqlCommand comm, string parameterName) {
            int retInt = 0;
            try {
                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                SqlParameter retParameter;
                retParameter = comm.Parameters.Add(parameterName, System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
            } catch (Exception ex) {
                retInt = -1;
                DisplayException(ex);

            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }

            }
            return retInt;
        }


        /// <summary>
        /// Sets Connection and Executes given comm on the database
        /// </summary>
        /// <param name="comm">SQLCommand to execute</param>
        /// <returns>number of rows affected; -1 on failure, positive value on success.</returns>
        /// <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.
        ///       Edit Connection is set here.</remarks>
        internal static int UpdateObject(SqlCommand comm) {
            int retInt = 0;
            try {
                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();

            } catch (Exception ex) {
                retInt = -1;
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retInt;
        }
        private static void DisplayException(Exception ex) {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            System.Diagnostics.Debug.WriteLine(ex.StackTrace);
        }

        #endregion


        #region Object Creation Methods

        private static T Build<T>(SqlDataReader dr) where T : DatabaseRecord, new() {
            T ret = new T();
            ret.Fill(dr);
            return ret;
        }

  

        #endregion



        #region ThingType
        /// <summary>
        /// Gets the ThingType correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static ThingType GetThingType(String idstring, Boolean retNewObject) {
            ThingType retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new ThingType();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetThingType(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the ThingTypecorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static ThingType GetThingType(int id) {
            SqlCommand comm = new SqlCommand("sprocThingTypeGet");
            ThingType retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + ThingType.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new ThingType(dr);
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all ThingType objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<ThingType> GetThingTypes() {
            SqlCommand comm = new SqlCommand("sprocThingTypesGetAll");
            List<ThingType> retList = new List<ThingType>();
            try {
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new ThingType(dr));
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given ThingType
        /// </summary>
        /// <remarks></remarks>

        internal static int AddThingType(ThingType obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_ThingTypeAdd");
            try {
                comm.Parameters.AddWithValue("@" + ThingType.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + ThingType.db_CanShare, obj.CanShare);
                return AddObject(comm, "@" + ThingType.db_ID);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given ThingType
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateThingType(ThingType obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_ThingTypeUpdate");
            try {
                comm.Parameters.AddWithValue("@" + ThingType.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + ThingType.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + ThingType.db_CanShare, obj.CanShare);
                return UpdateObject(comm);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the ThingType
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveThingType(ThingType obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                comm.CommandText = "sproc_ThingTypeRemove";
                comm.Parameters.AddWithValue("@" + ThingType.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        #endregion

        #region Thing
        /// <summary>
        /// Gets the Thing correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Thing GetThing(String idstring, Boolean retNewObject) {
            Thing retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Thing();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetThing(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the Thingcorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Thing GetThing(int id) {
            SqlCommand comm = new SqlCommand("sprocThingGet");
            Thing retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Thing.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new Thing(dr);
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all Thing objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Thing> GetThings() {
            SqlCommand comm = new SqlCommand("sprocThingsGetAll");
            List<Thing> retList = new List<Thing>();
            try {
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new Thing(dr));
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retList;
        }


        /// <summary>
        /// Gets a list of all Thing objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Thing> GetThings(Person p) {
            SqlCommand comm = new SqlCommand("sprocThingsGetForPerson");
            comm.Parameters.AddWithValue("@" + Person.db_ID, p.ID);
            List<Thing> retList = new List<Thing>();
            try {
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new Thing(dr));
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given Thing
        /// </summary>
        /// <remarks></remarks>

        internal static int AddThing(Thing obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_ThingAdd");
            try {
                comm.Parameters.AddWithValue("@" + Thing.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Thing.db_Description, obj.Description);
                comm.Parameters.AddWithValue("@" + Thing.db_Type, obj.TypeID);
                return AddObject(comm, "@" + Thing.db_ID);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Thing
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateThing(Thing obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_ThingUpdate");
            try {
                comm.Parameters.AddWithValue("@" + Thing.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Thing.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Thing.db_Description, obj.Description);
                comm.Parameters.AddWithValue("@" + Thing.db_Type, obj.TypeID);
                return UpdateObject(comm);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Thing
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveThing(Thing obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                comm.CommandText = "sproc_ThingRemove";
                comm.Parameters.AddWithValue("@" + Thing.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        #endregion

        #region Person
        /// <summary>
        /// Gets the Person correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Person GetPerson(String idstring, Boolean retNewObject) {
            Person retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Person();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetPerson(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the Personcorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Person GetPerson(int id) {
            SqlCommand comm = new SqlCommand("sprocPersonGet");
            Person retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Person.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    //retObj = new Person(dr);
                    // or
                    retObj = Build<Person>(dr);
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all Person objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Person> GetPeople() {
            SqlCommand comm = new SqlCommand("sprocPeopleGetAll");
            List<Person> retList = new List<Person>();
            try {
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    // retList.Add(new Person(dr));
                    // or
                    retList.Add(Build<Person>(dr));
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given Person
        /// </summary>
        /// <remarks></remarks>

        internal static int AddPerson(Person obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_PersonAdd");
            try {
                comm.Parameters.AddWithValue("@" + Person.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + Person.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + Person.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + Person.db_IsManager, obj.IsManager);
                comm.Parameters.AddWithValue("@" + Person.db_Prefix, obj.Prefix);
                comm.Parameters.AddWithValue("@" + Person.db_Postfix, obj.Postfix);
                comm.Parameters.AddWithValue("@" + Person.db_Phone, obj.Phone);
                comm.Parameters.AddWithValue("@" + Person.db_Email, obj.Email);
                comm.Parameters.AddWithValue("@" + Person.db_Homepage, obj.Homepage);
                return AddObject(comm, "@" + Person.db_ID);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Person
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdatePerson(Person obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_PersonUpdate");
            try {
                comm.Parameters.AddWithValue("@" + Person.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Person.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + Person.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + Person.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + Person.db_IsManager, obj.IsManager);
                comm.Parameters.AddWithValue("@" + Person.db_Prefix, obj.Prefix);
                comm.Parameters.AddWithValue("@" + Person.db_Postfix, obj.Postfix);
                comm.Parameters.AddWithValue("@" + Person.db_Phone, obj.Phone);
                comm.Parameters.AddWithValue("@" + Person.db_Email, obj.Email);
                comm.Parameters.AddWithValue("@" + Person.db_Homepage, obj.Homepage);
                return UpdateObject(comm);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Person
        /// </summary>
        /// <remarks></remarks>
        internal static int RemovePerson(Person obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                comm.CommandText = "sproc_PersonRemove";
                comm.Parameters.AddWithValue("@" + Person.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Person
        /// </summary>
        /// <remarks></remarks>
        internal static int RemovePersonThing(Person per, Thing thg) {
            if (per == null || thg == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                comm.CommandText = "sproc_PersonThingRemove";
                comm.Parameters.AddWithValue("@" + Person.db_ID, per.ID);
                comm.Parameters.AddWithValue("@" + Thing.db_ID, thg.ID);

                return UpdateObject(comm);
            } catch (Exception ex) {
                DisplayException(ex);
            }
            return -1;
        }



        #endregion

        #region Users and Roles

        /// <summary>
        /// Gets the User correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static User GetUser(String idstring, Boolean retNewObject) {
            User retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new User();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetUser(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the Usercorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static User GetUser(int id) {
            SqlCommand comm = new SqlCommand("sprocUserGet");
            User retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + User.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new User(dr);
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all User objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<User> GetUsers() {
            SqlCommand comm = new SqlCommand("sprocUsersGetAll");
            List<User> retList = new List<User>();
            try {
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new User(dr));
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retList;
        }



        internal static int AddUser(User user) {
            throw new NotImplementedException();
        }

        internal static int RemoveUser(User user) {
            throw new NotImplementedException();
        }

        internal static int UpdateUser(User user) {
            throw new NotImplementedException();
        }




        /// <summary>
        /// Gets the Role correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Role GetRole(String idstring, Boolean retNewObject) {
            Role retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Role();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetRole(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the Rolecorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Role GetRole(int id) {
            SqlCommand comm = new SqlCommand("sprocRoleGet");
            Role retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Role.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new Role(dr);
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all Role objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Role> GetRoles() {
            SqlCommand comm = new SqlCommand("sprocRolesGetAll");
            List<Role> retList = new List<Role>();
            try {
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new Role(dr));
                }
            } catch (Exception ex) {
                DisplayException(ex);
            } finally {
                if (comm != null && comm.Connection != null) {
                    comm.Connection.Close();
                }
            }
            return retList;
        }


        internal static int AddRole(Role role) {
            throw new NotImplementedException();
        }

        internal static int UpdateRole(Role role) {
            throw new NotImplementedException();
        }
        internal static int RemoveRole(Role role) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
