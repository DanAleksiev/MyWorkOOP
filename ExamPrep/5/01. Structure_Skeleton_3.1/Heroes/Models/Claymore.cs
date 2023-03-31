﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
    {
    public class Claymore : Weapon
        {
        private const int damage = 20;
        public Claymore(string name, int durability) : base(name, durability)
            {
            }

        public override int DoDamage()
            {
            return damage;
            }
        }
    }
