﻿using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace MVCDemo.Models {
    public abstract class DatabaseRecord {
        protected int _ID;
        [Key]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        public abstract int dbAdd();
        public abstract int dbUpdate();
        public abstract int dbRemove();
      
        public abstract void Fill(SqlDataReader dr);
    }
}
