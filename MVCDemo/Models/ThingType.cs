using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models {
    public class ThingType {

        #region Private Variables
        private int _ID;
        private string _Name;
        private bool _CanShare;
        #endregion

        #region Properties
        ////Accessor 
        //public int getID() { return _ID; }
        //// Mutator
        //public void setID(int value) { _ID = value; }
        [Key]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        [Display(Name = "Name")]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        [Display(Name="Can share this type of item")]
        public bool CanShare {
            get { return _CanShare; }
            set { _CanShare = value; }
        }

        #endregion

    }
}
