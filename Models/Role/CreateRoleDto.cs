﻿namespace EmployeeApi.Models
{
    public class CreateRoleDto
    {
        public string Position { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }
    }
}
