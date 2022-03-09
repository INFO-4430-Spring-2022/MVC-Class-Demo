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


        public List<Person> GetPeople() { // READ ALL
            List<Person> people = new List<Person>();
            people.Add(new Person() {ID=1, FirstName = "Bob", LastName="Awesome" });
            people.Add(new Person() {ID=2, FirstName = "Michael", LastName = "Scott" });
            people.Add(new Person() {ID=3, FirstName = "Bruce", LastName = "Banner" });
            people.Add(new Person() {ID=4, FirstName = "Chris", LastName = "Lass" });
            people.Add(new Person() {ID=5, FirstName = "Wayde", LastName = "Wilson" });
            people.Add(new Person() {ID=6, FirstName = "Zach", LastName = "Daniels" });
            people.Add(new Person() {ID=7, FirstName = "Leroy", LastName = "Jenkins" });
            people.Add(new Person() {ID=8, FirstName = "Hulk", LastName = "Hogan" });
            people.Add(new Person() {ID=9, FirstName = "Greg", LastName = "Williams" });
            people.Add(new Person() {ID=10, FirstName = "Jack", LastName = "Black" });
            people.Add(new Person() {ID=11, FirstName = "Sally", LastName = "Super" });

            return people;
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


    }
}
