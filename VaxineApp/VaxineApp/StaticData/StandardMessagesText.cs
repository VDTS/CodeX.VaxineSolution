using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.StaticData
{
    public static class StandardMessagesText
    {
        // Add
        public static string AddTitle { get; set; } = "Added";
        public static string AddBody(string input)
        {
            return $"{input} has been submitted";
        }

        // Delete
        public static string DeleteTitle { get; set; } = "Deleted";
        public static string DeleteBody(string input)
        {
            return $"{input} has been removed";
        }

        // Edit
        public static string EditTitle { get; set; } = "Updated";
        public static string EditBody(string input)
        {
            return $"{input} has been updated";
        }

        // Canceled
        public static string CanceledTitle { get; set; } = "Operation Canceled";
        public static string CanceledBody { get; set; } = "Try again later";

        // No data
        public static string NoDataTitle { get; set; } = "No data available";
        public static string NoDataBody { get; set; } = "No data available at the moment, come later or add data";

        // Invalid Data
        public static string InvalidDataTitle { get; set; } = "Invalid Data";
        public static string InvalidDataBody { get; set; } = "Add valid data to required Fields";
    }
}
