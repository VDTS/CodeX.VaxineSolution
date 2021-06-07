using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib
{
    public static class SharedData
    {
        public static Guid ClusterId { get; set; }
        public static Guid TeamId { get; set; }
        public static string Email { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }
    }
}
