﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRun
{
    public class MenuItem
    {
        public MenuItem()
        {
            TargetType = typeof(MenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string icono { get; set; }
        public Type TargetType { get; set; }
    }
}