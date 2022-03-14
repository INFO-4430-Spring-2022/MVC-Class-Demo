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

        public static List<Person> _People = null;

        private static List<Person> GetPeopleList() {
            return new List<Person>() {
                new Person() { ID = 1, FirstName = "Bob", LastName = "Awesome" },
                new Person() { ID = 2, FirstName = "Michael", LastName = "Scott" },
                new Person() { ID = 3, FirstName = "Bruce", LastName = "Banner" },
                new Person() { ID = 4, FirstName = "Chris", LastName = "Lass" },
                new Person() { ID = 5, FirstName = "Wayde", LastName = "Wilson" },
                new Person() { ID = 6, FirstName = "Zach", LastName = "Daniels" },
                new Person() { ID = 12, FirstName = "Samantha", LastName = "Day" },
                new Person() { ID = 7, FirstName = "Leroy", LastName = "Jenkins" },
                new Person() { ID = 8, FirstName = "Hulk", LastName = "Hogan" },
                new Person() { ID = 9, FirstName = "Greg", LastName = "Williams" },
                new Person() { ID = 10, FirstName = "Jack", LastName = "Black" },
                new Person() { ID = 11, FirstName = "Sally", LastName = "Super" }
            };
        }


            public List<Person> GetPeople() { // READ ALL
            if (_People == null) {
                _People = GetPeopleList();
            }
            return _People;
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
