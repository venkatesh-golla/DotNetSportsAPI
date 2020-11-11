﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAPI.Classes
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _size=20;

        public int Page { get; set; }
        public int Size { get { return _size; } set { _size = Math.Min(_maxSize, value); } }
        public string sortBy { get; set; } = "Id";
        public string _sortOrder = "asc";
        public string SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    _sortOrder = value;
                }
            }
        }
            
    }
}
