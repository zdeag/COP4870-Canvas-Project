﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class Announcement
    {
        private static int lastId = 0;
        private int id = 0;
        public int Id
        {
            get
            {
                if (id == 0)
                {
                    id = ++lastId;
                }
                return id;
            }
        }
        public string announcementContent { get; set; }

        public override string ToString()
        {
            return $"Announcement Content [{Id}]: {announcementContent}";
        }

    }
}
