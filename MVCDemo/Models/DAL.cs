using Microsoft.Data.SqlClient;

namespace MVCDemo.Models {
    /// <summary>
    /// Class to access the Database
    /// </summary>
    public class DAL {

        // C R U D
        // CREATE Add
        // READ   View
        // READALL 
        // UPDATE Edit
        // DELETE Disable

        //public static List<Person> _People = null;

        //private static List<Person> GetPeopleList() {
        //    return new List<Person>() {
        //        new Person() { ID = 1, FirstName = "Bob", LastName = "Awesome" },
        //        new Person() { ID = 2, FirstName = "Michael", LastName = "Scott" },
        //        new Person() { ID = 3, FirstName = "Bruce", LastName = "Banner" },
        //        new Person() { ID = 4, FirstName = "Chris", LastName = "Lass" },
        //        new Person() { ID = 5, FirstName = "Wayde", LastName = "Wilson" },
        //        new Person() { ID = 6, FirstName = "Zach", LastName = "Daniels" },
        //        new Person() { ID = 12, FirstName = "Samantha", LastName = "Day" },
        //        new Person() { ID = 7, FirstName = "Leroy", LastName = "Jenkins" },
        //        new Person() { ID = 8, FirstName = "Hulk", LastName = "Hogan" },
        //        new Person() { ID = 9, FirstName = "Greg", LastName = "Williams" },
        //        new Person() { ID = 10, FirstName = "Jack", LastName = "Black" },
        //        new Person() { ID = 11, FirstName = "Sally", LastName = "Super" }
        //    };
        //}


            public List<Person> GetPeople() { // READ ALL

            List<Person> retList = new List<Person>();
                SqlCommand comm = new SqlCommand();

            try {
               SqlConnection conn = new SqlConnection(
                   "Server = localhost; Database = MVCDemo;Trusted_Connection=True;Encrypt=False");
                    //"Server = localhost; Database = MVCDemo; User Id = mvcdemouser; Password = pass1234;");
                conn.Open();
                comm.Connection = conn;
                //comm.CommandText = "SELECT * FROM People" ;
                comm.CommandText = "sprocPeopleGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read()) {
                    Person pers = new Person();
                    pers.FirstName = dr["FirstName"].ToString();
                    pers.LastName = (string)dr["LastName"];
                    retList.Add(pers);
                }
            } catch (Exception ex) {

            } finally {
                if (comm.Connection != null) {
                    comm.Connection.Close();
                }
            }


            return retList;

            //if (_People == null) {
            //    _People = GetPeopleList();
            //}
            //return _People;
        }

        public Person GetPerson(int id) { // READ One
            Person person = null;

            foreach (Person p in GetPeople()) {
                if (p.ID == id) {
                    person = p;
                }
            }

            return person;
        }

        internal void UpdatePerson(Person newPersonData) {

            int idOfPersonToChange = newPersonData.ID;
            Person oriPerson = GetPerson(idOfPersonToChange);

            oriPerson.FirstName = newPersonData.FirstName;
            oriPerson.LastName = newPersonData.LastName;
            oriPerson.Email = newPersonData.Email;
            oriPerson.IsManager = newPersonData.IsManager;
            oriPerson.Prefix = newPersonData.Prefix;
            oriPerson.Phone = newPersonData.Phone;
            oriPerson.Homepage = newPersonData.Homepage;
            oriPerson.DateOfBirth = newPersonData.DateOfBirth;
            oriPerson.Postfix = newPersonData.Postfix;


        }
    }
}
