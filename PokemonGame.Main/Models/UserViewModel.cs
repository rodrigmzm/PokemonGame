﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonGame.Main.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}