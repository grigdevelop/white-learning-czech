﻿using System;
using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Core.Data
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }       
    }
}
